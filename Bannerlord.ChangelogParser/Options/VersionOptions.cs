using CommandLine;

namespace Bannerlord.ChangelogParser.Options
{
    [Verb("latestversion", HelpText = "Get latest entry version.")]
    internal class LatestVersionOptions
    {
        [Option('f', "file", Required = true)]
        public string ChangelogPath { get; set; } = default!;
    }
}