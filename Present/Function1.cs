using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Present
{
    public static class Function1
    {
        [FunctionName("Function3")]
        public static async Task<HttpResponseMessage> Hello2([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            return req.CreateResponse(HttpStatusCode.OK, "Say hello.", "text/plain");
        }

        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Answer([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "hello/{param}")]HttpRequestMessage req, string param, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            return param == null
                ? req.CreateResponse(HttpStatusCode.OK, "Hallo auch. Wer bist du?", "text/plain")
                : param.Equals("4lyn", System.StringComparison.InvariantCultureIgnoreCase) ? req.CreateResponse(HttpStatusCode.OK, "2W1s", "text/plain")
                : param.Equals("Insomnia", System.StringComparison.InvariantCultureIgnoreCase) ? req.CreateResponse(HttpStatusCode.OK, "T3M", "text/plain")
                : param.Equals("Dave", System.StringComparison.InvariantCultureIgnoreCase) || param.Equals("Jana", System.StringComparison.InvariantCultureIgnoreCase) ? req.CreateResponse(HttpStatusCode.OK, $"Hallo {param}. Was hast du herausgefunden?", "text/plain")
                : req.CreateResponse(HttpStatusCode.OK, $"Hallo {param}.", "text/plain");
        }

        [FunctionName("Function2")]
        public static async Task<HttpResponseMessage> Hello([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "hello")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            return req.CreateResponse(HttpStatusCode.OK, "Hallo auch. Wer bist du?", "text/plain");
        }

        [FunctionName("Mastermind")]
        public static async Task<HttpResponseMessage> MastermindStart([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "mastermind")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            return req.CreateResponse(HttpStatusCode.OK, "Gib mir deinen Tipp: XXXX, X sind Ziffern", "text/plain");
        }

        [FunctionName("Mastermind2")]
        public static async Task<HttpResponseMessage> MastermindTry([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "mastermind/{guess}")]HttpRequestMessage req, string guess, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            if (guess.Length == 4)
            {
                int[] solution = { 4, 2, 3, 6 };

                int[] array = new int[4];
                int count = 0;
                bool good = false;

                foreach (var number in guess)
                {
                    if (count > 3) break;
                    if (int.TryParse(new string(number, 1), out int current))
                    {
                        array[count] = current;
                        if (count == 3)
                        {
                            good = true;
                            break;
                        }
                        count++;
                    }
                    else break;
                }
                if (good)
                {
                    int rightPosition = 0;
                    int rightNumber = 0;

                    int position = 0;

                    foreach (int current in array)
                    {
                        if (solution[position] == current)
                        {
                            rightPosition++;
                            solution[position] = -1;
                        }
                        else if (solution.Contains(current))
                        {
                            rightNumber++;
                            solution[Array.IndexOf(solution, current)] = -1;
                        }
                        position++;
                    }

                    if (rightPosition < 4)
                        return req.CreateResponse(HttpStatusCode.OK, $"Richtige Nummer an der richtigen Position: {rightPosition}\nRichtige Nummer an der falschen Position: {rightNumber}", "text/plain");

                    return req.CreateResponse(HttpStatusCode.OK, "bit.ly", "text/plain");
                }
            }
            return req.CreateResponse(HttpStatusCode.OK, "Gib mir deinen Tipp: XXXX, X sind Ziffern", "text/plain");
        }
    }
}
