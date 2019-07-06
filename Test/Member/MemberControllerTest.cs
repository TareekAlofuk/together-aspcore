using Microsoft.Extensions.DependencyInjection;
using together_aspcore.App.Member;
using together_aspcore.Controllers;
using Xunit;

namespace together_aspcore.Test.Member
{
    public class MemberControllerTest
    {
        [Fact]
        public void ShouldAddNewMember()
        {
            var memberService = new Moq.Mock<IMemberService>();

            var controller = new MemberController(memberService.Object);
            var member = new App.Member.Member() {Name = "Ali"};

            var newMember = controller.CreateMember(member);

            Assert.True(newMember.Result.Value.Id > 0);
        }
    }
}