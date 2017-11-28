using System;
using System.Collections.Generic;

namespace Skylar
{
    public sealed class QueryDefinition
    {
        #region .Ctor

        internal QueryDefinition()
        {
            Properties = new List<AttributeDefinition>();
        }

        #endregion

        #region Auto Implemented Properties

        public string Target { get; set; }

        public IList<AttributeDefinition> Properties { get; set; }

        #endregion
    }
}