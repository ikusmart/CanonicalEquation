using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CanonicalEquation.Lib.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        public static string RemoveWhiteSpaces(this string value)
        {
            return value?.Replace(Symbols.Space.ToString(), String.Empty);
        }

        public static string RemoveSymbols(this string value, params char[] symbols)
        {
            if (symbols == null) return value;

            return symbols.Aggregate(value, (current, symbol) => current.Replace(symbol.ToString(), String.Empty));
        }

        public static string CollectionToStringWithSeparator<T>(this IEnumerable<T> collection, string separator )
        {
            if(collection == null) return String.Empty;

            return String.Join(separator, collection.Select(x => x.ToString()));
        }
    }
}
