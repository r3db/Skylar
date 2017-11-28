using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Epiphany.Extensions;

namespace Skylar
{
    public sealed class LanguageProcessor
    {
        #region Internal Static Data

        // Todo: Introduce Seconds and minutes and hours!
        private static readonly IDictionary<string, Tuple<bool, Func<int, TimeSpan>>> _temporal = new Dictionary<string, Tuple<bool, Func<int, TimeSpan>>>
        {
            { @"today",                        new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromDays(0))      },
            { @"moments ago",                  new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromMinutes(2))   },
            { @"moment ago",                   new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromMinutes(2))   },
            { @"just now",                     new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromMinutes(2))   },
            { @"yesterday",                    new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromDays(1))      },
            { @"last day",                     new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromDays(1))      },
            { @"(?<Number>\d+) day[s]? ago",   new Tuple<bool, Func<int, TimeSpan>>(true,  x => TimeSpan.FromDays(x))      },
            { @"last week",                    new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromDays(7))      },
            { @"(?<Number>\d+) week[s]? ago",  new Tuple<bool, Func<int, TimeSpan>>(true, x => TimeSpan.FromDays(7 * x))   },
            { @"last month",                   new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromDays(30))     },
            { @"(?<Number>\d+) month[s]? ago", new Tuple<bool, Func<int, TimeSpan>>(true, x => TimeSpan.FromDays(30 * x))  },
            { @"last year",                    new Tuple<bool, Func<int, TimeSpan>>(false, x => TimeSpan.FromDays(365))    },
            { @"(?<Number>\d+) year[s]? ago",  new Tuple<bool, Func<int, TimeSpan>>(true, x => TimeSpan.FromDays(365 * x)) },
        };

        #endregion

        #region Internal Instance Data

        private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();

        #endregion

        #region .Ctor

        // ReSharper disable once ParameterTypeCanBeEnumerable.Local
        public LanguageProcessor(IDictionary<string, IEnumerable<string>> properties)
        {
            foreach (var kvp in properties)
            {
                foreach (var value in kvp.Value)
                {
                    _properties.Add(new KeyValuePair<string, string>(value, kvp.Key));
                }
            }
        }

        #endregion

        #region Factory .Ctor

        public QueryDefinition Parse(IList<Token> tokens)
        {
            var result = new QueryDefinition();
            var lemma = tokens[0];
            var startIndex = 1;

            bool found = false;

            // Note: Adjective comes first!
            if (tokens.Count > 1 && tokens[0].Kind == TokenKind.None)
            {
                //startIndex++;

                int index = 0;
                var attributeDefinition = FindNextAttributeDefinition(tokens, ref index);

                if (attributeDefinition != null)
                {
                    result.Properties.Add(attributeDefinition);
                    lemma = tokens[index + 1];
                    startIndex = Math.Max(attributeDefinition.Key.Max(x => x.Index), attributeDefinition.Value.Max(x => x.Index)) + 2;
                    found = true;
                }

                if (found == false)
                {
                    if (_properties.ContainsKey(tokens[0].Content.ToLowerInvariant()))
                    {
                        result.Properties.Add(new AttributeDefinition(MultipartToken.FromToken(new Token
                        {
                            Kind = TokenKind.None,
                            Content = _properties[tokens[0].Content.ToLowerInvariant()]
                        }),
                            MultipartToken.FromToken(new Token
                            {
                                Kind = TokenKind.None,
                                Content = tokens[0].Content.ToLowerInvariant()
                            })
                            ));

                    }
                    else
                    {
                        // Todo: Determine if it's a bool property or if it's non-bool property!
                        result.Properties.Add(new AttributeDefinition(MultipartToken.FromToken(new Token
                        {
                            Kind = TokenKind.None,
                            Content = tokens[0].Content
                        })));
                    }
                }
            }

            // Note: Regular Parsing -> Propery - Value Pair!
            if (lemma.Kind == TokenKind.Keyword)
            {
                // Note: Now that we have a keyword find the next two tokens! If we can!
                var index = startIndex;
                var propertyValuePair = FindNextAttributeDefinition(tokens, ref index);

                result.Target = TokenNormalizer.CaseNormalizeLemma(lemma.Content).ToLowerInvariant();

                while (propertyValuePair != null)
                {
                    result.Properties.Add(propertyValuePair);
                    // Todo: We should be counting, instead of always adding one!!
                    index += 1;
                    propertyValuePair = FindNextAttributeDefinition(tokens, ref index);
                }
            }

            return result;
        }

        #endregion

        #region Helpers

        private static int GetMaxCount(IEnumerable<string> dictionary)
        {
            int max = 0;

            foreach (var item in dictionary)
            {
                var count = item.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;

                if (count > max)
                {
                    max = count;
                }
            }

            return max;
        }

        private AttributeDefinition FindNextAttributeDefinition(IList<Token> tokens, ref int index)
        {
            for (int i = index; i < tokens.Count - 1; i++)
            {
                var leftToken = MatchTokenAsGreedy(tokens, i);

                if (leftToken == null)
                {
                    continue;
                }

                index = leftToken.Match.Max(x => x.Index);
                var rightToken = MatchTokenAsGreedy(tokens, index + 1);

                if (rightToken == null)
                {
                    if (leftToken.Type == null)
                    {
                        return new AttributeDefinition(leftToken.Match, MultipartToken.FromToken(new Token
                        {
                            Content = "true",
                            Index = -1,
                            Kind = TokenKind.None
                        }));
                    }

                    return new AttributeDefinition(MultipartToken.FromToken(leftToken.Type), leftToken.Match);
                }

                index = rightToken.Match.Max(x => x.Index);
                return new AttributeDefinition(leftToken.Match, rightToken.Match);
            }

            if (tokens.Count - index == 1 && tokens[index].Kind == TokenKind.None)
            {
                // Todo: Should we check if it's a verb?!
                return new AttributeDefinition(MultipartToken.FromToken(tokens[index]));
            }

            return null;
        }

        #endregion

        #region Next Stage

        private TokenMatch MatchTokenAsGreedy(IList<Token> tokens, int index)
        {
            foreach (var item in CollectAllOfKind(tokens, TokenKind.None, index).Concat(CollectAllOfKind2(tokens, TokenKind.None, index)))
            {
                if (item.IsEmpty())
                {
                    continue;
                }

                var s1 = string.Join(" ", item.Select(x => x.NormalizedContentFull));
                var s2 = string.Join(" ", item.Select(x => x.NormalizedContentPartial));
                var s3 = string.Join(" ", item.Select(x => x.NormalizedContentNumber));
                var s4 = string.Join(" ", item.Select(x => x.Content));

                if (_properties.ContainsKey(s1))
                {
                    var token = new Token
                    {
                        Content = _properties[s1],
                        Index = -1,
                        Kind = TokenKind.None
                    };

                    return new TokenMatch(item, token);
                }

                if (_properties.ContainsKey(s2))
                {
                    var token = new Token
                    {
                        Content = _properties[s2],
                        Index = -1,
                        Kind = TokenKind.None
                    };

                    return new TokenMatch(item, token);
                }

                if (_temporal.ContainsKey(s1))
                {
                    var token = new Token
                    {
                        Content = s1,
                        Index = -1,
                        Kind = TokenKind.None
                    };

                    return new TokenMatch(item, token);
                }

                if (_temporal.ContainsKey(s2))
                {
                    var token = new Token
                    {
                        Content = s2,
                        Index = -1,
                        Kind = TokenKind.None
                    };

                    return new TokenMatch(item, token);
                }

                // Todo: Rename!
                foreach (var rename in _temporal.Where(x => x.Value.Item1))
                {
                    var regex = new Regex(rename.Key);

                    if (regex.IsMatch(s1))
                    {
                        var group = regex.Match(s1).Groups["Number"];

                        var token = new Token
                        {
                            Content = s1,
                            Index = -1,
                            Kind = TokenKind.None
                        };

                        item.Type = typeof(TimeSpan);
                        item.Hint = rename.Value.Item2.Invoke(int.Parse(group.Value));

                        return new TokenMatch(item, token);
                    }

                    if (regex.IsMatch(s2))
                    {
                        var group = regex.Match(s2).Groups["Number"];

                        var token = new Token
                        {
                            Content = s2,
                            Index = -1,
                            Kind = TokenKind.None,
                        };

                        item.Type = typeof(TimeSpan);
                        item.Hint = rename.Value.Item2.Invoke(int.Parse(group.Value));

                        return new TokenMatch(item, token);
                    }

                    if (regex.IsMatch(s3))
                    {
                        var group = regex.Match(s3).Groups["Number"];

                        var token = new Token
                        {
                            Content = s3,
                            Index = -1,
                            Kind = TokenKind.None
                        };

                        item.Type = typeof(TimeSpan);
                        item.Hint = rename.Value.Item2.Invoke(int.Parse(group.Value));

                        return new TokenMatch(item, token);
                    }

                    if (regex.IsMatch(s4))
                    {
                        var group = regex.Match(s4).Groups["Number"];

                        var token = new Token
                        {
                            Content = s4,
                            Index = -1,
                            Kind = TokenKind.None
                        };

                        item.Type = typeof(TimeSpan);
                        item.Hint = rename.Value.Item2.Invoke(int.Parse(group.Value));

                        return new TokenMatch(item, token);
                    }
                }

                if (item.Count == 3 && item[1].Kind == TokenKind.Owner)
                {
                    return new TokenMatch(new MultipartToken
                    {
                        item[0],
                        item[1],
                    });
                }
            }

            return tokens[index].Kind == TokenKind.None
                ? new TokenMatch(MultipartToken.FromToken(tokens[index]))
                : null;
        }

        private IEnumerable<MultipartToken> CollectAllOfKind(IList<Token> tokens, TokenKind kind, int startIndex)
        {
            var max = GetMaxCount(_properties.Keys.ToList());
            var result = new List<MultipartToken>();

            for (int i = 0; i < max; i++)
            {
                result.Add(CollectOfKind(tokens, kind, max - i, startIndex));
            }

            var d = new HashSet<string>();
            var r = new List<MultipartToken>();

            foreach (var item in result)
            {
                var s = string.Join(string.Empty, item.Select(x => x.NormalizedContentFull));

                if (d.Contains(s) == false)
                {
                    d.Add(s);
                    r.Add(item);
                }
            }

            return r;
        }

        private IEnumerable<MultipartToken> CollectAllOfKind2(IList<Token> tokens, TokenKind kind, int startIndex)
        {
            var max = GetMaxCount(_temporal.Keys.ToList());
            var result = new List<MultipartToken>();

            for (int i = 0; i < max; i++)
            {
                result.Add(CollectOfKind(tokens, kind, max - i, startIndex));
            }

            var d = new HashSet<string>();
            var r = new List<MultipartToken>();

            foreach (var item in result)
            {
                var s = string.Join(string.Empty, item.Select(x => x.NormalizedContentFull));

                if (d.Contains(s) == false)
                {
                    d.Add(s);
                    r.Add(item);
                }
            }

            return r;
        }

        private static MultipartToken CollectOfKind(IList<Token> tokens, TokenKind kind, int tokenCount, int startIndex)
        {
            var result = new MultipartToken();
            int excess = 0;

            for (int i = startIndex; i < tokens.Count; i++)
            {
                var token = tokens[i];

                if (token.Kind == kind || token.Kind == TokenKind.Keyword)
                {
                    result.Add(token);

                    if (result.Count == (tokenCount + excess))
                    {
                        break;
                    }
                }
                else if (token.Kind == TokenKind.Owner)
                {
                    if (result.Count == 1)
                    {
                        result.Add(token);
                        excess++;
                    }
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        #endregion
    }
}