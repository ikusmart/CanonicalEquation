﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using CanonicalEquation.Lib.Extensions;
using CanonicalEquation.Lib.Entities;

namespace CanonicalEquation.Lib.Parsers
{
    public class SummandParser 
    {
        private const string MultiplierRegexGroupName = "multiplier";
        private const string VariblesRegexGroupName = "varibles";
        private const string PowerRegexGroupName = "power";

        private static readonly string SummandRegexPattern = $@"^(?<{MultiplierRegexGroupName}>[+-]?\d*[\.\,]?\d*)(?<{VariblesRegexGroupName}>[a-zA-Z](?<{PowerRegexGroupName}>\^\d+)*)*$";
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

            if (multiplierString.Equals(String.Empty) || multiplierString.Equals(Symbols.Minus.ToString()) ||
                multiplierString.Equals(Symbols.Plus.ToString()))
                multiplierString += "1";

            var multiplier = float.Parse(multiplierString.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat);

            if (Math.Abs(multiplier) < float.Epsilon) return new Summand(0);

            var variables = new List<Variable>();
            foreach (Capture capture in regexGroupResult[VariblesRegexGroupName].Captures)
            {
                variables.Add(VariableParser.Parse(capture.Value));
            }

            return new Summand(variables, multiplier);
        }
    }
}