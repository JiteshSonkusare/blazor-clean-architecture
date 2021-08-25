
using Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}