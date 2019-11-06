using System.Linq;

namespace Messerli.PgpWordList
{
    public static partial class PgpWordList
    {
        public static string MapEven(byte @byte)
            => Data[@byte].Even;

        public static string MapOdd(byte @byte)
            => Data[@byte].Odd;

        public static byte? FindEven(string word)
            => Data.FirstOrDefault(element => element.Value.Even.Equals(word)).Key;

        public static byte? FindOdd(string word)
            => Data.FirstOrDefault(element => element.Value.Odd.Equals(word)).Key;
    }
}
