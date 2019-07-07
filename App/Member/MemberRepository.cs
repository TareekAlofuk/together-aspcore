using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var newMember = await _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            return newMember.Entity;
        }

        public async Task<List<Member>> GetAll()
        {
            return await _dbContext.Members.Where(x => !x.Archived).ToListAsync();
        }

        public async Task<Member> Edit(Member member)
        {
            var editedMember = _dbContext.Members.Update(member);
            await _dbContext.SaveChangesAsync();
            return editedMember.Entity;
        }
    }
}