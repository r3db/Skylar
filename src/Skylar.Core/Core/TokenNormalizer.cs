using System;
using System.Collections.Generic;

namespace Skylar
{
    internal static class TokenNormalizer
    {
        #region Internal Static Data

        private static readonly ISet<string> _punctuation = new HashSet<string>
        {
            ".",
            ",",
            ";",
            "!",
            "?",
        };

        private static readonly IDictionary<string, string> _spelledNumber = new Dictionary<string, string>
        {
            { "zero",         "0"   },
            { "one",          "1"   },
            { "two",          "2"   },
            { "three",        "3"   },
            { "four",         "4"   },
            { "five",         "5"   },
            { "six",          "6"   },
            { "seven",        "7"   },
            { "eight",        "8"   },
            { "nine",         "9"   },
            { "ten",          "10"  },
            { "eleven",       "11"  },
            { "twelve",       "12"  },
            { "thirteen",     "13"  },
            { "fourteen",     "14"  },
            { "fifteen",      "15"  },
            { "sixteen",      "16"  },
            { "seventeen",    "17"  },
            { "eighteen",     "18"  },
            { "nineteentwen", "19"  },
            { "twenty",       "20"  },
            { "thirty",       "30"  },
            { "forty",        "40"  },
            { "fourty",       "40"  },
            { "fifty",        "50"  },
            { "sixty",        "60"  },
            { "seventy",      "70"  },
            { "eighty",       "80"  },
            { "ninety",       "90"  },
            { "hundred",      "100" },
        };

        #endregion

        #region Methods

        internal static string NormalizeLemmaFull(string lemma)
        {
            var normalizedLemma = lemma.ToLowerInvariant();

            foreach (var item in _punctuation)
            {
                normalizedLemma = normalizedLemma.Replace(item, string.Empty);
            }

            if (normalizedLemma.EndsWith("'s"))
            {
                int index = normalizedLemma.LastIndexOf("'s", StringComparison.Ordinal);
                normalizedLemma = normalizedLemma.Substring(0, index);
            }

            if (normalizedLemma.EndsWith("s"))
            {
                int index = normalizedLemma.LastIndexOf("s", StringComparison.Ordinal);
                normalizedLemma = normalizedLemma.Substring(0, index);
            }

            if (normalizedLemma.EndsWith("ed"))
            {
                int index = normalizedLemma.LastIndexOf("ed", StringComparison.Ordinal);
                normalizedLemma = normalizedLemma.Substring(0, index + 1);
            }

            return normalizedLemma;
        }

        internal static string NormalizeLemmaPartial(string lemma)
        {
            var normalizedLemma = lemma.ToLowerInvariant();

            normalizedLemma = NormalizeLemmaNumber(normalizedLemma);
            normalizedLemma = RemovePunctuation(normalizedLemma);

            if (normalizedLemma.EndsWith("'s"))
            {
                int index = normalizedLemma.LastIndexOf("'s", StringComparison.Ordinal);
                normalizedLemma = normalizedLemma.Substring(0, index);
            }

            if (normalizedLemma.EndsWith("s"))
            {
                int index = normalizedLemma.LastIndexOf("s", StringComparison.Ordinal);
                normalizedLemma = normalizedLemma.Substring(0, index);
            }

            return normalizedLemma;
        }

        internal static string CaseNormalizeLemma(string lemma)
        {
            return lemma[0] + NormalizeLemmaFull(lemma).Substring(1);
        }

        internal static string RemovePunctuation(string normalizedLemma)
        {
            var result = normalizedLemma;

            foreach (var item in _punctuation)
            {
                result = result.Replace(item, string.Empty);
            }

            return result;
        }

        internal static bool EndsWithPunctuation(string s)
        {
            foreach (var item in _punctuation)
            {
                if (s.EndsWith(item))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        public static string NormalizeLemmaNumber(string content)
        {
            var result = content;

            foreach (var item in _spelledNumber)
            {
                result = result.Replace(item.Key, item.Value);
            }

            return result;
        }
    }
}