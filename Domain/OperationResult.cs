using Microsoft.Extensions.Logging;

namespace Domain
{
    public class OperationResult<T>
    {
        public bool IsSuccessful { get; private set; }
        public T Data { get; private set; }
        public string Message { get; private set; }
        public string Details { get; private set; }
        public Guid? KeyNotFoundGuid { get; private set; }
        public bool IsKeyNotFound { get { return KeyNotFoundGuid.HasValue; } }

        private OperationResult(bool isSuccessful, T data, string message, string details, Guid? keyNotFoundGuid)
        {
            IsSuccessful = isSuccessful;
            Data = data;
            Message = message;
            Details = details;
            KeyNotFoundGuid = keyNotFoundGuid;
        }

        static public OperationResult<T> Successful(T data, string details = "", string message = "Operation successful")
        {
            return new OperationResult<T>(true, data, message, details, null);
        }
        static public OperationResult<T> Failure(string details, string message = "Operation failed")
        {
            return new OperationResult<T>(false, default, message, details, null);
        }
        static public OperationResult<T> KeyNotFound(Guid guid, string detailFormat = "Id {0} not found", string message = "Operation failed, Key not found")
        {
            return new OperationResult<T>(false, default, message, String.Format(detailFormat, guid), guid);
        }
    }
}
