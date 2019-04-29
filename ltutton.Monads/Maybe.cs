namespace ltutton.Monads
{
    public readonly struct Maybe<T>
    {
        public readonly T Value;

        public readonly bool HasValue;

        internal Maybe(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }

        public override string ToString()
        {
            return $"HasValue: {HasValue} Value: {Value}";
        }
    }

    public class Maybe
    {
        public static Maybe<T> Yes<T>(T value)
        {
            return new Maybe<T>(value, true);
        }

        public static Maybe<T> No<T>()
        {
            return new Maybe<T>(default, false);
        }
    }
}