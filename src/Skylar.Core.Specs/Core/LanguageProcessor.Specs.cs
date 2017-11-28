using System;
using System.Collections.Generic;
using System.Linq;
using Axiomatic;
using NUnit.Framework;

namespace Skylar
{
    // Todo: cold work permits in state draft signed in July
    // Todo: Closed Permit signed fifty five years ago
    // Todo: Closed Permi
    // Todo: cold work awaiting authorization Isolations signEd one year ago titled a
    [TestFixture]
    public sealed class LanguageProcessorSpecs
    {
 
        #region Factory .Ctor

        // 01 - Permits in state draft.
        // 02 - Permit created yesterday.
        // 03 - Permits with title "Some Title" signed-by evision and in state "c"
        // 04 - Permit signed
        // 05 - Permit signed.
        // 06 - Permits signed in state xxx.
        // 07 - Permits signed and in state xxx.
        // 08 - Permits signed with state xxx!
        // 09 - Permits titled "Another title" in state draft
        #region Regular

        [Test]
        public void AssertParseMethod001()
        {
            // Arrange
            const string text = @"Permits in state draft.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", 2), Create(TokenKind.None, "draft", 3)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod002()
        {
            // Arrange
            const string text = @"Permit created yesterday.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "created", 1), Create(TokenKind.None, "yesterday", 2)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod003()
        {
            // Arrange
            const string text = @"Permits with title ""Some Title"" signed by evision and in state ""c""";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "title", 2), Create(TokenKind.None, "\"Some Title\"", 3)),
                    new AttributeDefinition(new MultipartToken
                    {
                        CreateToken(TokenKind.None, "signed", 4),
                        CreateToken(TokenKind.Owner, "by", 5),
                    }, Create(TokenKind.None, "evision", 6)),
                    new AttributeDefinition(Create(TokenKind.None, "state", 9), Create(TokenKind.None, "\"c\"", 10)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod004()
        {
            // Arrange
            const string text = @"Permit signed";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "signed", 1), Create(TokenKind.None, "true", -1)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod005()
        {
            // Arrange
            const string text = @"Permit signed.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "signed", 1), Create(TokenKind.None, "true", -1)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod006()
        {
            // Arrange
            const string text = @"Permits signed in state xxx.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "signed", 1), Create(TokenKind.None, "true", -1)),
                    new AttributeDefinition(Create(TokenKind.None, "state", 3), Create(TokenKind.None, "xxx", 4)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod007()
        {
            // Arrange
            const string text = @"Permits signed and in state xxx.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "signed", 1), Create(TokenKind.None, "true", -1)),
                    new AttributeDefinition(Create(TokenKind.None, "state", 4), Create(TokenKind.None, "xxx", 5)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod008()
        {
            // Arrange
            const string text = @"Permits signed with state xxx!";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "signed", 1), Create(TokenKind.None, "true", -1)),
                    new AttributeDefinition(Create(TokenKind.None, "state", 3), Create(TokenKind.None, "xxx", 4)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod009()
        {
            // Arrange
            const string text = @"Permits titled ""Another title"" in state draft";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "titled", 1), Create(TokenKind.None, "\"Another title\"", 2)),
                    new AttributeDefinition(Create(TokenKind.None, "state", 4), Create(TokenKind.None, "draft", 5)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        #endregion

        // 10 - draft permit
        // 11 - draft permit.
        // 12 - draft permits closed.
        // 13 - expired permits created yesterday.
        // 14 - draft permits signed
        // 15 - Draft Permit Signed.
        // 16 - Draft Permit Signed today.
        #region Properties Come First

        [Test]
        public void AssertParseMethod010()
        {
            // Arrange
            const string text = @"draft permit";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), Create(TokenKind.None, "draft", 0)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod011()
        {
            // Arrange
            const string text = @"draft permit.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), Create(TokenKind.None, "draft", 0)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod012()
        {
            // Arrange
            const string text = @"draft permits closed";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), Create(TokenKind.None, "draft", 0)),
                    new AttributeDefinition(Create(TokenKind.None, "closed", 2), Create(TokenKind.None, "true", -1)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod013()
        {
            // Arrange
            const string text = @"expired permits created yesterday.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), Create(TokenKind.None, "expired", 0)),
                    new AttributeDefinition(Create(TokenKind.None, "created", 2), Create(TokenKind.None, "yesterday", 3)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod014()
        {
            // Arrange
            const string text = @"draft permits signEd";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), Create(TokenKind.None, "draft", 0)),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 2), Create(TokenKind.None, "true", -1)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod015()
        {
            // Arrange
            const string text = @"Draft Permit Signed.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), Create(TokenKind.None, "Draft", 0)),
                    new AttributeDefinition(Create(TokenKind.None, "Signed", 2), Create(TokenKind.None, "true", -1)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod016()
        {
            // Arrange
            const string text = @"Draft Permit SiGned today.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), Create(TokenKind.None, "Draft", 0)),
                    new AttributeDefinition(Create(TokenKind.None, "SiGned", 2), Create(TokenKind.None, "today", 3)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        #endregion

        // 17 - Cold work permits.
        // 18 - permits of type Cold work .
        // 19 - coLd woRk Permits signEd.
        #region Multi-Property-Value

        [Test]
        public void AssertParseMethod017()
        {
            // Arrange
            const string text = @"Cold wOrk permits.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "Cold", 0),
                        CreateToken(TokenKind.None, "wOrk", 1),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod018()
        {
            // Arrange
            const string text = @"permits of type Cold worK .";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", 2), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "Cold", 3),
                        CreateToken(TokenKind.None, "worK", 4),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod019()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), Create(TokenKind.None, "true", -1)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        #endregion

        // 20 - coLd woRk Permits signEd.
        // 21 - coLd woRk Permits signEd by John.
        // 22 - coLd woRk Permits signEd at 11:34.
        // 23 - coLd woRk Permits signEd at 15.
        // 24 - coLd woRk Permits signEd at location-x.
        // 25 - coLd woRk Permits signEd 23-12-2015.
        // 26 - coLd woRk Permits signEd 23/12/2015.
        #region Multi-Property-Keys

        [Test]
        public void AssertParseMethod020()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), Create(TokenKind.None, "true", -1)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod021()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd by John.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(new MultipartToken
                    {
                        CreateToken(TokenKind.None, "signEd", 3),
                        CreateToken(TokenKind.Owner, "by", 4),
                    }, Create(TokenKind.None, "John", 5)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod022()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd at 11:34";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(new MultipartToken
                    {
                        CreateToken(TokenKind.None, "signEd", 3),
                        CreateToken(TokenKind.Owner, "at", 4),
                    }, Create(TokenKind.None, "11:34", 5)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod023()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd at 15";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(new MultipartToken
                    {
                        CreateToken(TokenKind.None, "signEd", 3),
                        CreateToken(TokenKind.Owner, "at", 4),
                    }, Create(TokenKind.None, "15", 5)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod024()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd at location-x";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(new MultipartToken
                    {
                        CreateToken(TokenKind.None, "signEd", 3),
                        CreateToken(TokenKind.Owner, "at", 4),
                    }, Create(TokenKind.None, "location-x", 5)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod025()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd 23-12-2015";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), Create(TokenKind.None, "23-12-2015", 4)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod026()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd 23/12/2015";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), Create(TokenKind.None, "23/12/2015", 4)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        #endregion

        // Todo: Date Ranges!
        // 27 - coLd woRk Permits signEd today     .
        // 28 - coLd woRk Permits signEd moments ago.
        // 29 - coLd woRk Permits signEd just now.
        // 30 - coLd woRk Permits signEd yesterday.
        // 31 - coLd woRk Permits signEd last day.
        // 32 - coLd woRk Permits signEd 5 days ago.
        // 33 - coLd woRk Permits signEd five days ago.
        // 34 - awaiting authorization Permits signEd last week.
        // 35 - awaiting authorization isolations signEd 3 weeks ago.
        // 36 - awaiting authorization isolations signEd last month.
        // 37 - awaiting authorization Isolations signEd 8 months ago.
        // 38 - awaiting authorization isolations signEd last year.
        // 39 - awaiting authorization isolations signEd 2 years ago.
        // 40 - awaiting authorization Isolations signEd one year ago.
        // 41 - awaiting authorization Isolations signEd one year ago titled a.
        #region Temporal

        [Test]
        public void AssertParseMethod027()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd today     .";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), Create(TokenKind.None, "today", 4)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod028()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd moments ago.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "moments", 4),
                        CreateToken(TokenKind.None, "ago", 5),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod029()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd just now.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "just", 4),
                        CreateToken(TokenKind.None, "now", 5),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod030()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd yesterday.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), Create(TokenKind.None, "yesterday", 4)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod031()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd last day.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "last", 4),
                        CreateToken(TokenKind.None, "day", 5),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod032()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd 5 days ago.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "5", 4),
                        CreateToken(TokenKind.None, "days", 5),
                        CreateToken(TokenKind.None, "ago", 6),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod033()
        {
            // Arrange
            const string text = @"coLd woRk Permits signEd five days ago.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "type", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "coLd", 0),
                        CreateToken(TokenKind.None, "woRk", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "five", 4),
                        CreateToken(TokenKind.None, "days", 5),
                        CreateToken(TokenKind.None, "ago", 6),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod034()
        {
            // Arrange
            const string text = @"awaiting authorization Permits signEd last week.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "last", 4),
                        CreateToken(TokenKind.None, "week", 5),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod035()
        {
            // Arrange
            const string text = @"awaiting authorization isolations signEd 3 weeks ago.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "isolation",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "3", 4),
                        CreateToken(TokenKind.None, "weeks", 5),
                        CreateToken(TokenKind.None, "ago", 6),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod036()
        {
            // Arrange
            const string text = @"awaiting authorization isolations signEd last month.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "isolation",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "last", 4),
                        CreateToken(TokenKind.None, "month", 5),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod037()
        {
            // Arrange
            const string text = @"awaiting authorization Isolations signEd 8 months ago.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "isolation",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "8", 4),
                        CreateToken(TokenKind.None, "months", 5),
                        CreateToken(TokenKind.None, "ago", 6),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod038()
        {
            // Arrange
            const string text = @"awaiting authorization isolations signEd last year.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "isolation",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "last", 4),
                        CreateToken(TokenKind.None, "year", 5),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod039()
        {
            // Arrange
            const string text = @"awaiting authorization isolations signEd 2 years ago.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "isolation",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "2", 4),
                        CreateToken(TokenKind.None, "years", 5),
                        CreateToken(TokenKind.None, "ago", 6),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod040()
        {
            // Arrange
            const string text = @"awaiting authorization Isolations signEd one year ago.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "isolation",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "one", 4),
                        CreateToken(TokenKind.None, "year", 5),
                        CreateToken(TokenKind.None, "ago", 6),
                    }),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        [Test]
        public void AssertParseMethod041()
        {
            // Arrange
            const string text = @"awaiting authorization Isolations signEd one year ago titled a.";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "isolation",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "signEd", 3), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "one", 4),
                        CreateToken(TokenKind.None, "year", 5),
                        CreateToken(TokenKind.None, "ago", 6),
                    }),
                    new AttributeDefinition(Create(TokenKind.None, "titled", 7), Create(TokenKind.None, "a", 8)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        #endregion

        // 42 - Awaiting authorization permits with work Description "Some description".
        #region Multi-Property-Value Defined aka Clr-Properties!

        [Test]
        public void AssertParseMethod042()
        {
            // Arrange
            const string text = @"Awaiting authorization permits with work Description ""Some description"".";

            var sentences = SentenceParser.Parse(new Parser(text));

            // Act
            var actual = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            // Assert
            var expected = new QueryDefinition
            {
                Target = "permit",
                Properties = new List<AttributeDefinition>
                {
                    new AttributeDefinition(Create(TokenKind.None, "state", -1), new MultipartToken
                    {
                        CreateToken(TokenKind.None, "Awaiting", 0),
                        CreateToken(TokenKind.None, "authorization", 1),
                    }),
                    new AttributeDefinition(new MultipartToken
                    {
                        CreateToken(TokenKind.None, "work", 4),
                        CreateToken(TokenKind.None, "Description", 5),
                    }, Create(TokenKind.None, "\"Some description\"", 6)),
                },
            };

            Axiom.AssertAreEqual(expected, actual);
        }

        #endregion

        #endregion

        #region Helpers

        private static MultipartToken Create(TokenKind kind, string content, int index)
        {
            return MultipartToken.FromToken(new Token
            {
                Content = content,
                Index = index,
                Kind = kind
            }); 
        }

        private static Token CreateToken(TokenKind kind, string content, int index)
        {
            return new Token
            {
                Content = content,
                Index = index,
                Kind = kind
            };
        }

        #endregion
    }
}