using Newtonsoft.Json;
namespace MoesifNet7Example
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

