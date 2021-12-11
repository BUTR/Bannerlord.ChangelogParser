using NUnit.Framework;

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Bannerlord.ChangelogParser.Test
{
    public class Tests
    {
        private static string NL => Environment.NewLine;
        private static Stream FromString(string str) => new MemoryStream(Encoding.UTF8.GetBytes(str));
       

        [Test]
        public void StandardOneValid()
        {
            const string text = @"
---------------------------------------------------------------------------------------------------
Version: 1.0.0
Game Versions: e1.4.3
* Line
---------------------------------------------------------------------------------------------------";

            var result = Program.GetChangelogEntries(FromString(text)).FirstOrDefault();
            Assert.NotNull(result);

            Assert.AreEqual("1.0.0", result.Version);
            Assert.AreEqual(new[] { "e1.4.3" }, result.SupportedGameVersions);
            Assert.AreEqual("* Line", result.Description);
        }

        [Test]
        public void StandardMultipleValid()
        {
            const string text = @"
---------------------------------------------------------------------------------------------------
Version: 1.0.1
Game Versions: e1.4.4
* Line 2
---------------------------------------------------------------------------------------------------
Version: 1.0.0
Game Versions: e1.4.3
* Line 1
---------------------------------------------------------------------------------------------------";

            var result = Program.GetChangelogEntries(FromString(text)).ToArray();
            Assert.True(result.Length == 2);

            Assert.AreEqual("1.0.1", result[0].Version);
            Assert.AreEqual(new[] { "e1.4.4" }, result[0].SupportedGameVersions);
            Assert.AreEqual("* Line 2", result[0].Description);

            Assert.AreEqual("1.0.0", result[1].Version);
            Assert.AreEqual(new[] { "e1.4.3" }, result[1].SupportedGameVersions);
            Assert.AreEqual("* Line 1", result[1].Description);
        }

        [Test]
        public void TextExcessiveNewLines()
        {
            const string text = @"
---------------------------------------------------------------------------------------------------

Version: 1.0.0

Game Versions: e1.4.3

* Line

* Line

---------------------------------------------------------------------------------------------------";

            var result = Program.GetChangelogEntries(FromString(text)).FirstOrDefault();
            Assert.NotNull(result);

            Assert.AreEqual("1.0.0", result.Version);
            Assert.AreEqual(new[] { "e1.4.3" }, result.SupportedGameVersions);
            Assert.AreEqual($"* Line{NL}{NL}* Line", result.Description);
        }

        [Test]
        public void TextExcessiveGameVersionComma()
        {
            const string text = @"
---------------------------------------------------------------------------------------------------
Version: 1.0.0
Game Versions: e1.4.3,
* Line
---------------------------------------------------------------------------------------------------";

            var result = Program.GetChangelogEntries(FromString(text)).FirstOrDefault();
            Assert.NotNull(result);

            Assert.AreEqual("1.0.0", result.Version);
            Assert.AreEqual(new[] { "e1.4.3" }, result.SupportedGameVersions);
            Assert.AreEqual("* Line", result.Description);
        }


        [Test]
        public void GitHub_Issue_8()
        {
            const string text = @"
---------------------------------------------------------------------------------------------------
Version: 1.0.0
Game Versions: e1.4.3
* Line
---------------------------------------------------------------------------------------------------";

            var result = Program.GetChangelogEntries(FromString(text)).FirstOrDefault();
            Assert.NotNull(result);

            Assert.AreEqual("1.0.0", result.Version);
            Assert.AreEqual(new[] { "e1.4.3" }, result.SupportedGameVersions);
            Assert.AreEqual("* Line", result.Description);
        }
    }
}