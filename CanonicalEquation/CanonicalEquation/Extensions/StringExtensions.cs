using System;
using System.Linq;

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
            return value?.Replace(SymbolsConsts.Space.ToString(), String.Empty);
        }

        public static string RemoveSymbols(this string value, params char[] symbols)
        {
            if (symbols == null) return value;

            return symbols.Aggregate(value, (current, symbol) => current.Replace(symbol.ToString(), String.Empty));
        }
    }
}
