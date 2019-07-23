using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace together_aspcore.App.Member
{
    public interface IMemberService
    {
        Task<Models.Member> CreateNewMember(Models.Member member);
        Task<List<Models.Member>> GetAllMembers();
        Task<Models.Member> EditExistingMember(Models.Member member);
        Task<MemberCredentials> SaveCredentials(MemberCredentials memberCredentials);
        Task<MemberFile> StoreMemberAttachment(int memberId, string displayFileName, IFormFile file);
        Task<Models.Member> GetMemberInfo(int id);
        Task<bool> ChangeArchiveStatus(int id, bool archive);
        Task<bool> ChangeDisableStatus(int id, bool disabled);
        Task<List<Models.Member>> GetRecentlyAdded(int limit);


        Task<List<Models.Member>> FindMembers(string query);


        Task<List<MemberFile>> GetMemberAttachments(int memberId);
        Task<bool> DeleteAttachedFile(int fileId);


        void RenewMembership(int memberId, DateTime until);
        void UpgradeMembership(int memberId, MembershipType newMembershipType, DateTime? until);
    }
}