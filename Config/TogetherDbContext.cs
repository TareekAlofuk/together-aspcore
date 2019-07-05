using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using together_aspcore.App.Member;

namespace together_aspcore.Config
{
    public class TogetherDbContext : DbContext
    {
        public virtual DbSet<Member> Members { get; set; }

        public TogetherDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}