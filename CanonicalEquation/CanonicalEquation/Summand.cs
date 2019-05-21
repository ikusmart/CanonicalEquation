using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CanonicalEquation
{
    /// <summary>
    /// Summand in equation
    /// For example^
    ///     2x^2y^4
    ///     -3x^2
    ///     5z
    ///     -1
    /// </summary>
    public class Summand : MonomialItem
    {
        public IList<Variable> Variables { get; } = new List<Variable>();

        public Summand(float multiplier = 1) : base(multiplier)
        {
        }

        public Summand(IEnumerable<Variable> variables, float multiplier = 1) : this(multiplier)
        {
            Variables = new List<Variable>(variables ?? new Variable[] {});
        }

        public override string ToString()
        {
            if (Math.Abs(Multiplier) < float.Epsilon) return string.Empty;

            var signStr = Multiplier < 0 ? SymbolsConsts.Minus.ToString() : SymbolsConsts.Plus.ToString();
            var absMultiplier = Math.Abs(Multiplier);

            var absMultiplierString = Math.Abs(absMultiplier - 1) < float.Epsilon
                ? Variables.Any() ? String.Empty : $"{absMultiplier}"
                : $"{absMultiplier}";

            var variablesString = String.Join(String.Empty, Variables.Select(x => x.ToString()));

            return $"{signStr}{absMultiplierString}{variablesString}";
        }
    }
}