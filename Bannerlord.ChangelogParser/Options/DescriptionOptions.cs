using CommandLine;

namespace Bannerlord.ChangelogParser.Options
{
    [Verb("getdescription", HelpText = "Get entry description.")]
    public class DescriptionOptions 
    {
        [Option('v', "version", Required = false)]
        public string? Version { get; set; }

        [Option('f', "file", Required = true)]
        public string ChangelogPath { get; set; } = default!;
    }
}