using System;
using Epiphany.Extensions;

namespace Skylar.Extensions
{
    internal static class StringExtensions
    {
        internal static bool IsWhiteSpaceOrControl(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return true;
            }

            foreach (var item in source)
            {
                if (item.IsWhiteSpaceOrControl())
                {
                    return true;
                }
            }

            return false;
        }
    }
}