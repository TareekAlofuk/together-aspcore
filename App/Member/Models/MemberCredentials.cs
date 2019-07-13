using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace together_aspcore.App.Member
{
    public class MemberCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [Key, ForeignKey("MemberFK")] public int MemberId { get; set; }
    }
}