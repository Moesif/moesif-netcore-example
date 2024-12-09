using Newtonsoft.Json;
namespace MoesifNetCore3Example
{
    public class ExceptionInfo
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
