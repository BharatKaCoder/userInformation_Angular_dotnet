using System.Net;

namespace ICC_Champion_Trophy_2025
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode {  get; set; }
        public string Message { get; set; }
        public bool success { get; set; }
        public List<string> Error { get; set; }
        public Object result { get; set; }
    }
}
