using Xunit;

namespace Messerli.PgpWordList.Test
{
    public class PgpWordListTestData : TheoryData<byte[], string, string>
    {
        public PgpWordListTestData()
        {
            Add(new byte[] { 0x00, 0x00, 0x00, 0x00 }, "-", "aardvark-adroitness-aardvark-adroitness");
            Add(new byte[] { 0x53, 0x01, 0xE2, 0x49 }, " ", "dwelling adviser tiger dinosaur");
            Add(new byte[] { 0x30, 0x0E, 0x03, 0x0B }, "|", "chairlift|Atlantic|acme|armistice");
            Add(new byte[] { 0x1F, 0xEB, 0x85, 0xFF }, "-", "billiard-underfoot-music-Yucatan");
            Add(new byte[] { 0x1F, 0xEB, 0x85, 0xFF }, "##", "billiard##underfoot##music##Yucatan");
        }
    }
}
