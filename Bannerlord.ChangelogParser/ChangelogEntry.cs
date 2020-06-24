namespace Bannerlord.ChangelogParser
{
    internal class ChangelogEntry
    {
        public string Version { get; set; } = default!;
        public string[] SupportedGameVersions { get; set; }= default!;
        public string Description { get; set; } = default!;

        public override string ToString() => $@"For {string.Join('/', SupportedGameVersions)}
{string.Join('/', SupportedGameVersions)}
{Description}";
    }
}