using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace together_aspcore.App.Member
{
    public class MemberFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FileName { get; set; }
        public string DisplayFileName { get; set; }
        public int MemberId { get; set; }
    }
}