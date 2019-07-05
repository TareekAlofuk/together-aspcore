using Microsoft.EntityFrameworkCore;

namespace together_aspcore.App.Member
{
    public interface IMemberRepository
    {
        Member Create();
    }
}