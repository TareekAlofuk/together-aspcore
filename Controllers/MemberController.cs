using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using together_aspcore.App.Member;
using together_aspcore.Shared;
using together_aspcore.Shared.Response;

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
        public async Task<ActionResult<MemberFile>> UploadAttachment(int id, [FromForm] MemberFile fileInfo,
            IFormFile file)
        {
            var memberFile = await _memberService.StoreMemberAttachment(id, fileInfo.DisplayFileName, file);
            return Ok(memberFile);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Member>> GetMemberInfo(int id)
        {
            var member = await _memberService.GetMemberInfo(id);
            if (member == null) return NotFound();
            return member;
        }


        [HttpPut("{id}/disable")]
        public async Task<ActionResult<SuccessfulStatusResponse>> Disable(int id)
        {
            var success = await _memberService.ChangeDisableStatus(id, true);
            return Ok(new SuccessfulStatusResponse {Success = success});
        }

        [HttpPut("{id}/enable")]
        public async Task<ActionResult<Member>> Enable(int id)
        {
            var success = await _memberService.ChangeDisableStatus(id, false);
            return Ok(new SuccessfulStatusResponse {Success = success});
        }


        [HttpPut("{id}/archive")]
        public async Task<ActionResult<Member>> Archive(int id)
        {
            var success = await _memberService.ChangeArchiveStatus(id, true);
            return Ok(new SuccessfulStatusResponse {Success = success});
        }

        [HttpPut("{id}/unarchive")]
        public async Task<ActionResult<Member>> UnArchive(int id)
        {
            var success = await _memberService.ChangeArchiveStatus(id, false);
            return Ok(new SuccessfulStatusResponse {Success = success});
        }


        [HttpGet("recent")]
        public async Task<ActionResult<List<Member>>> GetRecentlyAdded()
        {
            // todo : remove hard coded limit
            const int limit = 50;
            return await _memberService.GetRecentlyAdded(limit);
        }


        [HttpGet("search")]
        public async Task<ActionResult<List<Member>>> SearchEmployees([FromQuery]string query)
        {
            return await _memberService.FindMembers(query);
        }
    }
}