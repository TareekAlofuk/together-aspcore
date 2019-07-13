using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace together_aspcore.App.Member
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public string Phone2 { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public DateTime? BirthDate { set; get; }
        public bool Disabled { set; get; }
        public bool Archived { set; get; }
        public string PassportImage { set; get; }
        public string FaceImage { set; get; }
    }
}