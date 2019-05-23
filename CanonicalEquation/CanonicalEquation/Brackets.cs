using System;
using CanonicalEquation.Extensions;
using CanonicalEquation.Helpers;
using CanonicalEquation.Parsers;

namespace CanonicalEquation
{
    /// <summary>
    /// Brackets as a part of monomial. May contain a polynomial (of one or several monomials)
    /// For example:
    ///     (x^2+y^3-(y-x^5))
    ///     (x-1)
    ///     (3-4z)
    /// </summary>
    public class Brackets : MonomialItem
    {
        public Polynomial Polynomial { get; set; }

        public Brackets(Polynomial polynomial, float multiplier = 1) : base(multiplier)
        {
            
        }

        public override string ToString()
        {
            return $"({Polynomial})";
        }
    }

    public static class BracketsParser
    {
        public static Brackets Parse(string bracketsString)
        {
            if(bracketsString.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(bracketsString));

            ParseHelper.ValidateBrackets(bracketsString);

            //Get sign ??
            
            var contentInsideBrackets = ParseHelper.GetContentForBrackets(bracketsString);

            var polynomial = PolynomialParser.Parse(contentInsideBrackets);

            return new Brackets(polynomial);
        }
    }

}