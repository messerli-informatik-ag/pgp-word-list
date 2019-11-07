namespace Messerli.PgpWordList
{
    public class PgpWordAggregation
    {
        public PgpWordAggregation(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString()
            => Value;
    }
}
