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


        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberInfo(int id)
        {
            return await _memberService.GetMemberInfo(id);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> Disable(int id)
        {
            return await _memberService.Disabled(id, true);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> Enable(int id)
        {
            return await _memberService.Disabled(id, false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> Archive(int id)
        {
            return await _memberService.Archived(id, true);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> UnArchive(int id)
        {
            return await _memberService.Archived(id, false);
        }
        [HttpGet("{number}")]
        public async Task<ActionResult<List<Member>>> GetRecentlyadded(int number)
        {
            return await _memberService.GetRecentlyadded(number);
        }




    }
}