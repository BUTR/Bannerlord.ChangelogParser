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
            .ParseArguments<VersionOptions, DescriptionOptions>(args)
            .WithParsed<VersionOptions>(o =>
            {
                var changelogs = GetChangelogEntries(o.ChangelogPath);
                var first = changelogs.First();
                Console.Write(first.Version);
            })
            .WithParsed<DescriptionOptions>(o =>
            {
                var changelogs = GetChangelogEntries(o.ChangelogPath);
                if (o.Version == null)
                {
                    var first = changelogs.First();
                    Console.Write(first.Description);
                }
                else
                {
                    var find = changelogs.FirstOrDefault(x => x.Version == o.Version);
                    Console.Write(find != null ? find.Description : "NOT FOUND");
                }
            })
            .WithNotParsed(e =>
            {
                Console.Write("INVALID COMMAND");
            });

        private static IEnumerable<ChangelogEntry> GetChangelogEntries(string path)
        {
            var reader = new PeekingStreamReader(new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(path))));

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

            string? line;
            while ((line = reader.PeekLine()) != null)
            {
                if (line.StartsWith("Version:"))
                {
                    result.Version = line.Replace("Version:", "").Trim();
                    reader.ReadLine();
                    continue;
                }

                if (line.StartsWith("Game Versions:"))
                {
                    result.SupportedGameVersions = line.Replace("Game Versions:", "").Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
                    reader.ReadLine();
                    continue;
                }

                if (line.StartsWith("-"))
                {
                    return result;
                }

                result.Description += line + "\r\n";
                reader.ReadLine();
            }

            return null;
        }
    }
}