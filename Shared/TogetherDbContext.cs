using Microsoft.EntityFrameworkCore;
using together_aspcore.App.Member;
using together_aspcore.App.Member.Models;

namespace together_aspcore.Shared
{
    public class TogetherDbContext : DbContext
    {
        public virtual DbSet<MemberCredentials> MembersCredentials { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        //TODO : rename Files to MemberFiles
        public virtual DbSet<MemberFile> Files { get; set; }
        public TogetherDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}