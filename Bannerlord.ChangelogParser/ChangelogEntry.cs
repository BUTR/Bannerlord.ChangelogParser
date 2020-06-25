namespace Bannerlord.ChangelogParser
{
    internal class ChangelogEntry
    {
        public string Version { get; set; } = default!;
        public string[] SupportedGameVersions { get; set; }= default!;
        public string Description { get; set; } = default!;

        public string GetFullDescription() => $@"For {string.Join('/', SupportedGameVersions)}
{Description}";
    }
}