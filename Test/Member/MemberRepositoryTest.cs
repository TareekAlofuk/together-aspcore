using System;
using System.Data;
using System.Threading.Tasks;
using together_aspcore.App.Member;
using Xunit;

namespace together_aspcore.Test.Member
{
    public class MemberRepositoryTest
    {
        [Fact]
        public async Task ShouldAddNewMember()
        {
            const string name = "TestName";
            const string phone = "07800001";
            const string title = "MR";
            var passportExpirationDate = new DateTime(2020, 9, 27);
            const string passportNo = "A1208887";
            const MembershipType membershipType = MembershipType.SILVER;
            var expirationDate = new DateTime(2022, 10, 10);

            var member = new App.Member.Models.Member
            {
                Title = title,
                Name = name,
                Phone = phone,
                PassportExpirationDate = passportExpirationDate,
                PassportNo = passportNo,
                Type = membershipType,
                ExpirationDate = expirationDate
            };

            var memberRepository = new MemberRepository(TestHelper.GetInMemoryDbContext(nameof(ShouldAddNewMember)));

            var newMember = await memberRepository.Create(member);

            Assert.True(newMember.Id > 0);
            Assert.Equal(title, newMember.Title);
            Assert.Equal(name, newMember.Name);
            Assert.Equal(phone, newMember.Phone);
            Assert.Equal(expirationDate, newMember.ExpirationDate);
            Assert.Equal(membershipType, newMember.Type);
            Assert.Equal(passportExpirationDate, newMember.PassportExpirationDate);
            Assert.Equal(passportNo, newMember.PassportNo);
        }

        [Fact]
        public async Task ShouldRejectDuplicateName()
        {
            var context = TestHelper.GetInMemoryDbContext(nameof(ShouldAddNewMember));
            MemberTestHelper.Seed(context);
            var memberRepository = new MemberRepository(context);

            const string name = "Ali";

            var member = new App.Member.Models.Member {Name = name};

            await Assert.ThrowsAsync<DuplicateNameException>(() => memberRepository.Create(member));
        }


        [Fact]
        public async Task ShouldReturnAllMembers()
        {
            var context = TestHelper.GetInMemoryDbContext(nameof(ShouldReturnAllMembers));
            MemberTestHelper.Seed(context);
            var memberRepository = new MemberRepository(context);
            var allMembers = await memberRepository.GetAll();
            Assert.Equal(2, allMembers.Count);
        }

        [Fact]
        public async Task ShouldEditMember()
        {
            var context = TestHelper.GetInMemoryDbContext(nameof(ShouldEditMember));
            var memberRepository = new MemberRepository(context);
            var testMember = await memberRepository.Create(new App.Member.Models.Member {Name = "Ali"});

            testMember.Name = "EditedName";
            testMember.Phone = "TestPhone";

            var editedMember = await memberRepository.Edit(testMember);

            Assert.Equal("EditedName", editedMember.Name);
            Assert.Equal("TestPhone", editedMember.Phone);
        }
    }
}