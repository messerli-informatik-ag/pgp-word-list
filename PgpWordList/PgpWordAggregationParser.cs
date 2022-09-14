using System;
using System.Linq;
using Funcky.Extensions;
using Funcky.Monads;
using static Funcky.Functional;

namespace Messerli.PgpWordList;

public class PgpWordAggregationParser
{
    public const string DefaultSeparator = "-";

    private readonly string _separator;

    public PgpWordAggregationParser(string separator = DefaultSeparator)
    {
        _separator = separator;
    }

    public byte[] Parse(PgpWordAggregation pgpWordAggregation)
        => ParseInternal(pgpWordAggregation)
            .Match(
                left: word => throw new ArgumentException($"'{word}' is an invalid pgp word."),
                right: Identity);

    public Option<byte[]> ParseOrNone(PgpWordAggregation pgpWordAggregation)
        => ParseInternal(pgpWordAggregation).Match(_ => Option<byte[]>.None(), Option.Some);

    private Either<string, byte[]> ParseInternal(PgpWordAggregation pgpWordAggregation)
        => pgpWordAggregation
            .Value
            .Split(new[] { _separator }, StringSplitOptions.None)
            .SelectWhileSome(WordToByte);

    private static Option<byte> WordToByte(string word, int index)
        => Option.FromNullable(WordToByte(word, IsEven(index)));

    private static byte? WordToByte(string word, bool even)
        => even
            ? PgpWordList.FindEven(word)
            : PgpWordList.FindOdd(word);

    private static bool IsEven(int number) => number % 2 == 0;
}

internal static class PgpWordArrayExtensions
{
    public static Either<string, byte[]> SelectWhileSome(this string[] elements, Func<string, int, Option<byte>> selector)
        => elements.WithIndex().Aggregate(
            seed: Either<string, byte[]>.Right(new byte[elements.Length]),
            (accumulator, element)
                => accumulator.Match(
                    left: Either<string, byte[]>.Left,
                    right: bytes => selector(element.Value, element.Index).Match(
                        some: @byte =>
                        {
                            bytes[element.Index] = @byte;
                            return Either<string, byte[]>.Right(bytes);
                        },
                        none: Either<string, byte[]>.Left(element.Value))));
}
