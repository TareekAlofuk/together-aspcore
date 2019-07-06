using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using together_aspcore.App.Member;
using together_aspcore.Config;
using Xunit;

namespace together_aspcore.Test.Member
{
    public class MemberRepositoryTest
    {
        private readonly MemberRepository _memberRepository;

        public MemberRepositoryTest()
        {
            _memberRepository = new MemberRepository(TestHelper.GetInMemoryDbContext());
        }

        [Fact]
        public async Task ShouldAddNewMember()
        {
            const string name = "Ali";
            var member = await _memberRepository.Create(member: new App.Member.Member() {Name = name});
            Assert.Equal(name, member.Name);
            Assert.True(member.Id > 0);
        }
        
        
    }
}