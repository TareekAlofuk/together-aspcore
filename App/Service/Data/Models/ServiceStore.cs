using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using together_aspcore.Shared.JsonConverter;

namespace together_aspcore.App.Service.Models
{
    public class ServiceStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Service Service { get; set; }

        [Required]
        public Member.Models.Member Member { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [JsonConverter(typeof(OnlyDate))]
        public DateTime ExpirationDate { get; set; }
    }
}