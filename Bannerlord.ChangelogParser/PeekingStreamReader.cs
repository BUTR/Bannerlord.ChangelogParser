using System.Collections.Generic;
using System.IO;

namespace Bannerlord.ChangelogParser
{
    internal class PeekingStreamReader : StreamReader
    {
        private readonly Queue<string?> _peeks = new Queue<string?>();

        public PeekingStreamReader(Stream stream) : base(stream) { }

        public override string? ReadLine()
        {
            if (_peeks.Count > 0)
            {
                var nextLine = _peeks.Dequeue();
                return nextLine;
            }
            return base.ReadLine();
        }

        public string? PeekLine()
        {
            var nextLine = ReadLine();
            _peeks.Enqueue(nextLine);
            return nextLine;
        }
    }
}