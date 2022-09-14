using System;
using Funcky.Xunit;
using Xunit;

namespace Messerli.PgpWordList.Test;

public class PgpWordAggregationParserTest
{
    private const string InvalidWord = "invalidword";

    [Theory]
    [ClassData(typeof(PgpWordListTestData))]
    public void ParsesPgpWordAggregationCorrectly(byte[] expectedPgpBytes, string separator, string pgpWordAggregation)
    {
        var pgpBytes = new PgpWordAggregationParser(separator)
            .Parse(new PgpWordAggregation(pgpWordAggregation));

        Assert.Equal(expectedPgpBytes, pgpBytes);
    }

    [Theory]
    [ClassData(typeof(PgpWordListTestData))]
    public void ParsesPgpWordAggregationIntoOptionCorrectly(byte[] expectedPgpBytes, string separator, string pgpWordAggregation)
    {
        var pgpBytes = new PgpWordAggregationParser(separator)
            .ParseOrNone(new PgpWordAggregation(pgpWordAggregation));

        Assert.Equal(expectedPgpBytes, FunctionalAssert.IsSome(pgpBytes));
    }

    [Fact]
    public void ThrowsExceptionWhenParsingInvalidPgpWords()
    {
        var exception = Assert.Throws<ArgumentException>(() => new PgpWordAggregationParser()
            .Parse(new PgpWordAggregation($"{InvalidWord}-adroitness-aardvark-adroitness")));
        Assert.Equal($"'{InvalidWord}' is an invalid pgp word.", exception.Message);
    }

    [Fact]
    public void ReturnsNoneWhenParsingInvalidPgpWords()
        => FunctionalAssert.IsNone(new PgpWordAggregationParser()
            .ParseOrNone(new PgpWordAggregation($"{InvalidWord}-adroitness-aardvark-adroitness")));
}
