using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace together_aspcore.App.Member
{
    public interface IMemberService
    {
        Task<Member> CreateNewMember(Member member);
        Task<List<Member>> GetAllMembers();
        Task<Member> EditExistingMember(Member member);
        Task<MemberCredentials> SaveCredentials(MemberCredentials memberCredentials);
        Task<MemberFile> StoreMemberAttachment(int memberId, string displayFileName, IFormFile file);
        Task<Member> GetMemberInfo(int id);
        Task<bool> ChangeArchiveStatus(int id, bool archive);
        Task<bool> ChangeDisableStatus(int id, bool disabled);
        Task<List<Member>> GetRecentlyAdded(int limit);


        Task<List<Member>> FindMembers(string query);
    }
}