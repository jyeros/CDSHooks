using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CDSHooks.Core.Models
{
    // TODO: Maybe should not be enum
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum CDSCardActionType
    {
        Delete,
        Create,
        Update
    }
}
