using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Lib.Extensions;

namespace CanonicalEquation.Lib.Entities
{
    public class Summand
    {
        public float Multiplier { get; }
        public IEnumerable<Variable> Variables { get; }

        public string UniqueId => Variables
                                    .OrderBy(x => x.Power)
                                    .ThenBy(x => x.Name)
                                    .Select(x => x.ToString())
                                    .CollectionToStringWithSeparator(String.Empty);

        public string VariableNames => Variables.OrderBy(x => x.Name).CollectionToStringWithSeparator(String.Empty);

        public int MaxPower => Variables.Any() ? Variables.Max(x => x.Power) : 0;
        public int TotalPower => Variables.Any() ? Variables.Sum(x => x.Power) : 0;

        public Summand(float multiplier = 1) : this(null, multiplier)
        {
        }

        public Summand(IEnumerable<Variable> variables, float multiplier = 1)
        {
            Multiplier = multiplier;
            Variables = new List<Variable>(variables ?? new Variable[] { });
        }

        public Summand Normalize()
        {
            var normalizedVariables = Variables
                .GroupBy(x => x.Name)
                .Select(x => new Variable(x.Key, x.Sum(v => v.Power)))
                .Where(x => x.Power > 0);

            return new Summand(normalizedVariables, Multiplier);
        }

        public override string ToString()
        {
            if (Math.Abs(Multiplier) < float.Epsilon) return "0";

            var signStr = Multiplier < 0 ? Symbols.Minus.ToString() : String.Empty;
            var absMultiplier = Math.Abs(Multiplier);

            var absMultiplierString = Math.Abs(absMultiplier - 1) < float.Epsilon
                ? Variables.Any() ? String.Empty : $"{absMultiplier.ToString("G", System.Globalization.CultureInfo.InvariantCulture)}"
                : $"{absMultiplier.ToString("G", System.Globalization.CultureInfo.InvariantCulture)}";

            var variablesString = Variables
                .OrderByDescending(x => x.Power)
                .ThenBy(x => x.Name)
                .CollectionToStringWithSeparator(String.Empty);

            return $"{signStr}{absMultiplierString}{variablesString}";
        }

        public Summand Negate() => new Summand(Variables, -Multiplier);

        public static Summand operator *(Summand left, Summand right)
        {
            if (left == null || right == null) return null;

            var resultMultiplier = left.Multiplier * right.Multiplier;
            if(Math.Abs(resultMultiplier) < float.Epsilon) return new Summand(0);

            var concatVariables = new List<Variable>(left.Variables).Concat(right.Variables);

            return new Summand(concatVariables, resultMultiplier).Normalize();
        }

    }
}