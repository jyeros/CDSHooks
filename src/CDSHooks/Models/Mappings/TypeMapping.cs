using Hl7.Fhir.Model;
using System;

namespace CDSHooks.Models.Mappings
{
    public static class TypeMapping
    {
        public static string TypeToDisplay(Type type)
        {
            if (ModelInfo.FhirCsTypeToString.TryGetValue(type, out var fhirType))
                return fhirType;
            if (type == typeof(string))
                return "string";
            if (type == typeof(bool))
                return "boolean";
            if (type == typeof(double))
                return "number";
            if (type == typeof(object))
                return "object";
            return type.ToString();
        }
    }
}
