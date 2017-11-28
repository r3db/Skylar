using System;
using System.Collections.Generic;
using Epiphany.Extensions;

namespace Skylar
{
    public static class SentenceParser
    {
        private static readonly TokenClassifier _classifier = new TokenClassifier(new HashSet<string>
        {
            "permit",
            "isolation",
        });

        #region Methods

        public static IEnumerable<IList<Token>> Parse(Parser parser)
        {
            var result = new List<IList<Token>>();
            var sentence = new List<Token>();
            var tokens = parser.GetTokens();

            for (int i = 0; i < tokens.Count; i++)
            {
                var item = tokens[i];
                var token = _classifier.Classify(item);

                token.Index = i;

                if (string.IsNullOrEmpty(token.Content) == false)
                {
                    sentence.Add(token);
                }

                if (TokenNormalizer.EndsWithPunctuation(item))
                {
                    result.Add(sentence);
                    sentence = new List<Token>();
                }
            }

            if (sentence.IsEmpty() == false)
            {
                result.Add(sentence);
            }

            return result;
        }

        #endregion
    }
}