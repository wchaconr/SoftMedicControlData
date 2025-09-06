namespace SoftMedicControlData.Shared
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public T? Value { get; }
        public List<string> Errors { get; } = new();

        private Result(bool isSuccess, T? value, IEnumerable<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Value = value;
            if (errors != null)
                Errors.AddRange(errors);
        }

        // ===== Fábricas estáticas =====
        public static Result<T> Success(T value) =>
            new Result<T>(true, value);

        public static Result<T> Failure(string error) =>
            new Result<T>(false, default, new List<string> { error });

        public static Result<T> Failure(IEnumerable<string> errors) =>
            new Result<T>(false, default, errors);

        // ===== Métodos auxiliares =====
        public override string ToString()
        {
            return IsSuccess
                ? $"Success: {Value}"
                : $"Failure: {string.Join(", ", Errors)}";
        }
    }
}
