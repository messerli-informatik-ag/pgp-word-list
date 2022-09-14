using System;
using Funcky.Extensions;

namespace Messerli.PgpWordList;

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
            .FirstOrNone(element => valueSelector(element.Value) == word)
            .Match(
                some: pair => pair.Key,
                none: () => (byte?)null);
}
