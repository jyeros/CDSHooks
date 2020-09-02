using CDSHooks.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CDSHooks.Core.PrefetchTemplate
{
    public static class PrefetchTokenBuilder
    {
        public static IDictionary<string, object> Build(Dictionary<string, HookContext> hookContext,
            IDictionary<string, object> context)
        {
            return context.Where(x => hookContext.TryGetValue(x.Key, out var contextDefinition)
                        && contextDefinition.IsPrefetchToken)
                .ToDictionary(x => $"context.{x.Key}", x => x.Value);
        }
    }
}
