using Microsoft.EntityFrameworkCore;
using together_aspcore.App.Member;
using together_aspcore.App.Member.Models;
using together_aspcore.App.Service.Dto;
using together_aspcore.App.Service.Models;
using together_aspcore.App.Wallet;

namespace together_aspcore.Shared
{
    public class TogetherDbContext : DbContext
    {
        public virtual DbSet<MemberCredentials> MembersCredentials { get; set; }
        public virtual DbSet<Member> Members { get; set; }

        public virtual DbSet<MemberFile> Files { get; set; }
        public DbSet<Wallet> Wallets { get; set; }


        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceRule> ServicesRules { get; set; }
        public virtual DbSet<ServiceStore> ServicesStore { get; set; }
        public virtual DbSet<ServiceUsage> ServicesUsages { get; set; }
        public virtual DbSet<MembershipServiceDefault> ServiceMembershipDefaults { get; set; }


        public DbQuery<ServiceDetail> ServiceDetails { get; set; }
        public DbQuery<MemberAutoCompleteModel> MemberAutoComplete { get; set; }
        public DbQuery<MemberServiceQureyModel> ServicesUsageOfMember { get; set; }
        public DbSet<WalletAction> WalletActions { get; set; }

        public TogetherDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}