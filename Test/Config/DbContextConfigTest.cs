using System.Linq;
using Microsoft.EntityFrameworkCore;
using together_aspcore.App.Member;
using together_aspcore.Config;
using Xunit;

namespace together_aspcore.Test.Config
{
    public class DbContextConfigTest
    {
        private TogetherDbContext _togetherDbContext;

        public DbContextConfigTest()
        {
            var options = new DbContextOptionsBuilder<TogetherDbContext>()
                .UseInMemoryDatabase(databaseName: "db")
                .Options;
            _togetherDbContext = new TogetherDbContext(options);
        }

        [Fact]
        public void ShouldConnectToDatabase()
        {
            var count = _togetherDbContext.Members.Count();
            Assert.Equal(0, count);

            var member = new App.Member.Member {Name = "Test"};
            _togetherDbContext.Members.Add(member);
            _togetherDbContext.SaveChanges();

            count = _togetherDbContext.Members.Count();
            Assert.Equal(1, count);
        }
    }
}