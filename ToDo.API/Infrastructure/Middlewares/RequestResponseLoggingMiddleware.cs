using System.Text;
using ToDo.API.Infrastructure.Models.RequestResponseModels;

namespace ToDo.API.Infrastructure.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context.Request);
            Stream originalBody = context.Response.Body;

            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);

                    await LogResponse(context.Response, memStream);

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        private async Task LogRequest(HttpRequest httpRequest)
        {
            var requestModel = new RequestModel
            {
                IP = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString(),
                Scheme = httpRequest.Scheme,
                Host = httpRequest.Host.ToString(),
                IsSecured = httpRequest.IsHttps,
                Method = httpRequest.Method,
                QueryString = httpRequest.QueryString.ToString(),
                Path = httpRequest.Path,
                Body = await ReadRequestBody(httpRequest)
            };

            var logInfo = $"{Environment.NewLine}---- Request Log ----{Environment.NewLine}" +
                $"IP = {requestModel.IP}{Environment.NewLine}" +
                $"Scheme = {requestModel.Scheme}{Environment.NewLine}" +
                $"Host = {requestModel.Host}{Environment.NewLine}" +
                $"IsSecured = {requestModel.IsSecured}{Environment.NewLine}" +
                $"Method = {requestModel.Method}{Environment.NewLine}" +
                $"Query String = {requestModel.QueryString}{Environment.NewLine}" +
                $"Path = {requestModel.Path}{Environment.NewLine}" +
                $"Body = {requestModel.Body}{Environment.NewLine}" +
                $"Request Time = {requestModel.RequestTime}{Environment.NewLine}";

            var completePath = Directory.GetCurrentDirectory() + @"\Infrastructure\Logging\Logs.txt";
            await File.AppendAllTextAsync(completePath, logInfo);
        }
        private async Task LogResponse(HttpResponse httpResponse, MemoryStream memStream)
        {
            memStream.Position = 0;
            string responseBody = new StreamReader(memStream).ReadToEnd();

            var response = new ResponseModel
            {
                ResponseTime = DateTime.Now,
                Status = httpResponse.StatusCode.ToString(),
                Headers = httpResponse.Headers,
                Body = responseBody,
                ContentType = httpResponse.ContentType
            };
            var logInfo = $"{Environment.NewLine}---- Response Log ----{Environment.NewLine}" +
                $"Response Time = {response.ResponseTime}{Environment.NewLine}" +
                $"Status = {response.Status}{Environment.NewLine}" +
                $"Headers = {FormatHeaders(response.Headers)}{Environment.NewLine}" +
                $"Body = {response.Body}{Environment.NewLine}" +
                $"ContentType = {response.ContentType}";

            var completePath = Directory.GetCurrentDirectory() + @"\Infrastructure\Logging\Logs.txt";
            await File.AppendAllTextAsync(completePath, logInfo);
        }
        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            var buffer = new byte[request.ContentLength ?? 0];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;
            return bodyAsText;
        }
        private string FormatHeaders(IHeaderDictionary headers)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var header in headers)
            {
                sb.Append($"Key: {header.Key} | value: {header.Value}\n");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
