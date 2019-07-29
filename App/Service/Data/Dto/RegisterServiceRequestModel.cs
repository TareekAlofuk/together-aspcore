using together_aspcore.App.Service.Models;

namespace together_aspcore.App.Service.Dto
{
    public class RegisterServiceRequestModel
    {
        public int ServiceId { get; set; }
        public int MemberId { get; set; }
        public double? Price { get; set; }
        public double? Commission { get; set; }
        public double? Discount { get; set; }
        public double? Count { get; set; }
        public double? FinalPrice { get; set; }
        public string Notes { get; set; }

        public ServiceUsage toServiceUsage()
        {
            var serviceUsage = new ServiceUsage
            {
                ServiceId =  ServiceId,
                MemberId = MemberId,
                Price = Price,
                Commission = Commission,
                Discount = Discount,
                FinalPrice = FinalPrice,
                Count = Count,
                Notes = Notes
            };

            return serviceUsage;
        }
    }
}