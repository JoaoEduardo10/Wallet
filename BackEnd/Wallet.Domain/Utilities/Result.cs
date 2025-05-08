namespace Wallet.Domain.Utilities
{
    public class Result : ResultBase
    {

        public Result(params string[] errors) : base(errors) { }
        public Result(IEnumerable<string> errors) : base(errors) {}

        public static Result<TValue> ToValue<TValue>(TValue value)
        {
            return new Result<TValue>(value);
        }

        public static Result<TValue> ToError<TValue>(params string[] errors)
        {
            return new Result<TValue>(errors);
        }

        public static Result<TValue> ToError<TValue>(IEnumerable<string> errors)
        {
            return new Result<TValue>(errors);
        }

        public static Result ToError(params string[] errors)
        {
            return new Result(errors);
        }

        public static Result ToSuccess()
        {
            return new Result();
        }
    }

    public class Result<TValue> : Result
    {
        public TValue Value { get; set; }

        public Result(params string[] errors) : base(errors) { }
        public Result(IEnumerable<string> errors) : base(errors) { }
        public Result(List<string> errors) : base(errors) { }


        public Result(TValue value)
        {
            Value = value;
        }

        public static Result<TValue> ToValue(TValue value)
        {
            return new Result<TValue>(value);
        }

        public static new Result<TValue> ToErrors(params string[] errors)
        {
            return new Result<TValue>(errors);
        }

        public static new Result<TValue> ToErrors(IEnumerable<string> errors)
        {
            return new Result<TValue>(errors);
        }

    }
}
