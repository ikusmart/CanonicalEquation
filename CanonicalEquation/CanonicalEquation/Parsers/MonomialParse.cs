using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Extensions;
using CanonicalEquation.Helpers;

namespace CanonicalEquation.Parsers
{
    public static class MonomialParse
    {

        /// <summary>
        /// Consists of a collection of expressions in brackets (if there are in the string) and a list of summands (contained between brackets, if any).
        /// If there are no brackets, then the monomial consists of one summand)
        /// </summary>
        /// <param name="monomialString"></param>
        /// <returns></returns>
        public static Monomial Parse(string monomialString)
        {
            if(monomialString.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(monomialString));

            ParseHelper.GetMonomialItemsFromMonomialString(monomialString, out var summandPartsList, out var bracketsPartsList);

            var summands = summandPartsList?.Any() == true ? new List<Summand>(new[] { summandPartsList.Select(SummandParser.Parse).Multiply() }) : new List<Summand>();
            var brackets = bracketsPartsList?.Any() == true ? bracketsPartsList.Select(BracketsParser.Parse).ToList() : new List<Brackets>();

            return new Monomial(summands.OfType<MonomialItem>().Union(brackets));
        }

        


    }
}