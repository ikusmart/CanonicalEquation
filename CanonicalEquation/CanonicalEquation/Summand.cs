using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        #region Properties

        public IList<Variable> Variables { get; } = new List<Variable>();

        #endregion

        #region ctor

        public Summand(float multiplier = 1) : base(multiplier)
        {
        }

        public Summand(IEnumerable<Variable> variables, float multiplier = 1) : this(multiplier)
        {
            Variables = new List<Variable>(variables ?? new Variable[] {});
        }

        #endregion

        #region Methods

        public Summand Normaize()
        {
            var normalizedVariables = Variables
                .GroupBy(x => x.Name)
                .Select(x => new Variable(x.Key, x.Sum(v => v.Power)));

            return new Summand(normalizedVariables, Multiplier);
        }

        public int MaxPower => Variables.Any() ? Variables.Max(x => x.Power) : 0;
        public Summand Negate() => new Summand(Variables, -Multiplier);

        public override string ToString()
        {
            if (Math.Abs(Multiplier) < float.Epsilon) return "0";

            var signStr = Multiplier < 0 ? SymbolsConsts.Minus.ToString() : SymbolsConsts.Plus.ToString();
            var absMultiplier = Math.Abs(Multiplier);

            var absMultiplierString = Math.Abs(absMultiplier - 1) < float.Epsilon
                ? Variables.Any() ? String.Empty : $"{absMultiplier}"
                : $"{absMultiplier}";

            var variablesString = Variables
                .OrderByDescending(x => x.Power)
                .ThenBy(x => x.Name)
                .CollectionToStringWithSeparator(String.Empty);

            return $"{signStr}{absMultiplierString}{variablesString}";
        }

        #endregion

        public static Summand operator *(Summand left, Summand right)
        {
            if (left == null && right == null) return null;
            if (left != null && right == null) return left;
            if (left == null && right != null) return right;

            var unionVariables = new List<Variable>(left.Variables).Union(right.Variables);
            var multipliedPower = left.Multiplier * right.Multiplier;

            return new Summand(unionVariables, multipliedPower).Normaize();
        }

        public static Summand operator *(Summand left, IEnumerable<Summand> right)
        {
            if (left == null && right == null) return null;
            if (left != null && right == null) return left;

            var rightSummandsArray = right.ToArray();
            Summand resultSummand = left;
            foreach (var t in rightSummandsArray)
            {
                resultSummand *= t;
            }

            return resultSummand;
        }

    }
}