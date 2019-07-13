using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            List<Member> members = await _memberService.GetAllMembers();
            return Ok(members);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember(Member member)
        {
            Member newMember = await _memberService.CreateNewMember(member);
            return Ok(newMember);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Member>> EditExistingMember(int id, Member member)
        {
            if (member.Id != id)
            {
                return BadRequest(BadRequestResponse.Create(MemberErrorMessage.CANNOT_CHANGE_ID));
            }

            Member editedMember = await _memberService.EditExistingMember(member);
            return Ok(editedMember);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Member>> SetCredential(int id, Credential credential)
        {
            // check the input id Member to prevent spoofing(check input id with logined id)
            if (credential.Member.Id != id)
            {
                return BadRequest(BadRequestResponse.Create(MemberErrorMessage.CANNOT_CHANGE_ID));
            }
            Credential Newcredential = await _memberService.SetCredentialAsync(credential);
            return Ok(Newcredential);
        }
        [HttpPut]
        public async Task<ActionResult<File>> SetFile(int id, IFormFile formFile)
        {
            // check the input id Member to prevent spoofing(check input id with logined id)

            File file = await _memberService.SaveFile(id, formFile);
            return Ok(file);
        }



    }
}