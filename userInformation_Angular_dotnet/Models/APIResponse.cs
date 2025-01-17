using System.Net;

namespace userInformation_Angular_dotnet.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
