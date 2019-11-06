using Xunit;

namespace Messerli.PgpWordList.Test
{
    public class PgpWordAccumulationParserTest
    {
        [Theory]
        [ClassData(typeof(PgpWordListTestData))]
        public void ParsePgpWordAccumulation(byte[] expectedPgpBytes, string separator, string pgpString)
        {
            var pgpBytes =
                new PgpWordAccumulationParser(separator)
                    .Parse(new PgpWordAccumulation(pgpString));

            Assert.Equal(expectedPgpBytes, pgpBytes);
        }
    }
}
