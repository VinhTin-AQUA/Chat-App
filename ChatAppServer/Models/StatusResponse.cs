namespace ChatAppServer.Models
{
    public class StatusResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public StatusResponse(bool success, List<string> errors)
        {
            Success = success;
            Errors = errors;
        }
    }
}
