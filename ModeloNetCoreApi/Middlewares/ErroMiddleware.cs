using ModeloNetCoreBiblioteca.ClassesAuxiliar;
using System.Text.Json;

namespace ModeloNetCoreApi.Middlewares
{
    public class ErroMiddleware
    {
        private readonly RequestDelegate _next;

        public ErroMiddleware(RequestDelegate next, ILoggerFactory log)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IConfiguration config)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                bool mostrarExcecaoCompleta = Convert.ToBoolean(config.GetSection("MostrarExcecaoCompleta").Value);
                await HandleErrorAsync(httpContext, ex, mostrarExcecaoCompleta);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception, bool mostrarExcecaoCompleta)
        {
            RetornoModelo error;
            if (mostrarExcecaoCompleta && exception.TargetSite.IsSpecialName is false)
                error = new RetornoModelo { Status = false, Mensagem = exception.Message, Exception = $"{exception.Source} | {exception.StackTrace} | {exception.HelpLink}" };
            else
                error = new RetornoModelo { Status = false, Mensagem = exception.Message };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            return;
        }
    }
}
