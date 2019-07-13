using Microsoft.EntityFrameworkCore;
using together_aspcore.App.Member;

namespace together_aspcore.Shared
{
    public class TogetherDbContext : DbContext
    {
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public TogetherDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}