using System;

namespace Messerli.PgpWordList
{
    public class PgpWordAccumulationParser
    {
        public const string DefaultSeparator = "-";

        public PgpWordAccumulationParser(string separator = DefaultSeparator)
        {
        }

        public byte[] Parse(PgpWordAccumulation pgpWordAccumulation)
            => throw new NotImplementedException();
    }
}
