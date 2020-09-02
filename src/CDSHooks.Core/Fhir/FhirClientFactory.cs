using Hl7.Fhir.Rest;

namespace CDSHooks.Core.Fhir
{
    public class FhirClientFactory : IFhirClientFactory
    {
        public FhirClient CreateFhirClient(string fhirServer, string authorization)
        {
            var fhirClient = new FhirClient(fhirServer) { PreferredFormat = ResourceFormat.Json };

            fhirClient.OnBeforeRequest += (sender, e) =>
            {
                e.RawRequest.Headers.Add("Authorization", authorization);
            };
            fhirClient.ParserSettings.AcceptUnknownMembers = true;

            return fhirClient;
        }
    }
}
