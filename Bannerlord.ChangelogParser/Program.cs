using Bannerlord.ChangelogParser.Options;

using CommandLine;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bannerlord.ChangelogParser
{
    public static class Program
    {
        public static void Main(string[] args) => Parser
            .Default
            .ParseArguments<LatestVersionOptions, DescriptionOptions, FullDescriptionOptions, GameVersionOptions>(args)
            .WithParsed<LatestVersionOptions>(o =>
            {
                var latestVersion = GetChangelogEntries(o.ChangelogPath)
                    .OrderByDescending(x => x.Version, new AlphanumComparatorFast())
                    .First();
                Console.Write(latestVersion.Version);
            })
            .WithParsed<DescriptionOptions>(o =>
            {
                if (o.Version == null)
                {
                    var latestVersion = GetChangelogEntries(o.ChangelogPath)
                        .OrderByDescending(x => x.Version, new AlphanumComparatorFast())
                        .First();
                    Console.Write(latestVersion.Description);
                }
                else
                {
                    var find = GetChangelogEntries(o.ChangelogPath)
                        .FirstOrDefault(x => x.Version == o.Version);
                    Console.Write(find != null ? find.Description : "NOT FOUND");
                }
            })
            .WithParsed<FullDescriptionOptions>(o =>
            {
                if (o.Version == null)
                {
                    var latestVersion = GetChangelogEntries(o.ChangelogPath)
                        .OrderByDescending(x => x.Version, new AlphanumComparatorFast())
                        .First();
                    Console.Write(latestVersion.GetFullDescription());
                }
                else
                {
                    var find = GetChangelogEntries(o.ChangelogPath)
                        .FirstOrDefault(x => x.Version == o.Version);
                    Console.Write(find != null ? find.GetFullDescription() : "NOT FOUND");
                }
            })
            .WithParsed<GameVersionOptions>(o =>
            {
                if (o.Version == null)
                {
                    var latestVersion = GetChangelogEntries(o.ChangelogPath)
                        .OrderByDescending(x => x.Version, new AlphanumComparatorFast())
                        .First();
                    Console.Write(string.Join(',', latestVersion.SupportedGameVersions));
                }
                else
                {
                    var find = GetChangelogEntries(o.ChangelogPath)
                        .FirstOrDefault(x => x.Version == o.Version);
                    Console.Write(find != null ? string.Join(',', find.SupportedGameVersions) : "NOT FOUND");
                }
            })
            .WithNotParsed(e =>
            {
                Console.Write("INVALID COMMAND");
            });

        private static IEnumerable<ChangelogEntry> GetChangelogEntries(string path) =>
            GetChangelogEntries(new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(path))));
        private static IEnumerable<ChangelogEntry> GetChangelogEntries(Stream stream)
        {
            var reader = new PeekingStreamReader(stream);

            string? line;
            while ((line = reader.PeekLine()) != null)
            {
                if (line.StartsWith("-"))
                {
                    reader.ReadLine();
                    var changelogEntry = ReadChangeLogEntry(reader);
                    if (changelogEntry != null)
                        yield return changelogEntry;
                }
            }
        }
        private static ChangelogEntry? ReadChangeLogEntry(PeekingStreamReader reader)
        {
            var result = new ChangelogEntry();

            var builder = new StringBuilder();
            string? line;
            while ((line = reader.PeekLine()) != null)
            {
                switch (line)
                {
                    case { } str when str.StartsWith("Version:"):
                        result.Version = line.Replace("Version:", "").Trim();
                        reader.ReadLine();
                        continue;
                    case { } str when str.StartsWith("Game Versions:"):
                        result.SupportedGameVersions = line.Replace("Game Versions:", "").Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
                        reader.ReadLine();
                        continue;
                    case { } str when str.StartsWith("-"):
                        result.Description = builder.ToString();
                        return result;
                    default:
                        builder.AppendLine(line);
                        reader.ReadLine();
                        continue;
                }
            }

            return null;
        }
    }
}
