using System;
using System.IO;
using System.Linq;
using CanonicalEquation.Lib;
using CanonicalEquation.Lib.Extensions;
using CanonicalEquation.Lib.Parsers;
using CommandLine;

namespace CanonicalEquation.App
{
    class Program
    {
        static int Main(string[] args)
        {
            var parser = new Parser(cfg => cfg.CaseInsensitiveEnumValues = true);
            return parser.ParseArguments<Options>(args)
                .MapResult(HandleRunWithSelectedMode, errs => 1);
        }

        private static int HandleRunWithSelectedMode(Options arg)
        {
            try
            {
                switch (arg.Mode)
                {
                    case Options.EMode.Interactive:
                        return RunInteractiveMode();
                    case Options.EMode.File:
                        return RunFileMode(arg.InputFile);
                    default:
                        throw new ArgumentException("Unknown working mode. The application will be stopped");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return 1;
            }
        }

        private static int RunFileMode(string inputFileName)
        {
            var totalLinesProcessed = 0;
            var inputLines = File.ReadAllLines(inputFileName);

            var outputLines = inputLines.Select((inputLine, i) =>
            {
                var parseResult = EquationParser.TryParse(inputLine, out var equation);
                if (parseResult.IsSucceed)
                {
                    totalLinesProcessed++;
                    return equation.ToCanonicalEquation().ToString();
                }
                else
                {
                    return $"Failed to parse line {i}: {inputLine}";
                }
            });

            File.WriteAllLines($"{inputFileName}.out", outputLines);
            Console.WriteLine($"{totalLinesProcessed} lines processed");

            return 0;
        }

        private static int RunInteractiveMode()
        {

            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                var isCtrlC = e.SpecialKey == ConsoleSpecialKey.ControlC;
                // Prevent CTRL-C from terminating
                if (isCtrlC)
                {
                    e.Cancel = true;
                }
                // e.Cancel defaults to false so CTRL-BREAK will still cause termination
            };

            Console.WriteLine("Interactive mode: enter equations and displays result on enter. For exit press Ctrl+C");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter equation: ");
                string line = Console.ReadLine();

                var parseResult = EquationParser.TryParse(line, out var equation);
                if (parseResult.IsSucceed)
                {
                    Console.WriteLine("Canonical equation: " + equation.ToCanonicalEquation());
                }
                else
                {
                    Console.WriteLine($"Equation parse failed. See the errors: {parseResult.Errors.CollectionToStringWithSeparator(Environment.NewLine)}");
                }
            }


        }
    }
}
