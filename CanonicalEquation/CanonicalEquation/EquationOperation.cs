﻿using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Lib.Entities;

namespace CanonicalEquation.Lib
{
    public static class EquationOperation
    {
        public static IEnumerable<Summand> Sum(this IEnumerable<Summand> summands)
        {
            var summandArray = summands?.ToArray() ?? new Summand[] { };
            if (summandArray.Length == 0) return new Summand[] { };
            if (summandArray.Length == 1) return summandArray;

            var summandNews = summandArray.Select(x => x.Normalize());

            var result = summandNews
                .GroupBy(x => x.UniqueId)
                .Select(x => new Summand(x.First().Variables, x.Sum(y => y.Multiplier)))
                .Where(x => Math.Abs(x.Multiplier) > float.Epsilon);
            return result;
        }

        public static Summand Multiply(this IEnumerable<Summand> summands)
        {
            var summandArray = summands?.ToArray() ?? new Summand[] { };
            if (summandArray.Length == 0) return null;
            if (summandArray.Length == 1) return summandArray[0];

            Summand result = null;
            foreach (var summand in summandArray)
            {
                result *= summand;
            }

            return result;
        }
        
        public static IEnumerable<Summand> Multiply(IEnumerable<Summand> leftItems, IEnumerable<Summand> rightItems)
        {
            var leftArray = leftItems?.ToArray() ?? new Summand[] { };
            var rightArray = rightItems?.ToArray() ?? new Summand[] { };

            if (leftArray.Length == 0 || rightArray.Length == 0) return new Summand[] {};

            var result = leftArray.SelectMany(x => rightArray.Select(y => x * y)).Where(x => x != null);
            return result;
        }
    }
}