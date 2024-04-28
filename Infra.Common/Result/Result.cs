using Infra.Common.Errors;

namespace Infra.Common.Result
{
    public class Result
    {
        public bool IsSucess { get; }
        public bool IsFailure => !IsSucess;
        public Error Error { get; }
        public static Result Sucess() => new(true, Error.None);
        public static Result<T> Sucess<T>(T value) => new(value, true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<T> Failure<T>(Error error) => new(default, false, error);
        public static Result<T> Failure<T>(Error error, T value) => new(value, false, error);

        protected Result(bool isSucess, Error error)
        {
            if (isSucess && error != Error.None)
                throw new InvalidOperationException();

            if (!isSucess && error == Error.None)
                throw new InvalidOperationException();

            IsSucess = isSucess;
            Error = error;
        }
    }
}
