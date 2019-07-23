using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace together_aspcore.App.Member.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }


        //<REQUIRED> ==========================================

        [Required]
        public string Title { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Phone { set; get; }

        [Required]
        public DateTime PassportExpirationDate { set; get; }

        [Required]
        public string PassportNo { get; set; }

        [Required]
        public MembershipType Type { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
        //</REQUIRED> ==========================================


        public string SecondaryPhone { set; get; }

        [EmailAddress]
        public string Email { set; get; }

        public string Address { set; get; }
        public DateTime? BirthDate { set; get; }

        public string JobTitle { set; get; }


        public string PassportImage { set; get; }
        public string FaceImage { set; get; }

        public bool Disabled { set; get; }
        public bool Archived { set; get; }
    }
}