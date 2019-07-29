using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using together_aspcore.App.Member;

namespace together_aspcore.App.Service.Models
{
    public class ServiceRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Service Service { get; set; }

        public MembershipType MembershipType { get; set; }

        public ServiceLimitType LimitType { get; set; }
        public double? Discount { get; set; }
        public DiscountOptions? DiscountOptions { get; set; }
    }
}