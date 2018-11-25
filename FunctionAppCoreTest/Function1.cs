
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;

namespace FunctionAppCoreTest
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string value = req.Query["value"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            value = value ?? data?.value;
            int integerValue = 0;
            

            return (int.TryParse(value, out integerValue))
                ? (ActionResult)new OkObjectResult($"Prime test result: {Function1.isPrime(integerValue)}")
                : new BadRequestObjectResult("Please pass an integer parameter on the query string or in the request body");
        }

        private static bool isPrime(int value) {
            if (value <= 1)
            {
                return false;  
            }
            for (int div = 2; div <= Math.Sqrt(value); div++)
            {
                if (value % div == 0) {
                    return false;
                }
            }
            return true;
        }
    }
}
