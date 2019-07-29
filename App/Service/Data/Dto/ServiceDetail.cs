using together_aspcore.App.Service.Models;

namespace together_aspcore.App.Service.Dto
{
    public class ServiceDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ServiceLimitType LimitType { get; set; }
        public double? Discount { get; set; }
        public DiscountOptions? DiscountOptions { get; set; }
    }
}