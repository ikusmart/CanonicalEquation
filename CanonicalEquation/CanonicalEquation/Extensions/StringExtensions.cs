using System;

namespace CanonicalEquation.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }
    }
}
