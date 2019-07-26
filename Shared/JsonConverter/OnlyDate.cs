using Newtonsoft.Json.Converters;

namespace together_aspcore.Shared.JsonConverter
{
    public class OnlyDate : IsoDateTimeConverter
    {
        public OnlyDate()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}