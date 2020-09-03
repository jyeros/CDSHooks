using CDSHooks.Core.Fhir;
using CDSHooks.Core.Models;
using CDSHooks.Domain;
using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDSHooks.Core.PrefetchTemplate
{
    public class PrefetchTemplateRender : IPrefetchTemplateRender
    {
        private readonly ITemplateLanguage templateLanguage;
        private readonly IFhirClientFactory fhirClientFactory;

        public PrefetchTemplateRender(ITemplateLanguage templateLanguage, IFhirClientFactory fhirClientFactory)
        {
            this.templateLanguage = templateLanguage;
            this.fhirClientFactory = fhirClientFactory;
        }
        public async Task<(OperationOutcome outcome, Dictionary<string, Resource> prefetch)>
            Resolve(IDictionary<string, Resource> prefetchProvided,
                IDictionary<string, string> prefetchToResolve, IDictionary<string, object> context,
                Dictionary<string, HookContext> hookContext,
                string fhirServer, ExecuteServiceRequestFhirAuthorization fhirAuthorization)
        {
            var prefetchResolved = new Dictionary<string, Resource>();
            var prefetchProvidedClone = new Dictionary<string, Resource>(prefetchProvided ?? new Dictionary<string, Resource>());
            var errors = new List<OperationOutcome.IssueComponent>();

            var prefetchTokens = PrefetchTokenBuilder.Build(hookContext, context);

            var queries = new List<(string key, string query)>();

            foreach (var prefetch in prefetchToResolve ?? new Dictionary<string, string>())
            {
                if (prefetchProvidedClone.TryGetValue(prefetch.Key, out var value))
                {
                    prefetchResolved.Add(prefetch.Key, value);
                    prefetchProvidedClone.Remove(prefetch.Key);
                }
                else
                {
                    var (notRendered, templateRendered) = templateLanguage.Render(prefetch.Value, prefetchTokens);

                    errors.AddRange(notRendered.Select(placeHolder => new OperationOutcome.IssueComponent
                    {
                        Severity = OperationOutcome.IssueSeverity.Fatal,
                        Code = OperationOutcome.IssueType.Invalid,
                        Diagnostics = $"Prefetch Template render error at prefetch with key {prefetch.Key}: placeHolder {placeHolder} not found"
                    }));

                    queries.Add((prefetch.Key, templateRendered));
                }
            }

            if (prefetchProvidedClone.Count > 0)
            {
                errors.AddRange(prefetchProvidedClone.Select(item => new OperationOutcome.IssueComponent
                {
                    Severity = OperationOutcome.IssueSeverity.Fatal,
                    Code = OperationOutcome.IssueType.Invalid,
                    Diagnostics = $"Extra property: prefetch.{item.Key}"
                }));
            }

            if (queries.Count > 0)
            {
                if (fhirAuthorization == null || fhirServer == null)
                {
                    errors.Add(new OperationOutcome.IssueComponent
                    {
                        Severity = OperationOutcome.IssueSeverity.Fatal,
                        Code = OperationOutcome.IssueType.Invalid,
                        Diagnostics = $"Not all prefetch provided and can't access to fhir server, fhirAuthorization and fhirServer are needed"
                    });
                }
                else
                {
                    var authorization = $"{fhirAuthorization.TokenType} {fhirAuthorization.AccessToken}";
                    var client = fhirClientFactory.CreateFhirClient(fhirServer, authorization);
                    // TODO: Make batch instead many get
                    foreach (var (key, query) in queries)
                    {
                        var resource = await client.GetAsync(query);
                        prefetchResolved.Add(key, resource);
                    }
                }
            }

            return (errors.Count > 0 ? new OperationOutcome { Issue = errors } : null, prefetchResolved);
        }
    }
}
