using System;
using Newtonsoft.Json;
using together_aspcore.Shared.JsonConverter;

namespace together_aspcore.App.Service.Dto
{
    public class MemberServiceQureyModel
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public string ServiceName { get; set; }

        [JsonConverter(typeof(OnlyDate))]
        public DateTime Time { get; set; }

        public int MemberId { set; get; }
        public int ServiceId { set; get; }
        public string Notes { get; set; }
        public int ReferencePerson { get; set; }

        public double? Price { get; set; }
        public double? Commission { get; set; }
        public double? Discount { get; set; }
        public double? FinalPrice { get; set; }
        public double? Count { get; set; }
    }
}