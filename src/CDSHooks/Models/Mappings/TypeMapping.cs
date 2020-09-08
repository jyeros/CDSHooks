using Hl7.Fhir.Model;
using System;

namespace CDSHooks.Models.Mappings
{
    public static class TypeMapping
    {
        public static string TypeToDisplay(Type type)
        {
            var singleType = type.IsArray ? type.GetElementType() : type;
            var displayType = singleType.ToString();
            if (ModelInfo.FhirCsTypeToString.TryGetValue(singleType, out var fhirType))
                displayType = fhirType;
            else if (singleType == typeof(string))
                displayType = "string";
            else if (singleType == typeof(bool))
                displayType = "boolean";
            else if (singleType == typeof(double))
                displayType = "number";
            else if (singleType == typeof(object))
                displayType = "object";
            return $"{displayType}{(type.IsArray ? "[]" : "")}";
        }
    }
}
