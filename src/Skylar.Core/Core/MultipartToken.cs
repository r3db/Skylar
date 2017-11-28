using System;
using System.Text;
using Epiphany.Collections.Generic;

namespace Skylar
{
    public sealed class MultipartToken : RangeCollection<Token>
    {
        #region Factory Method

        public static MultipartToken FromToken(Token token)
        {
            return new MultipartToken
            {
                token
            };
        }

        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var item in this)
            {
                sb.AppendFormat("{0} ", item.Content);
            }

            return sb.ToString();
        }

        public Type Type { get; set; }
        
        public object Hint { get; set; }
    }
}