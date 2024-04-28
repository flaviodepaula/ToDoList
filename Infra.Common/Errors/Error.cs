namespace Infra.Common.Errors
{
    public sealed class Error : IEquatable<Error>
    {
        public string Code { get; }
        public string Message { get; }

        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "O Resultado retornado é nulo");

        public static implicit operator string(Error error) => error.Code;

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public bool Equals(Error? other)
        {
            throw new NotImplementedException();
        }
    }
}
