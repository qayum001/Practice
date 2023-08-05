using System.Text.Json.Serialization;

namespace Practice.Data.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male,
        Female
    }
}
