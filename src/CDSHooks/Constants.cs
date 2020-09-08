using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CDSHooks
{
    public static class Constants
    {
        public static IDictionary<string, string> allowedTypes = new []
            {
                (typeof(string), "string"),
                (typeof(bool), "boolean" ),
                (typeof(double), "number"),
                (typeof(object), "object")
            }.Concat(ModelInfo.SupportedResources
                .Select(resourceName => (ModelInfo.FhirTypeToCsType[resourceName], resourceName)))
            .ToDictionary(x => x.Item1.FullName, x => x.Item2);

        public static HashSet<Type> allowedPrefetchTokenTypes = new HashSet<Type>
        {
             typeof(string),
             typeof(double),
        };
    }
}
