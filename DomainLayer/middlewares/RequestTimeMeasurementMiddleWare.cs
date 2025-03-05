using System.Diagnostics;

namespace DomainLayer.middlewares
{
    public class RequestTimeMeasurementMiddleWare
    {

        private readonly RequestDelegate _next;
        public RequestTimeMeasurementMiddleWare(RequestDelegate next)
        {
            _next = next;
        }



        public async Task Invoke(HttpContext http)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await _next(http);
            stopWatch.Stop();
            Console.WriteLine($"Time The Request Took is {stopWatch.ElapsedMilliseconds}");
        }
    }
}
