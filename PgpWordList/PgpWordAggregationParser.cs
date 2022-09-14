using System;
using System.Linq;
using Funcky.Extensions;
using Funcky.Monads;

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
            .GetOrElse(word => throw new ArgumentException($"'{word}' is an invalid pgp word."));

    public Option<byte[]> ParseOrNone(PgpWordAggregation pgpWordAggregation)
        => ParseInternal(pgpWordAggregation).RightOrNone();

    private Either<string, byte[]> ParseInternal(PgpWordAggregation pgpWordAggregation)
        => pgpWordAggregation
            .Value
            .Split(new[] { _separator }, StringSplitOptions.None)
            .Select(WordToByte)
            .Sequence()
            .Select(s => s.ToArray());

    private static Either<string, byte> WordToByte(string word, int index)
        => WordToByte(word, IsEven(index)).ToEither(word);

    private static Option<byte> WordToByte(string word, bool even)
        => even
            ? Option.FromNullable(PgpWordList.FindEven(word))
            : Option.FromNullable(PgpWordList.FindOdd(word));

    private static bool IsEven(int number) => number % 2 == 0;
}
