namespace MediSyncHub.Bootstrapper.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await LogRequest(context);
            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            var startTime = DateTime.UtcNow;
            await next(context);
            var endTime = DateTime.UtcNow;

            await LogResponse(context, startTime, endTime);

            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while logging the request/response");
            throw;
        }
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();

        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        var logMessage = $"""
                          Request Information:
                          Schema:{context.Request.Scheme} 
                          Host: {context.Request.Host} 
                          Path: {context.Request.Path} 
                          QueryString: {context.Request.QueryString} 
                          Request Body: {requestBody}
                          """;

        logger.LogInformation(logMessage);
    }

    private async Task LogResponse(HttpContext context, DateTime startTime, DateTime endTime)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        var duration = (endTime - startTime).TotalMilliseconds;

        var logMessage = $"""
                          Response Information:
                          Status Code: {context.Response.StatusCode}
                          Duration: {duration}ms
                          Response Body: {responseBody}
                          """;

        logger.LogInformation(logMessage);
    }
}