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
    }
}