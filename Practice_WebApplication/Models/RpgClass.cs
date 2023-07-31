using System.Text.Json.Serialization;

namespace Practice_WebApplication.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,

        Mage = 2,

        Cleric = 3
    }
}
