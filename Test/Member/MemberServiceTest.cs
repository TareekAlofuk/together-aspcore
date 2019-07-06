using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using together_aspcore.App.Member;
using Xunit;

namespace together_aspcore.Test.Member
{
    public class MemberServiceTest
    {
        [Fact]
        public async void ShouldCreateNewMember()
        {
            var member = new App.Member.Member {Name = "Ali", Id = 1};
            var memberRepositoryMock = new Mock<IMemberRepository>();
            memberRepositoryMock.Setup(x => x.Create(It.IsAny<App.Member.Member>()))
                .Returns(Task.FromResult(member));

            var service = new MemberService(memberRepositoryMock.Object);
            var newMember = await service.CreateNewMember(member);

            Assert.Equal(member.Id, newMember.Id);
            Assert.Equal(member.Name, newMember.Name);
        }

        [Fact]
        public async Task ShouldReturnAllMembers()
        {
            var members = new List<App.Member.Member>
            {
                new App.Member.Member {Name = "Ali", Id = 1}, new App.Member.Member {Name = "Mustafa", Id = 2}
            };

            var memberRepositoryMock = new Mock<IMemberRepository>();
            memberRepositoryMock.Setup(x => x.GetAll())
                .ReturnsAsync(members);

            var service = new MemberService(memberRepositoryMock.Object);

            var allMembers = await service.GetAllMembers();
            Assert.Equal(2, allMembers.Count);
        }
    }
}