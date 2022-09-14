using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Funcky.Extensions;
using Funcky.Monads;

namespace Messerli.PgpWordList;

public static class PgpWordSequence
{
    private const string DefaultSeparator = "-";

    /// <summary>Converts the bytes to a sequence of PGP words.</summary>
    public static string ToWords(IEnumerable<byte> value, string separator = DefaultSeparator)
        => value.Select(ByteToWord).JoinToString(separator);

    /// <summary>Converts the integer to a sequence of PGP words. The integer is always written in big endian order.</summary>
    public static string ToWords(int value, string separator = DefaultSeparator)
        => ToWords(value.ToBytesBigEndian(), separator);

    /// <summary>Converts the PGP word sequence back to bytes.</summary>
    public static Option<IReadOnlyList<byte>> ToBytesOrNone(string words, string separator = DefaultSeparator)
        => words.Length == 0
            ? ImmutableArray<byte>.Empty
            : words
                .Split(new[] { separator }, StringSplitOptions.None)
                .Select(WordToByte)
                .Sequence();

    /// <summary>Converts the PGP word sequence back to an integer. Big endian order is assumed.</summary>
    public static Option<int> ToInt32OrNone(string words, string separator = DefaultSeparator)
        => ToBytesOrNone(words, separator)
            .Where(static bytes => bytes.Count == Unsafe.SizeOf<int>())
            .Select(static bytes => BinaryPrimitives.ReadInt32BigEndian(bytes.ToArray()));

    private static IEnumerable<byte> ToBytesBigEndian(this int value)
    {
        var bytes = new byte[Unsafe.SizeOf<int>()];
        BinaryPrimitives.WriteInt32BigEndian(bytes, value);
        return bytes;
    }

    private static string ByteToWord(byte @byte, int index)
        => IsEven(index)
            ? PgpWords.ToEvenWord(@byte)
            : PgpWords.ToOddWord(@byte);

    private static Option<byte> WordToByte(string word, int index)
        => IsEven(index)
            ? PgpWords.ToEvenByteOrNone(word)
            : PgpWords.ToOddByteOrNone(word);

    private static bool IsEven(int number) => number % 2 == 0;
}
