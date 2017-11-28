using System;
using System.Collections.Generic;
using System.Linq;

namespace Skylar
{
    internal static class QueryDefinitionToInfoConverter
    {
        internal static QueryDefinitionInfo Convert(QueryDefinition query)
        {
            var result = new QueryDefinitionInfo
            {
                Target = query.Target,
                Properties = query.Properties.Select(x => new KeyValuePair<AttributeInfo, AttributeInfo>(
                    new AttributeInfo
                    {
                        Content = Normalize(string.Join(" ", x.Key.Select(w => w.Content)))
                    }, 
                    new AttributeInfo
                    {
                        Content = Normalize(string.Join(" ", x.Value.Select(w => w.Content))),
                        Type = x.Value.Type,
                        Hint = x.Value.Hint,
                    }))
                    .ToList()
            };

            return result;
        }

        private static string Normalize(string s)
        {
            return s.StartsWith("\"") && s.EndsWith("\"") 
                ? s.Substring(1, s.Length - 2) : 
                s;
        }
    }
}