using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using together_aspcore.App.Member;
using Xunit;

namespace together_aspcore.Test.Member
{
    public class MemberServiceTest
    {
        private readonly DependencyResolverHelpercs _serviceProvider;

        public MemberServiceTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/../../../")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .Build();
            _serviceProvider = new DependencyResolverHelpercs(webHost);
        }

        [Fact]
        public async void ShouldCreateNewMember()
        {

            var memberService = _serviceProvider.GetService<IMemberService>();
            var newMember = await memberService.CreateNewMember(new App.Member.Member() {Name = "Ali"});

            Assert.Equal("Ali", newMember.Name);
        }
    }
}