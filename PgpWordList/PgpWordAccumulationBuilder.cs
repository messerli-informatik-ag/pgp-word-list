using System.Collections.Generic;
using System.Linq;

namespace Messerli.PgpWordList
{
    public class PgpWordAccumulationBuilder
    {
        public const string DefaultSeparator = "-";

        private readonly List<byte> _byteList = new List<byte>();

        private string _separator = DefaultSeparator;

        public PgpWordAccumulationBuilder SetSeparator(string separator)
        {
            _separator = separator;
            return this;
        }

        public PgpWordAccumulationBuilder Add(byte @byte)
        {
            _byteList.Add(@byte);
            return this;
        }

        public PgpWordAccumulationBuilder Add(byte[] byteArray)
        {
            _byteList.AddRange(byteArray);
            return this;
        }

        public PgpWordAccumulation Build()
            => new PgpWordAccumulation(
                string.Join(
                    _separator,
                    _byteList.Aggregate(new List<string>(), ByteToWord)));

        private static List<string> ByteToWord(List<string> list, byte @byte)
        {
            list.Add(
                list.Count % 2 == 0
                    ? PgpWordList.MapEven(@byte)
                    : PgpWordList.MapOdd(@byte));

            return list;
        }
    }
}
