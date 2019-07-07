using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using together_aspcore.App.Member;
using together_aspcore.Controllers;
using Xunit;

namespace together_aspcore.Test.Member
{
    public class MemberControllerTest
    {
        [Fact]
        public async Task ShouldAddNewMember()
        {
            var memberService = new Mock<IMemberService>();
            var member = new App.Member.Member {Name = "Ali", Id = 1};
            memberService.Setup(e => e.CreateNewMember(It.IsAny<App.Member.Member>()))
                .Returns(Task.FromResult(member));

            var controller = new MemberController(memberService.Object);
            var r = await controller.CreateMember(member);

            var result = r.Result as OkObjectResult;
            Assert.NotNull(result);
            var newMember = result.Value as App.Member.Member;
            Assert.NotNull(newMember);
            Assert.Equal(member.Id, newMember.Id);
            Assert.Equal(member.Name, newMember.Name);
        }

        [Fact]
        public async Task ShouldEditMember()
        {
            var editedMember = new App.Member.Member {Name = "Ali", Id = 1, Phone = "0000"};
            var memberServiceMock = new Mock<IMemberService>();
            memberServiceMock.Setup(x => x.EditExistingMember(It.IsAny<App.Member.Member>()))
                .ReturnsAsync(editedMember);

            var controller = new MemberController(memberServiceMock.Object);
            var testMember = new App.Member.Member {Name = "SOMEONE", Id = 1};
            var r = await controller.EditExistingMember(testMember);

            var result = r.Result as OkObjectResult;
            Assert.NotNull(result);
            var member = result.Value as App.Member.Member;
            Assert.NotNull(member);
            Assert.Equal(editedMember.Id, member.Id);
            Assert.Equal(editedMember.Name, member.Name);
            Assert.Equal(editedMember.Phone, member.Phone);
        }
    }
}