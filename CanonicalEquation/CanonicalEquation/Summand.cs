using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CanonicalEquation.Extensions;

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

    public static class SummandParser
    {
        private const string MultiplierRegexGroupName = "multiplier";
        private const string VariblesRegexGroupName = "varibles";
        private const string PowerRegexGroupName = "power";

        private static readonly string SummandRegexPattern = $@"^(?<{MultiplierRegexGroupName}>[+-]?\d*\.?\d*)(?<{VariblesRegexGroupName}>[a-zA-Z](?<{PowerRegexGroupName}>\^\d+)*)*$";
        private static readonly Regex SummandRegex = new Regex(SummandRegexPattern, RegexOptions.Singleline | RegexOptions.Compiled);

        public static Summand Parse(string summandString)
        {
            if (summandString.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(summandString));

            if (!SummandRegex.IsMatch(summandString))
            {
                throw new FormatException($"Could not parse summand string: {summandString}. Format string not matched by summand patters - '|(+,-)(number)|<variables=(name)^(power)>'.");
            }

            var regexGroupResult = SummandRegex.Match(summandString).Groups;

            var multiplierString = String.Empty;
            if (regexGroupResult[MultiplierRegexGroupName].Captures.Count == 1)
            {
                multiplierString = regexGroupResult[MultiplierRegexGroupName].Captures[0].Value;
            }

            if (multiplierString.Equals(String.Empty) || multiplierString.Equals(SymbolsConsts.Minus.ToString()) ||
                multiplierString.Equals(SymbolsConsts.Plus.ToString()))
                multiplierString += "1";

            var multiplier = float.Parse(multiplierString);

            var variables = new List<Variable>();
            foreach (Capture capture in regexGroupResult[VariblesRegexGroupName].Captures)
            {
                variables.Add(VariableParser.Parse(capture.Value));
            }

            return new Summand(variables, multiplier);
        }
    }
}