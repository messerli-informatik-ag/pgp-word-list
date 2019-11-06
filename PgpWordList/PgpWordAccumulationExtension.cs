using System;

namespace Messerli.PgpWordList
{
    public static class PgpWordAccumulationExtension
    {
        public static PgpWordAccumulation ToPgpWordAccumulation(this short number, string separator)
            => throw new NotImplementedException();

        public static PgpWordAccumulation ToPgpWordAccumulation(this int number, string separator)
            => throw new NotImplementedException();

        public static PgpWordAccumulation ToPgpWordAccumulation(this byte[] byteArray, string separator)
            => throw new NotImplementedException();

        public static int ToInt32(this PgpWordAccumulation pgpWordAccumulation, string separator)
            => throw new NotImplementedException();

        public static int ToInt16(this PgpWordAccumulation pgpWordAccumulation, string separator)
            => throw new NotImplementedException();

        public static byte[] ToByteArray(this PgpWordAccumulation pgpWordAccumulation, string separator)
            => throw new NotImplementedException();
    }
}
