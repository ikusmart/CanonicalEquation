﻿using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Lib.Extensions;
using CanonicalEquation.Lib.Entities;
using CanonicalEquation.Lib.Helpers;

namespace CanonicalEquation.Lib.Parsers
{
    public static class MonomialParser 
    {
        /// <summary>
        /// Consists of a collection of expressions in brackets (if there are in the string) and a list of summands (contained between brackets, if any).
        /// If there are no brackets, then the monomial consists of one summand)
        /// </summary>
        /// <param name="monomialString"></param>
        /// <returns></returns>
        public static Monomial Parse(string monomialString)
        {
            if (monomialString== null) throw new ArgumentNullException(nameof(monomialString));
            if (monomialString.IsNullOrWhiteSpace()) throw new ArgumentException(nameof(monomialString));

            var summandItems = ParseHelper.GetSummandsForMonomial(monomialString);

            var summand = summandItems.summandItems.Select(SummandParser.Parse).Multiply();
            
            IEnumerable<Summand> resultSummands = summand != null ? new []{ summand } : new Summand[]{ };
            resultSummands = summandItems.bracketsParts.Aggregate(
                    resultSummands, 
                    (current, bracketsPart) 
                        => EquationOperation.Multiply(current, PolynomialParser.Parse(bracketsPart).Normalize().Summands))
                .Sum();

            return new Monomial(resultSummands ?? new Summand[]{});
        }
    }
}