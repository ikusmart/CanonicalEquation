using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace CanonicalEquation.App
{
    public class Options
    {
        public enum EMode
        {
            Unknown = -1,
            Interactive = 1,
            File = 2
        }

        [Usage(ApplicationAlias = "CanonicalEquation.App")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Interactive mode", new Options { Mode = EMode.Interactive });
                yield return new Example("File mode", new Options { Mode = EMode.File, InputFile = "inputFile.in" });
            }
        }

        [Option('m', "mode", Required = true, HelpText = "Mode: 'interactive' (or 1) or 'file' (or 2)  modes")]
        public EMode Mode { get; set; }

        [Option('f', "file", Required = false, HelpText = "Input file name")]
        public string InputFile { get; set; }
    }
}