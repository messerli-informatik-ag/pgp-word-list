using System;
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
                .Split(new[] { _separator }, StringSplitOptions.None)
                .Select(WordToByte)
                .ToArray();

        private static byte WordToByte(string word, int index)
            => WordToByte(word, IsEven(index)) ?? throw new ArgumentException($"'{word}' is an invalid pgp word.");

        private static byte? WordToByte(string word, bool even)
            => even
                ? PgpWordList.FindEven(word)
                : PgpWordList.FindOdd(word);

        private static bool IsEven(int number) => number % 2 == 0;
    }
}
