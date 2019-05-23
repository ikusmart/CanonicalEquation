using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Exceptions;
using CanonicalEquation.Extensions;
using CanonicalEquation.Helpers;

namespace CanonicalEquation.Parsers
{
    public static class PolynomialParser
    {
        private static readonly HashSet<char> AllowedSymbols = new HashSet<char>(SymbolsConsts.AllowedSymbolsForEquation);

        public static Polynomial Parse(string polynomialString)
        {
            if (polynomialString.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(polynomialString), "Initial polynomial string must be not empty");
            if (!IsContainsOnlyAllowedSymbols(polynomialString))
            {
                throw new NotValidPolynomialArgumentException(
                    $"Initial polynomial string must contains only letters, digits or special symbols: '{AllowedSymbols}'", nameof(polynomialString));
            }

            ParseHelper.ValidateBrackets(polynomialString);

            var monomialsParts = ParseHelper.GetMonomialsPartsForPolynomialString(polynomialString);

            return new Polynomial(monomialsParts.Select(MonomialParse.Parse));
        }

        private static bool IsContainsOnlyAllowedSymbols(string initialString)
            => !initialString.IsNullOrWhiteSpace() &&
               initialString.All(x => char.IsLetterOrDigit(x) || AllowedSymbols.Contains(x));
    }
}