using System.Collections.Generic;

namespace ltutton.Monads
{
    public readonly struct Result
    {
        public readonly string Message;

        public readonly bool Success;
        
        internal Result(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public override string ToString()
        {
            return $"Success: {Success} Message: {Message}";
        }

        public static Result Fail(string message)
        {
            return new Result(message, false);
        }

        public static Result<T> Fail<T>(string message, T value)
        {
            return new Result<T>(message, false, value);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(message, false, default);
        }

        public static Result Ok()
        {
            return new Result(string.Empty, true);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(string.Empty, true, value);
        }

        public static IEnumerable<Result> FailIfAny(in IEnumerable<Result> results)
        {
            var fails = new List<Result>();
            var successes = new List<Result>();
            foreach (var result in results)
            {
                if (result.Success)
                {
                    successes.Add(result);
                }
                else
                {
                    fails.Add(result);
                }
            }

            if (fails.Count > 0)
            {
                return fails;
            }

            return successes;
        }

        public static IEnumerable<Result<T>> FailIfAny<T>(in IEnumerable<Result<T>> results)
        {
            var fails = new List<Result<T>>();
            var successes = new List<Result<T>>();
            foreach (var result in results)
            {
                if (result.Success)
                {
                    successes.Add(result);
                }
                else
                {
                    fails.Add(result);
                }
            }

            if (fails.Count > 0)
            {
                return fails;
            }

            return successes;
        }
    }

    public readonly struct Result<T>
    {
        public readonly string Message;
        public readonly T Value;
        public readonly bool Success;

        internal Result(string message, bool success, T value)
        {
            Message = message;
            Value = value;
            Success = success;
        }

        public static explicit operator Result(Result<T> result)
        {
            return new Result(result.Message, result.Success);
        }

        public override string ToString()
        {
            return $"Success: {Success} Message: {Message} Value: {Value}";
        }
    }
}