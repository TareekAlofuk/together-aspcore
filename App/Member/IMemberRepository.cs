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
        Task<Credential> SetCredential(Credential credential);
        Task<Credential> EditCredential(Credential credential);
        Task<File> SaveFile(int id, File file);
        Task<Member> GetMemberInfo(int id);
        Task<Member> Archived(int id, bool archived);
        Task<Member> Disabled(int id, bool disabled);
        Task<List<Member>> GetRecentlyadded(int number);
        Task<Member> GetById(int id);
        Task<List<Member>> GetByName(string name);


    }
}