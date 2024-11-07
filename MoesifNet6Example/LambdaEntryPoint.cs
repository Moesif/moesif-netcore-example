using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace MoesifNet6Example
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<Startup>();
        }

        public override async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext lambdaContext)
        {
            var response = await base.FunctionHandlerAsync(request, lambdaContext);

            // Log the response body to Moesif
            response.Body = "{\"message\":\"Lambda is working! Response logged.\"}";

            return response;
        }
    }
}

