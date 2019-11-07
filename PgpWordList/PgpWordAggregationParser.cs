using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Messerli.PgpWordList
{
    public class PgpWordAggregationParser
    {
        public const string DefaultSeparator = "-";

        private readonly string _separator;

        public PgpWordAggregationParser(string separator = DefaultSeparator)
        {
            _separator = separator;
        }

        public byte[] Parse(PgpWordAggregation pgpWordAggregation)
            => pgpWordAggregation
                .Value
                .Split(_separator)
                .Aggregate(new List<byte>(), AggregateWordToByteArray)
                .ToArray();

        private static List<byte> AggregateWordToByteArray(List<byte> list, string word)
        {
            list.Add(
                WordToByte(word, IsEven(list))
                     ?? throw new ArgumentException($"'{word}' is an invalid pgp word."));

            return list;
        }

        private static byte? WordToByte(string word, bool even)
            => even
                ? PgpWordList.FindEven(word)
                : PgpWordList.FindOdd(word);

        private static bool IsEven(ICollection list)
            => list.Count % 2 == 0;
    }
}
