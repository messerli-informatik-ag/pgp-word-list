using System;
using System.Linq;

namespace Messerli.PgpWordList
{
    public static class PgpWordAccumulationExtension
    {
        public static PgpWordAccumulation ToPgpWordAccumulation(
            this short number,
            string separator = PgpWordAccumulationBuilder.DefaultSeparator)
            => number
                .ToByteArray()
                .ReverseForLittleEndianArchitecture()
                .ToPgpWordAccumulation(separator);

        public static PgpWordAccumulation ToPgpWordAccumulation(
            this int number,
            string separator = PgpWordAccumulationBuilder.DefaultSeparator)
            => number
                .ToByteArray()
                .ReverseForLittleEndianArchitecture()
                .ToPgpWordAccumulation(separator);

        public static PgpWordAccumulation ToPgpWordAccumulation(
            this byte[] byteArray,
            string separator = PgpWordAccumulationBuilder.DefaultSeparator)
            => new PgpWordAccumulationBuilder()
                .SetSeparator(separator)
                .Add(byteArray)
                .Build();

        public static int ToInt32(
            this PgpWordAccumulation pgpWordAccumulation,
            string separator = PgpWordAccumulationBuilder.DefaultSeparator)
            => pgpWordAccumulation
                .ToByteArray(separator)
                .ReverseForLittleEndianArchitecture()
                .ToInt32();

        public static int ToInt16(
            this PgpWordAccumulation pgpWordAccumulation,
            string separator = PgpWordAccumulationBuilder.DefaultSeparator)
            => pgpWordAccumulation
                .ToByteArray(separator)
                .ReverseForLittleEndianArchitecture()
                .ToInt16();

        public static byte[] ToByteArray(
            this PgpWordAccumulation pgpWordAccumulation,
            string separator = PgpWordAccumulationBuilder.DefaultSeparator)
            => new PgpWordAccumulationParser(separator)
                .Parse(pgpWordAccumulation);

        private static byte[] ToByteArray(this int number)
            => BitConverter.GetBytes(number);

        private static byte[] ToByteArray(this short number)
            => BitConverter.GetBytes(number);

        private static short ToInt16(this byte[] byteArray)
            => BitConverter.ToInt16(byteArray);

        private static int ToInt32(this byte[] byteArray)
            => BitConverter.ToInt32(byteArray);

        private static byte[] ReverseForLittleEndianArchitecture(this byte[] byteArray)
            => BitConverter.IsLittleEndian
                ? byteArray.Reverse().ToArray()
                : byteArray;
    }
}
