using System;
using System.Collections.Generic;

namespace Skylar
{
    // Todo: Remove from here, its client specific configuration!
    public static class EvisionLanguageProcessor
    {
        #region Internal Static Data

        private static readonly LanguageProcessor _processor = new LanguageProcessor(new Dictionary<string, IEnumerable<string>>
        {
            {
                "state", new[]
                {
                    "draft",
                    "authorized",
                    "awaiting authorization",
                    "awaiting issue",
                    "closed",
                    "closing",
                    "closing (without wo)",
                    "expired",
                    "finalizing jha",
                    "overdue (awaiting suspension)",
                }
            },
            {
                "type", new[]
                {
                    "cold work",
                    "spark potential",
                    "hot work",
                    "radiography",
                    "driver permit",
                }
            },
            {
                "clr-property", new[]
                {
                    "work description",
                    "tag number",
                    "reason for isolation",
                    "long term isolation",
                }
            }
        });

        #endregion

        #region Properties

        public static LanguageProcessor Processor
        {
            get { return _processor; }
        }

        #endregion
    }
}