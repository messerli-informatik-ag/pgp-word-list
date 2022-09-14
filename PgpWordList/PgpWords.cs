using Funcky.Extensions;
using Funcky.Monads;

namespace Messerli.PgpWordList;

public static partial class PgpWords
{
    public static string ToEvenWord(byte @byte) => Data[@byte].Even;

    public static string ToOddWord(byte @byte) => Data[@byte].Odd;

    public static Option<byte> ToEvenByteOrNone(string word)
        => Data
            .FirstOrNone(pair => pair.Value.Even == word)
            .Select(pair => pair.Key);

    public static Option<byte> ToOddByteOrNone(string word)
        => Data
            .FirstOrNone(pair => pair.Value.Odd == word)
            .Select(pair => pair.Key);
}
