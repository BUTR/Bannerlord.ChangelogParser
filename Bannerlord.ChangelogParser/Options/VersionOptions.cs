using CommandLine;

namespace Bannerlord.ChangelogParser.Options
{
    [Verb("getversion", HelpText = "Get latest entry version.")]
    public class VersionOptions 
    {
        [Option('f', "file", Required = true)]
        public string ChangelogPath { get; set; } = default!;
    }
}