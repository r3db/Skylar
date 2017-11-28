using System;
using System.Collections.Generic;
using System.Text;
using Epiphany.Extensions;
using Skylar.Extensions;

namespace Skylar
{
    // Done!
    public sealed class Parser
    {
        #region Internal Instance Data

        private readonly TokenReader _reader;

        #endregion
        
        #region .Ctor

        public Parser(string content)
        {
            _reader = new TokenReader(content.Trim());
        }

        #endregion

        #region Methods

        internal IList<string> GetTokens()
        {
            var result = new List<string>();
            var sb = new StringBuilder();
            bool isQuote = false;

            while (_reader.CanRead(1))
            {
                if (_reader.Current == '"')
                {
                    isQuote = !isQuote;

                    if (isQuote)
                    {
                        sb.Append(_reader.Current);
                    }
                    else
                    {
                        sb.Append(_reader.Current);
                        result.Add(sb.ToString());
                        sb.Clear();
                    }

                    _reader.Move();
                    continue;
                }

                if (isQuote == false)
                {
                    if (_reader.Current.IsWhiteSpaceOrControl())
                    {
                        string content = sb.ToString();

                        if (content.IsWhiteSpaceOrControl() == false)
                        {
                            result.Add(content);
                        }

                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(_reader.Current);
                    }
                }
                else
                {
                    sb.Append(_reader.Current);
                }

                _reader.Move();
            }

            var remaningToken = sb.ToString();

            if (remaningToken.IsEmpty() == false)
            {
                result.Add(remaningToken);
            }

            return result;
        }

        #endregion
    }
}