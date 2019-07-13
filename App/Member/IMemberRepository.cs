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
        Task<Member> Archived(int id, bool archived);
        Task<bool> ChangeDisableStatus(int memberId, bool disable);
        Task<List<Member>> GetRecentlyAdded(int limit);
        Task<Member> GetById(int id);
        Task<List<Member>> GetByName(string name);


    }
}