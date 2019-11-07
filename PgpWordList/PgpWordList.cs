using System;
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
            => Find(element => element.Even, word);

        public static byte? FindOdd(string word)
            => Find(element => element.Odd, word);

        private static byte? Find(Func<(string Even, string Odd), string> valueSelector, string word)
            => Data
                .FirstOrDefault(element => valueSelector(element.Value) == word)
                .Key;
    }
}
