using System;
using Microsoft.EntityFrameworkCore;
using together_aspcore.App.Member;
using together_aspcore.Shared;

namespace together_aspcore.Test.Member
{
    public class MemberTestHelper
    {
        public static void Seed(TogetherDbContext context)
        {
            context.Members.Add(
                new App.Member.Models.Member
                {
                    Name = "Ali",
                    Phone = "07800000",
                    ExpirationDate = new DateTime(2020, 10, 10),
                    PassportExpirationDate = new DateTime(2020, 10, 10),
                    PassportNo = "A120358",
                    Type = MembershipType.SILVER,
                    Title = "MR"
                }
            );
            context.Members.Add(
                new App.Member.Models.Member
                {
                    Name = "Mustafa",
                    Phone = "07800001",
                    ExpirationDate = new DateTime(2020, 10, 10),
                    PassportExpirationDate = new DateTime(2020, 10, 10),
                    PassportNo = "A120358",
                    Type = MembershipType.SILVER,
                    Title = "MR"
                }
            );


            context.SaveChanges();
        }
    }
}