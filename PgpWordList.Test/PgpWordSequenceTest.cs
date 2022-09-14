using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Funcky;
using Xunit;

namespace Messerli.PgpWordList.Test;

public sealed class PgpWordSequenceTest
{
    private const string InvalidWord = "invalidword";

    [Theory]
    [MemberData(nameof(BytesTestData))]
    public void ConvertsBytesToWords(byte[] pgpBytes, string separator, string expectedWords)
        => Assert.Equal(expectedWords, PgpWordSequence.ToWords(pgpBytes, separator));

    [Theory]
    [MemberData(nameof(BytesTestData))]
    public void ParsesWordsToBytes(byte[] expectedPgpBytes, string separator, string pgpWordAggregation)
        => Assert.Equal(expectedPgpBytes, FunctionalAssert.Some(PgpWordSequence.ToBytesOrNone(pgpWordAggregation, separator)));

    [Fact]
    public void ReturnsNoneForInvalidBytes()
        => FunctionalAssert.None(PgpWordSequence.ToBytesOrNone($"{InvalidWord}-adroitness-aardvark-adroitness"));

    public static TheoryData<byte[], string, string> BytesTestData()
        => new()
        {
            { new byte[] { 0x00, 0x00, 0x00, 0x00 }, "-", "aardvark-adroitness-aardvark-adroitness" },
            { new byte[] { 0x53, 0x01, 0xE2, 0x49 }, " ", "dwelling adviser tiger dinosaur" },
            { new byte[] { 0x30, 0x0E, 0x03, 0x0B }, "|", "chairlift|Atlantic|acme|armistice" },
            { new byte[] { 0x1F, 0xEB, 0x85, 0xFF }, "-", "billiard-underfoot-music-Yucatan" },
            { new byte[] { 0x1F, 0xEB, 0x85, 0xFF }, "##", "billiard##underfoot##music##Yucatan" },
        };

    [Theory]
    [InlineData(new byte[0])]
    [InlineData(new byte[] { 0 })]
    [InlineData(new byte[] { 0, 0 })]
    [InlineData(new byte[] { 0, 0, 0 })]
    [InlineData(new byte[] { 0, 0, 0, 0, 0 })]
    [InlineData(new byte[] { 0, 0, 0, 0, 0, 0 })]
    public void ReturnsNoneWhenTooFewOrTooManyBytesAreGivenForInt32(byte[] bytes)
        => FunctionalAssert.None(PgpWordSequence.ToInt32OrNone(PgpWordSequence.ToWords(bytes)));

    [Property]
    public Property ConvertsArbitraryBytes(byte[] bytes)
        => PgpWordSequence.ToBytesOrNone(PgpWordSequence.ToWords(bytes))
            .Match(none: false, some: parsed => parsed.SequenceEqual(bytes))
            .ToProperty();

    [Property]
    public Property ConvertsArbitraryIntegers(int value)
        => PgpWordSequence.ToInt32OrNone(PgpWordSequence.ToWords(value))
            .Match(none: false, some: parsed => parsed == value)
            .ToProperty();

    [Theory]
    [MemberData(nameof(Int32TestData))]
    public void WordsGeneratedForIntegerMatchTheWordsForTheirBytes(int int32, byte[] bytes)
        => Assert.Equal(PgpWordSequence.ToWords(bytes), PgpWordSequence.ToWords(int32));

    public static TheoryData<int, byte[]> Int32TestData()
        => new()
        {
            { 0, new byte[] { 0x00, 0x00, 0x00, 0x00 } },
            { 76798590, new byte[] { 0x04, 0x93, 0xDA, 0x7E } },
            { 421101039, new byte[] { 0x19, 0x19, 0x7D, 0xEF } },
            { -629214037, new byte[] { 0xDA, 0x7E, 0xF4, 0xAB } },
            { -1, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF } },
        };
}
