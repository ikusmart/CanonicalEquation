using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Lib.Extensions;
using CanonicalEquation.Lib.Entities;
using CanonicalEquation.Lib.Exceptions;
using CanonicalEquation.Lib.Helpers;

namespace CanonicalEquation.Lib.Parsers
{
    public static class PolynomialParser 
    {
        private static readonly HashSet<char> AllowedSymbols = new HashSet<char>(Symbols.AllowedSymbolsForEquation);

        public static Polynomial Parse(string polynomialString)
        {
            if (polynomialString == null) throw new ArgumentNullException(nameof(polynomialString), "Initial polynomial string must be not null");
            if (polynomialString.IsNullOrWhiteSpace()) throw new NotValidPolynomialArgumentException(nameof(polynomialString), "Initial polynomial string must be not empty");
            if (!IsContainsOnlyAllowedSymbols(polynomialString))
            {
                throw new NotValidPolynomialArgumentException(
                    $"Initial polynomial string must contains only letters, digits or special symbols: '{AllowedSymbols}'", nameof(polynomialString));
            }

            ParseHelper.ValidateBrackets(polynomialString);

            var monomialsParts = ParseHelper.GetMonomialssForPolynomial(polynomialString);

            var monomialsItems = monomialsParts.Select(MonomialParser.Parse).Select(x => x.Summands).ToArray();

            return new Polynomial(monomialsItems.SelectMany(x => x)).Normalize();
        }

        private static bool IsContainsOnlyAllowedSymbols(string initialString)
            => !initialString.IsNullOrWhiteSpace() &&
               initialString.All(x => char.IsLetterOrDigit(x) || AllowedSymbols.Contains(x));
    }
}