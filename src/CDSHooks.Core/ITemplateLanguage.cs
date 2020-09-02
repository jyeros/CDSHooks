using System.Collections.Generic;

namespace CDSHooks.Core
{
    public interface ITemplateLanguage
    {
        IEnumerable<string> GetPlaceHolders(string source);
        (IEnumerable<string> notRender, string rendered) Render(string source, IDictionary<string, object> data);
    }
}
