using Hl7.Fhir.Rest;

namespace CDSHooks.Core.Fhir
{
    public interface IFhirClientFactory
    {
        FhirClient CreateFhirClient(string fhirServer, string authorization);
    }
}
