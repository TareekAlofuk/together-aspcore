using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Member> Create(Member member)
        {
            EntityEntry<Member> newMember = await _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            return newMember.Entity;
        }

        public async Task<List<Member>> GetAll()
        {
            return await _dbContext.Members.Where(x => !x.Archived).ToListAsync();
        }

        public async Task<Member> Edit(Member member)
        {
            EntityEntry<Member> editedMember = _dbContext.Members.Update(member);
            await _dbContext.SaveChangesAsync();
            return editedMember.Entity;
        }

        public async Task<MemberCredentials> SaveCredentials(MemberCredentials memberCredentials)
        {
            var credentials = _dbContext.MembersCredentials.Find(memberCredentials.MemberId);
            if (credentials != null)
            {
                credentials.Email = memberCredentials.Email;
                credentials.Password = memberCredentials.Password;
                var newCredentials = _dbContext.MembersCredentials.Update(credentials);
                await _dbContext.SaveChangesAsync();
                return newCredentials.Entity;
            }

            var newCredential = _dbContext.MembersCredentials.Add(memberCredentials);
            await _dbContext.SaveChangesAsync();
            return newCredential.Entity;
        }

        public async Task<MemberCredentials> EditCredential(MemberCredentials memberCredentials)
        {
            MemberCredentials editedMemberCredentials = _dbContext.MembersCredentials.Find(memberCredentials);
            editedMemberCredentials.Email = memberCredentials.Email;
            editedMemberCredentials.Password = memberCredentials.Password;
            _dbContext.SaveChanges();
            return editedMemberCredentials;
        }

        public async Task<MemberFile> SaveFile(int id, MemberFile memberFile)
        {
            Member member = _dbContext.Members.Find(id);
            if (memberFile != null && member != null)
            {
                memberFile.Member = member;
                _dbContext.Files.Add(memberFile);
                await _dbContext.SaveChangesAsync();
                return memberFile;
            }

            return null;
        }

        public async Task<Member> GetMemberInfo(int id)
        {
            Member member = await _dbContext.Members.FindAsync(id);
            if (member != null)
            {
                return member;
            }

            return null;
        }


        public async Task<Member> Archived(int id, bool archived)
        {
            Member member = await _dbContext.Members.FindAsync(id);
            if (member != null)
            {
                member.Archived = archived;
                _dbContext.SaveChanges();
            }

            return null;
        }

        public async Task<Member> Disabled(int id, bool disabled)
        {
            Member member = await _dbContext.Members.FindAsync(id);
            if (member != null)
            {
                member.Disabled = disabled;
                _dbContext.SaveChanges();
            }

            return null;
        }

        public Task<List<Member>> GetRecentlyAdded(int number)
        {
            try
            {
                IQueryable<Member> members = _dbContext.Members.TakeLast(number);
                return members.ToListAsync<Member>();
            }
            catch
            {
            }

            return null;
        }

        public async Task<Member> GetById(int id)
        {
            try
            {
                Task<Member> member = _dbContext.Members.FindAsync(id);
                if (member != null)
                {
                    return await member;
                }
            }
            catch
            {
            }

            return null;
        }

        public async Task<List<Member>> GetByName(string name)
        {
            try
            {
                IQueryable<Member> member = _dbContext.Members.Where(itm => itm.Name.Contains(name));
                if (member != null)
                {
                    return await member.ToListAsync();
                }
            }
            catch
            {
            }

            return null;
        }
    }
}