using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Skylar
{
    public sealed class QueryDefinitionInfo
    {
        public string Target { get; set; }

        public IEnumerable<KeyValuePair<AttributeInfo, AttributeInfo>> Properties { get; set; }
    }

    public sealed class AttributeInfo
    {
        public string Content { get; set; }


        [JsonIgnore]
        public Type Type { get; set; }

        [JsonIgnore]
        public object Hint { get; set; }
    }
}