using System.Threading.Tasks;
using together_aspcore.App.Member;
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

        [Fact]
        public async Task ShouldEditMember()
        {
            var context = TestHelper.GetInMemoryDbContext(nameof(ShouldEditMember));
            var memberRepository = new MemberRepository(context);
            var testMember = await memberRepository.Create(new App.Member.Member {Name = "Ali"});

            testMember.Name = "EditedName";
            testMember.Phone = "TestPhone";

            var editedMember = await memberRepository.Edit(testMember);

            Assert.Equal("EditedName", editedMember.Name);
            Assert.Equal("TestPhone", editedMember.Phone);
        }
    }
}