using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace together_aspcore.App.Member
{
    public interface IMemberRepository
    {
        Task<Member> Create(Member member);
        Task<List<Member>> GetAll();
        Task<Member> Edit(Member member);
        Task<MemberCredentials> SaveCredentials(MemberCredentials memberCredentials);
        Task<MemberFile> CreateMemberAttachment(MemberFile memberFile);
        Task<Member> GetMemberInfo(int id);
        Task<bool> ChangeArchiveStatus(int memberId, bool archive);
        Task<bool> ChangeDisableStatus(int memberId, bool disable);
        Task<List<Member>> GetRecentlyAdded(int limit);


        Task<List<Member>> FindMembersById(int id);
        Task<List<Member>> FindMembersByName(string name);


        Task<List<MemberFile>> GetMemberAttachments(int memberId);
        Task<MemberFile> GetMemberFile(int id);
        Task<bool> DeleteAttachedMemberFile(int id);
    }
}