using System;
using Funcky.Monads;

namespace Messerli.PgpWordList;

public static partial class PgpWords
{
    private const int NotFoundIndex = -1;

    public static string ToEvenWord(byte @byte) => Data[@byte].Even;

    public static string ToOddWord(byte @byte) => Data[@byte].Odd;

    public static Option<byte> ToEvenByteOrNone(string word)
        => ToByte(pair => pair.Even == word);

    public static Option<byte> ToOddByteOrNone(string word)
        => ToByte(pair => pair.Odd == word);

    private static Option<byte> ToByte(Predicate<(string Even, string Odd)> predicate)
        => Option.Return(Array.FindIndex(Data, predicate))
            .Where(index => index != NotFoundIndex)
            .Select(Convert.ToByte);
}
