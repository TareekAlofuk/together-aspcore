using System;
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
        public async Task<ActionResult<Member>> CreateMember([FromForm] Member member)
        {
            var newMember = await _memberService.CreateNewMember(member);
            return Ok(newMember);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Member>> EditExistingMember(int id, [FromForm] Member member)
        {
            member.Id = id;
            var editedMember = await _memberService.EditExistingMember(member);
            return Ok(editedMember);
        }

        [HttpPost("credentials")]
        public ActionResult SaveMemberCredentials()
        {
            return Ok();
        }
    }
}