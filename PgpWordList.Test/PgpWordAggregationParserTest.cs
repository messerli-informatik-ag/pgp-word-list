using Xunit;

namespace Messerli.PgpWordList.Test
{
    public class PgpWordAggregationParserTest
    {
        [Theory]
        [ClassData(typeof(PgpWordListTestData))]
        public void ParsesPgpWordAggregation(byte[] expectedPgpBytes, string separator, string pgpWordAggregation)
        {
            var pgpBytes =
                new PgpWordAggregationParser(separator)
                    .Parse(new PgpWordAggregation(pgpWordAggregation));

            Assert.Equal(expectedPgpBytes, pgpBytes);
        }
    }
}
