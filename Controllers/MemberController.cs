using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using together_aspcore.App.Member;
using together_aspcore.App.Member.Exceptions;
using together_aspcore.App.Member.Models;
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
            var members = await _memberService.GetAllMembers();
            return Ok(members);
        }


        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember([FromForm] Member member)
        {
            try
            {
                var newMember = await _memberService.CreateNewMember(member);
                return Ok(newMember);
            }
            catch (DuplicateNameException)
            {
                return BadRequest(new BadRequestResponse {ErrorCode = MemberErrorCode.NAME_ALREADY_EXISTS});
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPost("{memberId}/upload-passport")]
        public async Task<ActionResult> UploadPassportImage(int memberId, IFormFile passport)
        {
            try
            {
                var filename = await _memberService.UploadPassport(memberId, passport);
                return Ok(new SuccessfulStatusResponse {Success = true, extra = filename});
            }
            catch (Exception)
            {
                return BadRequest(new SuccessfulStatusResponse {Success = false});
            }
        }


        [HttpPost("{memberId}/upload-identity-image")]
        public async Task<ActionResult> UploadFaceImage(int memberId, IFormFile identityImage)
        {
            try
            {
                var filename = await _memberService.UploadIdentityImage(memberId, identityImage);
                return Ok(new SuccessfulStatusResponse {Success = true, extra = filename});
            }
            catch (Exception)
            {
                return BadRequest(new SuccessfulStatusResponse {Success = false});
            }
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
                return BadRequest(BadRequestResponse.Create(MemberErrorCode.DATA_IS_NOT_VALID));
            }

            var newCredential = await _memberService.SaveCredentials(memberCredentials);
            return Ok(newCredential);
        }


        [HttpGet("{memberId}")]
        public ActionResult GetAttachments(int memberId)
        {
            var attachments = _memberService.GetMemberAttachments(memberId);
            return Ok();
        }

        [HttpPost("{id}/attachment")]
        public async Task<ActionResult<MemberFile>> UploadAttachment(int id,
            IFormFile file)
        {
            var memberFile = await _memberService.StoreMemberAttachment(id, file.FileName, file);
            return Ok(memberFile);
        }

        [HttpDelete("attachment/{fileId}")]
        public async Task<ActionResult> DeleteAttachment(int fileId)
        {
            var success = await _memberService.DeleteAttachedFile(fileId);
            if (success)
                return Ok();
            return BadRequest();
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
        public async Task<ActionResult<List<Member>>> SearchEmployees([FromQuery] string query)
        {
            return await _memberService.FindMembers(query);
        }

        [HttpGet("{id:int}/attachments")]
        public async Task<ActionResult<List<MemberFile>>> GetMemberAttachments(int id)
        {
            return await _memberService.GetMemberAttachments(id);
        }

        [HttpDelete("{id:int}/attachments/{fileId:int}")]
        public async Task<ActionResult<SuccessfulStatusResponse>> DeleteAttachedFile(int fileId)
        {
            var success = await _memberService.DeleteAttachedFile(fileId);
            return Ok(new SuccessfulStatusResponse {Success = success});
        }


        [HttpGet("expired")]
        public async Task<ActionResult<List<Member>>> GetExpiredMembership()
        {
            var members = await _memberService.GetMembersWithExpiredMembership();
            return Ok(members);
        }

        [HttpGet("will-expire")]
        public async Task<ActionResult<List<Member>>> GetExpiredMembershipWillExpire()
        {
            var members = await _memberService.GetMembersWithNearlyExpiredMembership();
            return Ok(members);
        }

        [HttpGet("passport-will-expire")]
        public async Task<ActionResult<List<Member>>> GetPassportWillExpire()
        {
            var members = await _memberService.GetMembersWithPassportWillExpire();
            return Ok(members);
        }


        [HttpGet("on-birth-date")]
        public async Task<ActionResult<List<Member>>> GetMembersWithBirthDate()
        {
            var members = await _memberService.GetMembersWithBirthDate();
            return Ok(members);
        }

        [HttpGet("disabled")]
        public async Task<ActionResult<List<Member>>> GetMembersWithDisabledMembership()
        {
            var members = await _memberService.GetMembersWithDisabledMembership();
            return Ok(members);
        }

        [HttpGet("archived")]
        public async Task<ActionResult<List<Member>>> GetArchivedMembers()
        {
            var members = await _memberService.GetArchivedMembers();
            return Ok(members);
        }

        [HttpPost("{memberId:int}/upgrade")]
        public async Task<ActionResult<Member>> UpgradeMembership(int memberId,
            [FromForm] UpgradeMembershipRequestModel upgradeModel)
        {
            try
            {
                var member = await _memberService.UpgradeMembership(memberId, upgradeModel.MembershipType,
                    upgradeModel.Until);
                return Ok(member);
            }
            catch (MemberNotFoundException)
            {
                return BadRequest(new BadRequestResponse {ErrorCode = MemberErrorCode.MEMBER_NOT_FOUND});
            }
        }
    }
}


//job title => options
//home page show must do
//SEND SMS , Email
//ORDER RESULTS