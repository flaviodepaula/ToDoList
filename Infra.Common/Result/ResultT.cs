using Infra.Common.Errors;

namespace Infra.Common.Result
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public Result(TValue? value, bool isSucess, Error error) : base(isSucess, error) => _value = value;

        public TValue Value => _value != null ? _value! : throw new InvalidOperationException("Não é possível recuperar o valor da falha");

        public static implicit operator Result<TValue>(TValue? value) => new(value, true, Error.None);
    }
}
