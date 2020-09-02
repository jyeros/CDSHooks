using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CDSHooks.Core
{
    public class RegexTemplateLanguage : ITemplateLanguage
    {
        private const string placeHolderPattern = "{{(?<placeHolder>[^{}]+)}}";
        public IEnumerable<string> GetPlaceHolders(string source)
            => Regex.Matches(source, placeHolderPattern)
                .Select(match => match.Groups["placeHolder"].Value).ToList();

        public (IEnumerable<string> notRender, string rendered) Render(string source, IDictionary<string, object> data)
        {
            var notRender = new List<string>();

            var rendered = Regex.Replace(source, placeHolderPattern, match =>
            {
                if (!data.TryGetValue(match.Groups["placeHolder"].Value, out var value))
                {
                    notRender.Add(match.Groups["placeHolder"].Value);
                    return match.Value;
                }
                else
                {
                    return value.ToString();
                }
            });

            return (notRender, rendered);
        }
    }
}
