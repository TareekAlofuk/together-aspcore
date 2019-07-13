using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace together_aspcore.App.Member
{
    public class MemberFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Path { get; set; }
        public Member Member { get; set; }


    }
}
