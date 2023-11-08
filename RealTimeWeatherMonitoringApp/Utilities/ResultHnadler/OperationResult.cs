namespace RealTimeWeatherMonitoringApp.Utilities.ResultHnadler
{
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; } = new object();
        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public OperationResult(bool isSuccess, string message, object data) : this(isSuccess, message)
        {
            Data = data;
        }
        public static OperationResult SuccessResult(string msg)
        {
            return new OperationResult(true, msg);
        }
        public static OperationResult FailureResult(string msg)
        {
            return new OperationResult(false, msg);
        }
        public static OperationResult SuccessDataMessage(string msg, object data)
        {
            return new OperationResult(true, msg, data);
        }
        public static OperationResult FailureDataMessage(string msg, object data)
        {
            return new OperationResult(false, msg, data);
        }
    }
}

