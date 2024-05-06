using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace BuildWise.Provider
{
    public static class JsonProvider
    {
        public static JsonSerializerSettings GetNewtonSoft(bool supressError = false)
        {
            JsonSerializerSettings settings = new();
            SetNewtonSoft(ref settings, supressError);
            return settings;
        }

        public static void SetNewtonSoft(ref JsonSerializerSettings SerializerSettings, bool supressError = false)
        {
#pragma warning disable S125 // Sections of code should not be commented out
            //Returns enums as string and also in payload example
            //SerializerSettings.Converters.Add(new StringEnumConverter());
#pragma warning restore S125 // Sections of code should not be commented out
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.Formatting = Formatting.Indented;
            SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = true,
                }
            };

            if (supressError is true)
            {
                SerializerSettings.Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)//To avoid exception on deserialization error
                {
                    args.ErrorContext.Handled = true;
                };
            }
        }
    }
}
