namespace EInvoiceSolution.Core.Shared
{
    public class OperationalResult<T>
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }

        public static OperationalResult<T> Ok(T data, string? message = null) =>
            new OperationalResult<T> { Success = true, Data = data, Message = message };

        public static OperationalResult<T> Fail(string message, List<string>? errors = null) =>
            new OperationalResult<T> { Success = false, Message = message, Errors = errors };
    }

}
