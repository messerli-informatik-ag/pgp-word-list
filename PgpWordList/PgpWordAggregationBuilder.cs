using System.Collections.Immutable;
using System.Linq;

namespace Messerli.PgpWordList;

public class PgpWordAggregationBuilder
{
    public const string DefaultSeparator = "-";

    private readonly ImmutableList<byte> _byteList;

    private readonly string _separator;

    public PgpWordAggregationBuilder()
    {
        _byteList = ImmutableList<byte>.Empty;
        _separator = DefaultSeparator;
    }

    private PgpWordAggregationBuilder(string separator, ImmutableList<byte> byteList)
    {
        _separator = separator;
        _byteList = byteList;
    }

    public PgpWordAggregationBuilder SetSeparator(string separator)
        => new PgpWordAggregationBuilder(separator, _byteList);

    public PgpWordAggregationBuilder Add(byte @byte)
        => new PgpWordAggregationBuilder(_separator, _byteList.Add(@byte));

    public PgpWordAggregationBuilder Add(byte[] byteArray)
        => new PgpWordAggregationBuilder(_separator, _byteList.AddRange(byteArray));

    public PgpWordAggregation Build()
        => new PgpWordAggregation(string.Join(_separator, _byteList.Select(ByteToWord)));

    private static string ByteToWord(byte @byte, int index)
        => IsEven(index)
            ? PgpWordList.MapEven(@byte)
            : PgpWordList.MapOdd(@byte);

    private static bool IsEven(int number) => number % 2 == 0;
}
