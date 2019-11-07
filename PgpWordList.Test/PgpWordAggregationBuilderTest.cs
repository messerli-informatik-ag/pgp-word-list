using Xunit;

namespace Messerli.PgpWordList.Test
{
    public class PgpWordAggregationBuilderTest
    {
        [Theory]
        [ClassData(typeof(PgpWordListTestData))]
        public void GeneratesPgpWordAggregation(byte[] pgpBytes, string separator, string expectedPgpWordAggregation)
        {
            var pgpWordAggregation =
                new PgpWordAggregationBuilder()
                    .SetSeparator(separator)
                    .Add(pgpBytes)
                    .Build();

            Assert.Equal(expectedPgpWordAggregation, pgpWordAggregation.Value);
        }
    }
}
