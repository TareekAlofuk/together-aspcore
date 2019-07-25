using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace together_aspcore.App.Member
{
    public interface IMemberRepository
    {
        Task<Models.Member> Create(Models.Member member);
        Task<List<Models.Member>> GetAll();
        Task<Models.Member> Edit(Models.Member member);
        Task<MemberCredentials> SaveCredentials(MemberCredentials memberCredentials);
        Task<MemberFile> CreateMemberAttachment(MemberFile memberFile);
        Task<Models.Member> GetMemberInfo(int id);
        Task<bool> ChangeArchiveStatus(int memberId, bool archive);
        Task<bool> ChangeDisableStatus(int memberId, bool disable);
        Task<List<Models.Member>> GetRecentlyAdded(int limit);


        Task<List<Models.Member>> FindMembersById(int id);
        Task<List<Models.Member>> FindMembersByName(string name);


        Task<List<MemberFile>> GetMemberAttachments(int memberId);
        Task<MemberFile> GetMemberFile(int id);
        Task<bool> DeleteAttachedMemberFile(int id);
        Task<List<Models.Member>> GetMembersWithExpiredMembership();
        Task<List<Models.Member>> GetMembersWithNearlyExpiredMembership();
        Task<List<Models.Member>> GetMembersWithPassportWillExpire();
        Task<List<Models.Member>> GetMembersWithBirthDate();
        Task<List<Models.Member>> GetArchivedMembers();
        Task<List<Models.Member>> GetMembersWithDisabledMembership();
    }
}