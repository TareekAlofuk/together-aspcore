using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using together_aspcore.Shared.JsonConverter;

namespace together_aspcore.App.Service.Models
{
    public class ServiceUsage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int MemberId { get; set; }
        
        [Column(TypeName = "Date")]
        [JsonConverter(typeof(OnlyDate))]
        public DateTime? ExpirationDate { get; set; }


        [Required]
        public DateTime Time { get; set; }

        public string Notes { get; set; }

        public int ReferencePerson { get; set; }

        public double? Price { get; set; }
        public double? Commission { get; set; }
        public double? Discount { get; set; }
        public double? FinalPrice { get; set; }
        public double? Count { get; set; }
    }
}