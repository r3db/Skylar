using System;
using System.Linq;
using Epiphany.Extensions;

namespace Skylar
{
    public sealed class AttributeDefinition
    {
        #region .Ctor

        internal AttributeDefinition(MultipartToken key)
            : this(key, MultipartToken.FromToken(new Token
            {
                Content = "true", 
                Index = -1, 
                Kind = TokenKind.None
            }))
        {
        }

        internal AttributeDefinition(MultipartToken key, MultipartToken value)
        {
            Key = key;
            Value = value;
        }

        #endregion

        #region Auto Implemented Properties

        public MultipartToken Key { get; private set; }

        public MultipartToken Value { get; private set; }

        #endregion

        #region Base Method Overrides

        public override string ToString()
        {
            return "Key: {0}, Value: {1}".ToInvariantFormat(
                string.Join(":", Key.Select(x => x.NormalizedContentFull)),
                string.Join(":", Value.Select(x => x.NormalizedContentFull)));
        }

        #endregion
    }
}