namespace BlogPlatform.Application.Common
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public static Result<T> Success(T data, string message = "") =>
            new() { Succeeded = true, Data = data, Message = message };

        public static Result<T> Failure(List<string> errors, string message = "") =>
            new() { Succeeded = false, Errors = errors, Message = message };

        public static Result<T> Failure(string error, string message = "") =>
            new() { Succeeded = false, Errors = new List<string> { error }, Message = message };
    }

    public class Result
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        public static Result Success(string message = "") =>
            new() { Succeeded = true, Message = message };

        public static Result Failure(List<string> errors, string message = "") =>
            new() { Succeeded = false, Errors = errors, Message = message };

        public static Result Failure(string error, string message = "") =>
            new() { Succeeded = false, Errors = new List<string> { error }, Message = message };
    }

}
