using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using together_aspcore.App.Member;
using together_aspcore.Shared;

namespace together_aspcore.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Member>>> AllMembers()
        {
            var members = await _memberService.GetAllMembers();
            return Ok(members);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember(Member member)
        {
            var newMember = await _memberService.CreateNewMember(member);
            return Ok(newMember);
        }


        [HttpPost("upload-passport")]
        public ActionResult UploadPassportImage()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Member>> EditExistingMember(int id, Member member)
        {
            if (member.Id != id)
            {
                return BadRequest(BadRequestResponse.Create(MemberErrorMessage.CANNOT_CHANGE_ID));
            }

            var editedMember = await _memberService.EditExistingMember(member);
            return Ok(editedMember);
        }
    }
}