using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using together_aspcore.App.Member;

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

        [HttpPut]
        public async Task<ActionResult<Member>> EditExistingMember([FromRoute(Name = "id")] int id, Member member)
        {
            if (member.Id != id)
            {
                return BadRequest(MemberErrorMessage.CANNOT_CHANGE_ID);
            }

            var editedMember = await _memberService.EditExistingMember(member);
            return Ok(editedMember);
        }
    }
}