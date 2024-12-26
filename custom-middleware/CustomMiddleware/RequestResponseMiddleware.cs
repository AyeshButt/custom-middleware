namespace custom_middleware.CustomMiddleware
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public RequestResponseMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var requestExtension = context.Request.Path.Value ?? null;

            if (requestExtension is not null && requestExtension.EndsWith(".css"))
            {
                await _requestDelegate(context);
                return;
            }

            var LogInfo = new LogInfo();
            
            Console.WriteLine($"Id: {LogInfo.Id}");

            Console.WriteLine($"Incoming request {context.Request.Path} - Request Date Time: {LogInfo.RequestDateTime}");

            await _requestDelegate(context);

            LogInfo.ResponseDateTime = DateTime.Now;

            Console.WriteLine($"Outgoing response {context.Response.StatusCode} - Response Date Time: {LogInfo.ResponseDateTime}");
        }
    }
}

public class LogInfo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime RequestDateTime { get; set; } = DateTime.Now;
    public DateTime ResponseDateTime { get; set; }
}