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
        Task<Credential> SetCredentialAsync(Credential credential);
        Task<Credential> EditCredentialAsync(Credential credential);
        Task<File> SaveFile(int id, IFormFile file);
        Task<Member> GetMemberInfo(int id);
        Task<Member> Archived(int id, bool archived);
        Task<Member> Disabled(int id, bool disabled);
        Task<List<Member>> GetRecentlyadded(int number);
        Task<Member> GetById(int id);
        Task<List<Member>> GetByName(string name);


    }
}