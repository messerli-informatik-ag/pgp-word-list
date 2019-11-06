using System;
using System.Collections.Generic;
using System.Linq;

namespace Messerli.PgpWordList
{
    public class PgpWordAccumulationParser
    {
        public const string DefaultSeparator = "-";

        private readonly string _separator;

        public PgpWordAccumulationParser(string separator = DefaultSeparator)
        {
            _separator = separator;
        }

        public byte[] Parse(PgpWordAccumulation pgpWordAccumulation)
            => pgpWordAccumulation
                .Value
                .Split(_separator)
                .Aggregate(new List<byte>(), WordToByte)
                .ToArray();

        private static List<byte> WordToByte(List<byte> list, string word)
        {
            var @byte = list.Count % 2 == 0
                ? PgpWordList.FindEven(word)
                : PgpWordList.FindOdd(word);

            list.Add(@byte ?? throw new ArgumentException($"'{word}' is an invalid pgp word."));

            return list;
        }
    }
}
