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

        public async Task<Credential> SetCredential(Credential credential)
        {
            EntityEntry<Credential> newCredential = await _dbContext.Credentials.AddAsync(credential);
            return newCredential.Entity;
        }

        public async Task<Credential> EditCredential(Credential credential)
        {
            Credential editedCredential = _dbContext.Credentials.Find(credential);
            editedCredential.Email = credential.Email;
            editedCredential.Password = credential.Password;
            _dbContext.SaveChanges();
            return editedCredential;


        }

        public async Task<File> SaveFile(int id, File file)
        {

            Member member = _dbContext.Members.Find(id);
            if (file != null && member != null)
            {
                file.Member = member;
                _dbContext.Files.Add(file);
                await _dbContext.SaveChangesAsync();
                return file;
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
    }
}