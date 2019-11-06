using System;
using System.Linq;

namespace Messerli.PgpWordList
{
    public static class PgpWordAggregationExtension
    {
        public static PgpWordAggregation ToPgpWordAccumulation(
            this short number,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => number
                .ToByteArray()
                .ReverseForLittleEndianArchitecture()
                .ToPgpWordAccumulation(separator);

        public static PgpWordAggregation ToPgpWordAccumulation(
            this int number,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => number
                .ToByteArray()
                .ReverseForLittleEndianArchitecture()
                .ToPgpWordAccumulation(separator);

        public static PgpWordAggregation ToPgpWordAccumulation(
            this byte[] byteArray,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => new PgpWordAggregationBuilder()
                .SetSeparator(separator)
                .Add(byteArray)
                .Build();

        public static int ToInt32(
            this PgpWordAggregation pgpWordAggregation,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => pgpWordAggregation
                .ToByteArray(separator)
                .ReverseForLittleEndianArchitecture()
                .ToInt32();

        public static int ToInt16(
            this PgpWordAggregation pgpWordAggregation,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => pgpWordAggregation
                .ToByteArray(separator)
                .ReverseForLittleEndianArchitecture()
                .ToInt16();

        public static byte[] ToByteArray(
            this PgpWordAggregation pgpWordAggregation,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => new PgpWordAggregationParser(separator)
                .Parse(pgpWordAggregation);

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
