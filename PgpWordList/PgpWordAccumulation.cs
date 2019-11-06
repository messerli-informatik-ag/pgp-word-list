namespace Messerli.PgpWordList
{
    public class PgpWordAccumulation
    {
        public PgpWordAccumulation(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString()
            => Value;
    }
}
