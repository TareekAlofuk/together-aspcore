using System;

namespace together_aspcore.App.Member.Models
{
    public class UpgradeMembershipRequestModel
    {
        public DateTime Until { set; get; }
        public MembershipType MembershipType { set; get; }
    }
}