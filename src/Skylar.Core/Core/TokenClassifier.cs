using System;
using System.Collections.Generic;

namespace Skylar
{
    internal sealed class TokenClassifier
    {
        #region Internal Static Data

        // It could be multiple things at once, ambiguity has to be resolved at a later point!
        private static readonly IDictionary<string, TokenKind> _words = new Dictionary<string, TokenKind>
        {
            { "in",   TokenKind.Connector },
            { "with", TokenKind.Connector },
            { "and",  TokenKind.Connector },
            // Todo: These last two are not connectors, they are prepositions!
            { "from", TokenKind.Connector },
            { "of",   TokenKind.Connector },
            { "by",   TokenKind.Owner     },
            { "at",   TokenKind.Owner     },
        };

        #endregion

        #region Internal Instance Data

        private readonly ISet<string> _keywords;

        #endregion

        #region .Ctor

        internal TokenClassifier(ISet<string> keywords)
        {
            _keywords = keywords;
        }

        #endregion

        #region Methods

        internal Token Classify(string token)
        {
            var normalizedLemma = TokenNormalizer.NormalizeLemmaFull(token);

            if (_keywords.Contains(normalizedLemma))
            {
                return new Token
                {
                    Content = token,
                    Kind = TokenKind.Keyword
                };
            }

            if (_words.ContainsKey(normalizedLemma))
            {
                return new Token
                {
                    Content = token,
                    Kind = _words[normalizedLemma]
                };
            }

            return new Token
            {
                Content = TokenNormalizer.RemovePunctuation(token),
                Kind = TokenKind.None
            };
        }

        #endregion
    }
}