namespace ChatAppServer.Schema.Types
{
    public class ResultType
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; } = [];

        [GraphQLType(typeof(AnyType))]
        public object? Data { get; set; } = null;

        public ResultType(bool success = false, List<string>? errorsMessages = null, object? data = null)
        {
            Success = success;
            ErrorMessages = errorsMessages == null ? new List<string>() : errorsMessages;
            Data = data;
        }
    }
}
