using System.Threading.Tasks;
using together_aspcore.App.Member;
using together_aspcore.Shared;
using Xunit;

namespace together_aspcore.Test.Member
{
    public class MemberRepositoryTest
    {
        private async Task AddFakeMembers(TogetherDbContext dbContext)
        {
            dbContext.Members.Add(new App.Member.Member {Name = "Ali", Id = 1, Archived = false});
            dbContext.Members.Add(new App.Member.Member {Name = "Mustafa", Id = 2, Archived = false});
            dbContext.Members.Add(new App.Member.Member {Name = "Someone", Id = 3, Archived = true});
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task ShouldAddNewMember()
        {
            var memberRepository = new MemberRepository(TestHelper.GetInMemoryDbContext(nameof(ShouldAddNewMember)));
            const string name = "X";
            var member = await memberRepository.Create(new App.Member.Member {Name = name});
            Assert.Equal(name, member.Name);
            Assert.True(member.Id > 0);
        }

        [Fact]
        public async Task ShouldReturnAllMembers()
        {
            var context = TestHelper.GetInMemoryDbContext(nameof(ShouldReturnAllMembers));
            await AddFakeMembers(context);
            var memberRepository = new MemberRepository(context);
            var allMembers = await memberRepository.GetAll();
            Assert.Equal(2, allMembers.Count);
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