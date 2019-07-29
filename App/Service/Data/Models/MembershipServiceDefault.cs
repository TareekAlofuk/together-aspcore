using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using together_aspcore.App.Member;

namespace together_aspcore.App.Service.Models
{
    public class MembershipServiceDefault
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public MembershipType MembershipType { get; set; }
        public Service Service { get; set; }

        public double? Count { get; set; }
    }
}