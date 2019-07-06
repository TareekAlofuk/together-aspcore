using System.Collections.Generic;
using System.Threading.Tasks;

namespace together_aspcore.App.Member
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> CreateNewMember(Member member)
        {
            return await _memberRepository.Create(member);
        }

        public async Task<List<Member>> GetAllMembers()
        {
            return await _memberRepository.GetAll();
        }
    }
}