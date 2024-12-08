namespace Domain
{
    public class OperationResult<T>
    {
        public bool IsSuccessful { get; private set; }
        public T Data { get; private set; }
        public string Message { get; private set; }
        public string Details { get; private set; }
        public bool IsKeyNotFound { get; private set; }

        private OperationResult(bool isSuccessful, T data, string message, string details, bool keyNotFound)
        {
            IsSuccessful = isSuccessful;
            Data = data;
            Message = message;
            Details = details;
            IsKeyNotFound = keyNotFound;
        }

        static public OperationResult<T> Successful(T data, string details = "", string message = "Operation successful")
        {
            return new OperationResult<T>(true, data, message, details, false);
        }
        static public OperationResult<T> Failure(string details, string message = "Operation failed")
        {
            return new OperationResult<T>(false, default, message, details, false);
        }
        static public OperationResult<T> KeyNotFound(Guid guid, string detailFormat = "Id {0} not found", string message = "Operation failed, Key not found")
        {
            return new OperationResult<T>(false, default, message, String.Format(detailFormat, guid), true);
        }
    }
}
