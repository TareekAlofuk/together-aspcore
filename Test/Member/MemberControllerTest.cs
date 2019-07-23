using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using together_aspcore.App.Member;
using together_aspcore.Controllers;
using together_aspcore.Shared;
using Xunit;

namespace together_aspcore.Test.Member
{
    public class MemberControllerTest
    {
        [Fact]
        public async Task ShouldAddNewMember()
        {
            var memberService = new Mock<IMemberService>();
            var member = new App.Member.Models.Member {Name = "Ali", Id = 1};
            memberService.Setup(e => e.CreateNewMember(It.IsAny<App.Member.Models.Member>()))
                .Returns(Task.FromResult(member));

            var controller = new MemberController(memberService.Object);
            var r = await controller.CreateMember(member);

            var result = r.Result as OkObjectResult;
            Assert.NotNull(result);
            var newMember = result.Value as App.Member.Models.Member;
            Assert.NotNull(newMember);
            Assert.Equal(member.Id, newMember.Id);
            Assert.Equal(member.Name, newMember.Name);
        }

        [Fact]
        public async Task ShouldReturnBadRequestResponse()
        {
            var memberService = new Mock<IMemberService>();
            var member = new App.Member.Models.Member {Name = "Ali", Id = 1};
            memberService.Setup(e => e.CreateNewMember(It.IsAny<App.Member.Models.Member>()))
                .ThrowsAsync(new DuplicateNameException());

            var controller = new MemberController(memberService.Object);
            var r = await controller.CreateMember(member);

            var result = Assert.IsType<BadRequestObjectResult>(r.Result);
            var badRequestResponse = Assert.IsType<BadRequestResponse>(result.Value);
            Assert.Equal(MemberErrorCode.NAME_ALREADY_EXISTS, badRequestResponse.ErrorCode);
        }

        [Fact]
        public async Task ShouldGetAllMembers()
        {
            var memberServiceMock = new Mock<IMemberService>();
            var testMembers = new List<App.Member.Models.Member>
            {
                new App.Member.Models.Member {Name = "Ali", Id = 1},
                new App.Member.Models.Member {Name = "Mustafa", Id = 2}
            };

            memberServiceMock.Setup(x => x.GetAllMembers())
                .ReturnsAsync(testMembers);

            var controller = new MemberController(memberServiceMock.Object);
            var r = await controller.AllMembers();
            var result = r.Result as OkObjectResult;
            Assert.NotNull(result);
            var members = result.Value as List<App.Member.Models.Member>;
            Assert.NotNull(members);
            Assert.Equal(2, members.Count);
        }

        [Fact]
        public async Task ShouldEditMember()
        {
            var editedMember = new App.Member.Models.Member {Name = "Ali", Id = 1, Phone = "0000"};
            var memberServiceMock = new Mock<IMemberService>();
            memberServiceMock.Setup(x => x.EditExistingMember(It.IsAny<App.Member.Models.Member>()))
                .ReturnsAsync(editedMember);

            var controller = new MemberController(memberServiceMock.Object);
            var testMember = new App.Member.Models.Member {Name = "SOMEONE", Id = 1};
            var r = await controller.EditExistingMember(1, testMember);

            var result = r.Result as OkObjectResult;
            Assert.NotNull(result);
            var member = result.Value as App.Member.Models.Member;
            Assert.NotNull(member);
            Assert.Equal(editedMember.Id, member.Id);
            Assert.Equal(editedMember.Name, member.Name);
            Assert.Equal(editedMember.Phone, member.Phone);
        }

        [Fact]
        public async Task ShouldRejectEditWhenIdsNotIdentical()
        {
            var editedMember = new App.Member.Models.Member {Name = "Ali", Id = 1, Phone = "0000"};
            var memberServiceMock = new Mock<IMemberService>();
            memberServiceMock.Setup(x => x.EditExistingMember(It.IsAny<App.Member.Models.Member>()))
                .ReturnsAsync(editedMember);

            var controller = new MemberController(memberServiceMock.Object);
            var testMember = new App.Member.Models.Member {Name = "SOMEONE", Id = 1};
            var r = await controller.EditExistingMember(2, testMember);

            var result = r.Result as BadRequestObjectResult;
            Assert.NotNull(result);
            var badRequestResponse = result.Value as BadRequestResponse;
            Assert.NotNull(badRequestResponse);
            var error = badRequestResponse.ErrorCode is MemberErrorCode value ? value : 0;
            Assert.Equal(MemberErrorCode.CANNOT_CHANGE_ID, error);
        }
    }
}