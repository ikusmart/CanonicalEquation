using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Exceptions;
using CanonicalEquation.Parsers;

namespace CanonicalEquation
{
    /// <summary>
    /// Variable in equation
    /// Example:
    ///     x^3
    ///     y
    ///     z^5
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// Name of variable
        ///     For 'x^3' name is 'x'
        /// </summary>
        public char Name { get; }

        /// <summary>
        /// Power of variable
        ///     default: 1 (for example 'x')
        ///     For 'x^3' power is '3'
        /// </summary>
        public int Power { get; }

        public Variable(char name, int power = 1)
        {
            if (!char.IsLetter(name)) throw new NotValidVariableArgumentException("Name of variable must be a letter", nameof(name));

            Name = name;
            Power = power;
        }

        public override string ToString()
        {
            return Power == 1 ? Name.ToString() : $"{Name}^{Power}";
        }

    }

    public static class EquationOperation
    {
        //public static Summand Sum(this IEnumerable<Summand> summands)
        //{
        //    var summandArray = summands?.ToArray();
        //    if (summandArray?.Any() == false) return null;

        //    if (summandArray.Length == 1) return summandArray[0];


        //    Summand summandResult;
        //    for (int i = 0; i < summandArray.Length; i++)
        //    {
        //        summandResult = summandArray[i] * summandArray[i+1];
        //    }
        //}

        public static Summand Multiply(this IEnumerable<Summand> summands)
        {
            var summandArray = summands.ToArray();
            if (summandArray.Length == 0) return null;
            if (summandArray.Length == 1) return summandArray[0];

            Summand result = null;
            foreach (var summand in summandArray)
            {
                result *= summand;
            }

            return result;
        }
    }
}