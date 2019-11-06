using Xunit;

namespace Messerli.PgpWordList.Test
{
    public class PgpWordAccumulationBuilderTest
    {
        [Theory]
        [ClassData(typeof(PgpWordListTestData))]
        public void GeneratesPgpString(byte[] pgpBytes, string separator, string expectedPgpString)
        {
            var pgpString =
                new PgpWordAccumulationBuilder()
                    .SetSeparator(separator)
                    .Add(pgpBytes)
                    .Build();

            Assert.Equal(expectedPgpString, pgpString.Value);
        }
    }
}
