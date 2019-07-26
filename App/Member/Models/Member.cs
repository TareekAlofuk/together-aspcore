using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using together_aspcore.Shared.JsonConverter;

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
        [Column(TypeName = "Date")]
        [JsonConverter(typeof(OnlyDate))]
        public DateTime PassportExpirationDate { set; get; }

        [Required]
        public string PassportNo { get; set; }

        [Required]
        public MembershipType Type { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [JsonConverter(typeof(OnlyDate))]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [JsonConverter(typeof(OnlyDate))]
        public DateTime JoinDate { get; set; }


        
        [Column(TypeName = "Date")]
        [JsonConverter(typeof(OnlyDate))]
        public DateTime? BirthDate { set; get; }
        //</REQUIRED> ==========================================


        public string SecondaryPhone { set; get; }

        [EmailAddress]
        public string Email { set; get; }

        public string Address { set; get; }

        public string JobTitle { set; get; }


        public string PassportImage { set; get; }
        public string FaceImage { set; get; }

        public bool Disabled { set; get; }
        public bool Archived { set; get; }
    }
}