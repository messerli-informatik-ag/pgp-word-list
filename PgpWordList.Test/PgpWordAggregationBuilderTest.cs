using Xunit;

namespace Messerli.PgpWordList.Test
{
    public class PgpWordAggregationBuilderTest
    {
        [Theory]
        [ClassData(typeof(PgpWordListTestData))]
        public void GeneratesPgpString(byte[] pgpBytes, string separator, string expectedPgpString)
        {
            var pgpString =
                new PgpWordAggregationBuilder()
                    .SetSeparator(separator)
                    .Add(pgpBytes)
                    .Build();

            Assert.Equal(expectedPgpString, pgpString.Value);
        }
    }
}
