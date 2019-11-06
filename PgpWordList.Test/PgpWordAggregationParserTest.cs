using Xunit;

namespace Messerli.PgpWordList.Test
{
    public class PgpWordAggregationParserTest
    {
        [Theory]
        [ClassData(typeof(PgpWordListTestData))]
        public void ParsePgpWordAccumulation(byte[] expectedPgpBytes, string separator, string pgpString)
        {
            var pgpBytes =
                new PgpWordAggregationParser(separator)
                    .Parse(new PgpWordAggregation(pgpString));

            Assert.Equal(expectedPgpBytes, pgpBytes);
        }
    }
}
