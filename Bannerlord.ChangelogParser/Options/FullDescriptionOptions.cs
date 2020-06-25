using CommandLine;

namespace Bannerlord.ChangelogParser.Options
{
    [Verb("fulldescription", HelpText = "Get entry full description.")]
    internal class FullDescriptionOptions 
    {
        [Option('v', "version", Required = false)]
        public string? Version { get; set; }

        [Option('f', "file", Required = true)]
        public string ChangelogPath { get; set; } = default!;
    }
}