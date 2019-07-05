using Microsoft.EntityFrameworkCore;

namespace together_aspcore.App.Member
{
    public class MemberRepository : IMemberRepository
    {
        private DbContext _dbContext;

        public MemberRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Member Create()
        {
            return new Member();
        }
    }
}