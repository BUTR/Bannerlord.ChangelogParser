using CommandLine;

namespace Bannerlord.ChangelogParser.Options
{
    [Verb("gameversion", HelpText = "Get entry supported game versions.")]
    internal class GameVersionOptions
    {
        [Option('v', "version", Required = false)]
        public string? Version { get; set; }

        [Option('f', "file", Required = true)]
        public string ChangelogPath { get; set; } = default!;
    }
}