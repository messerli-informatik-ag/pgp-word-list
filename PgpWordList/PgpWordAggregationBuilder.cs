using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Messerli.PgpWordList
{
    public class PgpWordAggregationBuilder
    {
        public const string DefaultSeparator = "-";

        private readonly ImmutableList<byte> _byteList = ImmutableList<byte>.Empty;

        private string _separator = DefaultSeparator;

        public PgpWordAggregationBuilder SetSeparator(string separator)
        {
            _separator = separator;
            return this;
        }

        public PgpWordAggregationBuilder Add(byte @byte)
        {
            _byteList.Add(@byte);
            return this;
        }

        public PgpWordAggregationBuilder Add(byte[] byteArray)
        {
            _byteList.AddRange(byteArray);
            return this;
        }

        public PgpWordAggregation Build()
            => new PgpWordAggregation(
                string.Join(
                    _separator,
                    _byteList.Aggregate(new List<string>(), AggregateByteToWordList)));

        private static List<string> AggregateByteToWordList(List<string> list, byte @byte)
        {
            list.Add(ByteToWord(@byte, IsEven(list)));
            return list;
        }

        private static string ByteToWord(byte @byte, bool even)
            => even
                ? PgpWordList.MapEven(@byte)
                : PgpWordList.MapOdd(@byte);

        private static bool IsEven(ICollection list)
            => list.Count % 2 == 0;
    }
}
