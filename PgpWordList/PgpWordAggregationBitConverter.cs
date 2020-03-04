using System;
using System.Linq;

namespace Messerli.PgpWordList
{
    public static class PgpWordAggregationBitConverter
    {
        public static PgpWordAggregation ToPgpWordAccumulation(
            short number,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => ToPgpWordAccumulation(
                number
                    .GetBytes()
                    .ReverseForLittleEndianArchitecture(),
                separator);

        public static PgpWordAggregation ToPgpWordAccumulation(
            int number,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => ToPgpWordAccumulation(
                number
                    .GetBytes()
                    .ReverseForLittleEndianArchitecture(),
                separator);

        public static PgpWordAggregation ToPgpWordAccumulation(
            byte[] byteArray,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => new PgpWordAggregationBuilder()
                .SetSeparator(separator)
                .Add(byteArray)
                .Build();

        public static int ToInt32(
            PgpWordAggregation pgpWordAggregation,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => GetBytes(pgpWordAggregation, separator)
                .ReverseForLittleEndianArchitecture()
                .ToInt32();

        public static int ToInt16(
            PgpWordAggregation pgpWordAggregation,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => GetBytes(pgpWordAggregation, separator)
                .ReverseForLittleEndianArchitecture()
                .ToInt16();

        public static byte[] GetBytes(
            PgpWordAggregation pgpWordAggregation,
            string separator = PgpWordAggregationBuilder.DefaultSeparator)
            => new PgpWordAggregationParser(separator)
                .Parse(pgpWordAggregation);

        private static byte[] GetBytes(this int number)
            => BitConverter.GetBytes(number);

        private static byte[] GetBytes(this short number)
            => BitConverter.GetBytes(number);

        private static short ToInt16(this byte[] byteArray)
            => BitConverter.ToInt16(byteArray, startIndex: 0);

        private static int ToInt32(this byte[] byteArray)
            => BitConverter.ToInt32(byteArray, startIndex: 0);

        private static byte[] ReverseForLittleEndianArchitecture(this byte[] byteArray)
            => BitConverter.IsLittleEndian
                ? byteArray.Reverse().ToArray()
                : byteArray;
    }
}
