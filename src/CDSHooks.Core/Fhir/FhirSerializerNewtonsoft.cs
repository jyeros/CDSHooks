using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;
using System;

namespace CDSHooks.Core.Fhir
{
    public class FhirSerializerNewtonsoft : JsonConverter
    {
        private readonly FhirJsonSerializer fhirJsonSerializer;
        private readonly FhirJsonParser fhirJsonParser;
        public FhirSerializerNewtonsoft()
        {
            fhirJsonSerializer = new FhirJsonSerializer();
            fhirJsonParser = new FhirJsonParser();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => fhirJsonSerializer.Serialize(value as Base, writer);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => fhirJsonParser.Parse(reader, objectType);

        public override bool CanConvert(Type objectType)
            => typeof(Base).IsAssignableFrom(objectType);
    }
}
