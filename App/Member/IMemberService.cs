using System.Collections.Generic;
using System.Threading.Tasks;

namespace together_aspcore.App.Member
{
    public interface IMemberService
    {
        Task<Member> CreateNewMember(Member member);
        Task<List<Member>> GetAllMembers();
        Task<Member> EditExistingMember(Member member);
    }
}