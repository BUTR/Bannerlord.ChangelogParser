using CommandLine;

namespace Bannerlord.ChangelogParser.Options
{
    [Verb("description", HelpText = "Get entry description.")]
    internal class DescriptionOptions
    {
        [Option('v', "version", Required = false)]
        public string? Version { get; set; }

        [Option('f', "file", Required = true)]
        public string ChangelogPath { get; set; } = default!;
    }
}