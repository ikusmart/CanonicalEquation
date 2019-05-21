using System;

namespace CanonicalEquation.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        public static string RemoveWhiteSpaces(this string value)
        {
            if (value == null) return null;

            return value.Trim(SymbolsConsts.Space);
        }
    }
}
