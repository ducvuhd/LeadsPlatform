using System.Text.Json;

namespace DVG.Autoportal.Core.Logging.StaticConfig
{
    public static class JsonSerializerSettings
    {
        public static readonly JsonSerializerOptions CAMEL = new JsonSerializerOptions
        {

            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }
}