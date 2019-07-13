using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            List<Member> members = await _memberService.GetAllMembers();
            return Ok(members);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember([FromForm] Member member)
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
        public async Task<ActionResult<Member>> EditExistingMember(int id, [FromForm] Member member)
        {
            member.Id = id;
            var editedMember = await _memberService.EditExistingMember(member);
            return Ok(editedMember);
        }


        [HttpPost("{id}/credentials")]
        public async Task<ActionResult<Member>> SaveCredentials(int id, [FromForm] MemberCredentials memberCredentials)
        {
            if (memberCredentials.MemberId != id)
            {
                return BadRequest(BadRequestResponse.Create(MemberErrorMessage.DATA_IS_NOT_VALID));
            }

            var newCredential = await _memberService.SaveCredentials(memberCredentials);
            return Ok(newCredential);
        }

        [HttpPost("{id}/upload-attachment")]
        public async Task<ActionResult<MemberFile>> UploadAttachment(int id, [FromForm] MemberFile fileInfo , IFormFile file)
        {
            var memberFile = await _memberService.StoreMemberAttachment(id, fileInfo.DisplayFileName , file);
            return Ok(memberFile);
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


        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetById(int id)
        {
            return await _memberService.GetById(id);
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<List<Member>>> GetByName(string name)
        {
            return await _memberService.GetByName(name);
        }
    }
}