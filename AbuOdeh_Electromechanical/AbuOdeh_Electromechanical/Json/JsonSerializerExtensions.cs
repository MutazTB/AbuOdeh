using System.Text.Json.Serialization;
using System.Text.Json;

namespace AbuOdeh_Electromechanical.Json
{
    public static class JsonSerializerExtensions
    {
        private static readonly JsonSerializerOptions _defaultOptions;

        static JsonSerializerExtensions()
        {
            DefaultConverters = new JsonConverter[]
            {
                new JsonStringEnumConverter(),
                new DateOnlyJsonConverter(),
                new NullableDateOnlyJsonConverter(),
                new TimeOnlyJsonConverter(),
                new NullableTimeOnlyJsonConverter()
            };
            _defaultOptions = new JsonSerializerOptions();
            DefaultConverters.ForEach(_defaultOptions.Converters.Add);
            _defaultOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        }

        public static IEnumerable<JsonConverter> DefaultConverters { get; }

        public static string SerializeWithDefaultOptions(object value)
        {
            if (value == null)
                return null;

            return JsonSerializer.Serialize(value, value.GetType(), _defaultOptions);
        }

        public static T DeserializeWithDefaultOptions<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _defaultOptions);
        }
    }
}
