using Newtonsoft.Json.Converters;

namespace together_aspcore.Shared.JsonConverter
{
    public class DateTimeJsonFormat : IsoDateTimeConverter
    {
        public DateTimeJsonFormat()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm";
        }
    }
}