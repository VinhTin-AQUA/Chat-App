using ChatAppServer.Schema.Types.IType;

namespace ChatAppServer.Schema.Types
{
    public class ResultType
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; } = [];

        //[GraphQLType(typeof(IResultType))]
        public IResultType? Data { get; set; } = null;

        public ResultType(bool success = false, List<string>? errorsMessages = null, IResultType? data = null)
        {
            Success = success;
            ErrorMessages = errorsMessages == null ? new List<string>() : errorsMessages;
            Data = data;
        }
    }
}
