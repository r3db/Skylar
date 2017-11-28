using System;
using System.Diagnostics;

namespace Skylar
{
    [DebuggerDisplay("{Content}")]
    public sealed class Token
    {
        #region Auto Implemented Properties

        internal int Index { get; set; }

        internal TokenKind Kind { get; set; }

        public string Content { get; set; }

        #endregion

        #region Properties

        internal string NormalizedContentFull
        {
            get
            {
                return TokenNormalizer.NormalizeLemmaFull(Content);
            }
        }

        public string NormalizedContentPartial
        {
            get
            {
                return TokenNormalizer.NormalizeLemmaPartial(Content);
            }
        }

        public string NormalizedContentNumber
        {
            get
            {
                return TokenNormalizer.NormalizeLemmaNumber(Content);
            }
        }

        #endregion
    }
}