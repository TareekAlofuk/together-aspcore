using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using together_aspcore.App.Member.Exceptions;
using together_aspcore.App.Member.Models;
using together_aspcore.Shared;

namespace together_aspcore.App.Member
{
    public class MemberRepository : IMemberRepository
    {
        private readonly TogetherDbContext _dbContext;

        public MemberRepository(TogetherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.Member> Create(Models.Member member)
        {
            var memberWithSameName = await _dbContext.Members.Where(x => x.Name == member.Name).FirstOrDefaultAsync();
            if (memberWithSameName != null)
            {
                throw new DuplicateNameException($"{member.Name} is already exists");
            }

            member.JoinDate = DateTime.Now.Date;

            var newMember = await _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            return newMember.Entity;
        }

        public async Task<List<Models.Member>> GetAll()
        {
            return await _dbContext.Members.Where(x => !x.Archived).ToListAsync();
        }

        public async Task<Models.Member> Edit(Models.Member member)
        {
            var editedMember = _dbContext.Members.Update(member);
            await _dbContext.SaveChangesAsync();
            return editedMember.Entity;
        }

        public async Task<MemberCredentials> SaveCredentials(MemberCredentials memberCredentials)
        {
            var credentials = _dbContext.MembersCredentials.Find(memberCredentials.MemberId);
            if (credentials != null)
            {
                credentials.Username = memberCredentials.Username;
                credentials.Password = memberCredentials.Password;
                var newCredentials = _dbContext.MembersCredentials.Update(credentials);
                await _dbContext.SaveChangesAsync();
                return newCredentials.Entity;
            }

            var newCredential = _dbContext.MembersCredentials.Add(memberCredentials);
            await _dbContext.SaveChangesAsync();
            return newCredential.Entity;
        }

        public async Task<MemberFile> CreateMemberAttachment(MemberFile memberFile)
        {
            _dbContext.Files.Add(memberFile);
            await _dbContext.SaveChangesAsync();
            return memberFile;
        }

        public async Task<Models.Member> GetMemberInfo(int id)
        {
            var member = await _dbContext.Members.FindAsync(id);
            return member;
        }


        public async Task<bool> ChangeArchiveStatus(int memberId, bool archive)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.Id == memberId);
            if (member == null) return false;
            member.Archived = archive;
            _dbContext.Members.Update(member);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeDisableStatus(int memberId, bool disable)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.Id == memberId);
            if (member == null) return false;
            member.Disabled = disable;
            _dbContext.Members.Update(member);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Models.Member>> GetRecentlyAdded(int limit)
        {
            return await _dbContext.Members
                .OrderByDescending(x => x.Id)
                .Take(limit).ToListAsync();
        }

        public async Task<List<Models.Member>> FindMembersById(int id)
        {
            return await _dbContext.Members.Where(x => x.Id == id).ToListAsync();
        }

        public async Task<List<Models.Member>> FindMembersByName(string name)
        {
            name = "%" + name + "%";
            return await _dbContext.Members
                .Where(x => EF.Functions.ILike(x.Name, name))
                .ToListAsync();
        }

        public async Task<List<MemberFile>> GetMemberAttachments(int memberId)
        {
            return await _dbContext.Files.Where(x => x.MemberId == memberId).ToListAsync();
        }

        public async Task<MemberFile> GetMemberFile(int id)
        {
            return await _dbContext.Files.FindAsync(id);
        }

        public async Task<bool> DeleteAttachedMemberFile(int id)
        {
            var file = _dbContext.Files.Find(id);
            _dbContext.Files.Remove(file);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Models.Member>> GetMembersWithExpiredMembership()
        {
            var date = DateTime.Now;
            return await _dbContext.Members
                .Where(x => x.ExpirationDate <= date)
                .ToListAsync();
        }

        public async Task<List<Models.Member>> GetMembersWithNearlyExpiredMembership()
        {
            var date = DateTime.Now.AddDays(30);
            return await _dbContext.Members
                .Where(x => x.ExpirationDate <= date)
                .ToListAsync();
        }

        public async Task<List<Models.Member>> GetMembersWithPassportWillExpire()
        {
            var date = DateTime.Now.AddMonths(6);
            return await _dbContext.Members
                .Where(x => x.PassportExpirationDate <= date)
                .ToListAsync();
        }

        public async Task<List<Models.Member>> GetMembersWithBirthDate()
        {
            var date = DateTime.Now.Date;
            return await _dbContext.Members
                .Where(x => x.BirthDate == date)
                .ToListAsync();
        }

        public async Task<List<Models.Member>> GetArchivedMembers()
        {
            return await _dbContext.Members.Where(x => x.Archived).ToListAsync();
        }

        public async Task<List<Models.Member>> GetMembersWithDisabledMembership()
        {
            return await _dbContext.Members.Where(x => x.Disabled).ToListAsync();
        }

        public async Task<Models.Member> UpgradeMembership(int memberId, DateTime newExpirationDate,
            MembershipType membershipType)
        {
            var member = await _dbContext.Members.FindAsync(memberId);
            if (member == null)
            {
                throw new MemberNotFoundException();
            }

            member.ExpirationDate = newExpirationDate;
            member.Type = membershipType;

            return await Edit(member);
        }

        public async Task<List<MemberAutoCompleteModel>> GetSuggestions(string query)
        {
            var nameParam = new NpgsqlParameter("name", $"%{query.ToLower()}%");
            var sql = "SELECT \"Id\" , \"Name\" FROM \"Members\" WHERE LOWER(\"Name\") LIKE @name";
            return await _dbContext.MemberAutoComplete
                .FromSql(sql, nameParam)
                .ToListAsync();
        }
    }
}

//TODO : skip archived members from result