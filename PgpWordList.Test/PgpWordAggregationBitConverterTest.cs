using Xunit;
using static Messerli.PgpWordList.PgpWordAggregationBitConverter;

namespace Messerli.PgpWordList.Test
{
    public class PgpWordAggregationBitConverterTest
    {
        [Theory]
        [MemberData(nameof(GetInt16TestData))]
        public void Int16ToPgpWordAccumulationMatchesPgpWordAccumulationBuilder(short int16, byte[] bytes)
            => Assert.Equal(BuildPgpWordAccumulation(bytes).Value, ToPgpWordAggregation(int16).Value);

        [Theory]
        [MemberData(nameof(GetInt16TestData))]
        public void PgpWordAccumulationToInt16MatchesPgpWordAccumulationParser(short int16, byte[] bytes)
        {
            var pgpWordAggregation = BuildPgpWordAccumulation(bytes);
            Assert.Equal(bytes, ParsePgpWordAccumulation(pgpWordAggregation));
            Assert.Equal(int16, ToInt16(pgpWordAggregation));
        }

        public static TheoryData<short, byte[]> GetInt16TestData()
            => new TheoryData<short, byte[]>
            {
                { 0, new byte[] { 0x00, 0x00 } },
                { 1171, new byte[] { 0x04, 0x93 } },
                { 6425, new byte[] { 0x19, 0x19 } },
                { -9602, new byte[] { 0xDA, 0x7E } },
                { -1, new byte[] { 0xFF, 0xFF } },
            };

        [Theory]
        [MemberData(nameof(GetInt32TestData))]
        public void Int32ToPgpWordAccumulationMatchesPgpWordAccumulationBuilder(int int32, byte[] bytes)
            => Assert.Equal(BuildPgpWordAccumulation(bytes).Value, ToPgpWordAggregation(int32).Value);

        [Theory]
        [MemberData(nameof(GetInt32TestData))]
        public void PgpWordAccumulationToInt32MatchesPgpWordAccumulationParser(int int32, byte[] bytes)
        {
            var pgpWordAggregation = BuildPgpWordAccumulation(bytes);
            Assert.Equal(bytes, ParsePgpWordAccumulation(pgpWordAggregation));
            Assert.Equal(int32, ToInt32(pgpWordAggregation));
        }

        public static TheoryData<int, byte[]> GetInt32TestData()
            => new TheoryData<int, byte[]>
            {
                { 0, new byte[] { 0x00, 0x00, 0x00, 0x00 } },
                { 76798590, new byte[] { 0x04, 0x93, 0xDA, 0x7E } },
                { 421101039, new byte[] { 0x19, 0x19, 0x7D, 0xEF } },
                { -629214037, new byte[] { 0xDA, 0x7E, 0xF4, 0xAB } },
                { -1, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF } },
            };

        [Theory]
        [MemberData(nameof(GetByteArrayTestData))]
        public void ByteArrayToPgpWordAccumulationMatchesPgpWordAccumulationBuilder(byte[] bytes)
            => Assert.Equal(BuildPgpWordAccumulation(bytes).Value, ToPgpWordAggregation(bytes).Value);

        [Theory]
        [MemberData(nameof(GetByteArrayTestData))]
        public void PgpWordAccumulationToByteArrayMatchesPgpWordAccumulationParser(byte[] bytes)
        {
            var pgpWordAggregation = BuildPgpWordAccumulation(bytes);
            Assert.Equal(ParsePgpWordAccumulation(pgpWordAggregation), GetBytes(pgpWordAggregation));
        }

        public static TheoryData<byte[]> GetByteArrayTestData()
            => new TheoryData<byte[]>
            {
                new byte[] { 0x00, 0x00, 0x00, 0x00 },
                new byte[] { 0x04, 0x93, 0xDA, 0x7E },
                new byte[] { 0x19, 0x19, 0x7D, 0xEF },
                new byte[] { 0xDA, 0x7E, 0xF4, 0xAB },
                new byte[] { 0xFF, 0xFF, 0xFF, 0xFF },
            };

        private static PgpWordAggregation BuildPgpWordAccumulation(byte[] bytes)
            => new PgpWordAggregationBuilder()
                .Add(bytes)
                .Build();

        private static byte[] ParsePgpWordAccumulation(PgpWordAggregation pgpWordAggregation)
            => new PgpWordAggregationParser()
                .Parse(pgpWordAggregation);
    }
}
