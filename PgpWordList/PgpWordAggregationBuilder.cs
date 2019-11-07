using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Messerli.PgpWordList
{
    public class PgpWordAggregationBuilder
    {
        public const string DefaultSeparator = "-";

        private readonly List<byte> _byteList = new List<byte>();

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
                    _byteList.Aggregate(new List<string>(), ByteToWord)));

        private static List<string> ByteToWord(List<string> list, byte @byte)
        {
            list.Add(
                IsEven(list)
                    ? PgpWordList.MapEven(@byte)
                    : PgpWordList.MapOdd(@byte));

            return list;
        }

        private static bool IsEven(ICollection list)
            => list.Count % 2 == 0;
    }
}
