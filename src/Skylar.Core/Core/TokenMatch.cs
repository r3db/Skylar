using System;

namespace Skylar
{
    internal sealed class TokenMatch
    {
        internal TokenMatch(MultipartToken match)
            : this(match, null)
        {
        }

        internal TokenMatch(MultipartToken match, Token type)
        {
            Match = match;
            Type = type;
        }

        internal MultipartToken Match { get; private set; }

        internal Token Type { get; private set; }
    }
}