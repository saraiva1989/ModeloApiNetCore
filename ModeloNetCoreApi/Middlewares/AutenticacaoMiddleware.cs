using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using ModeloNetCoreBiblioteca.Dominio.Usuario;

namespace ModeloNetCoreApi.Middlewares
{
    public class AutenticacaoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _verbosValidar = new string[]
        {
            "GET",
            "POST",
            "PUT",
            "DELETE"
        };
        public AutenticacaoMiddleware(RequestDelegate next)
        => _next = next;

        public async Task Invoke(HttpContext context, IUsuarioServico usuarioServico)
        {
            var endpoint = context.Features?.Get<IEndpointFeature>()?.Endpoint;
            string chave = string.Empty;
            bool endpointRequerAutorizacao = false;

            if (endpoint is not null && _verbosValidar.Contains(context.Request.Method.ToUpper()))
            {
                endpointRequerAutorizacao
                    = endpoint.Metadata.Any(attribute => attribute is AllowAnonymousAttribute) is false;
            }

            if (endpointRequerAutorizacao is false)
            {
                await _next.Invoke(context);
                return;
            }

            chave = context.Request
                               .Headers
                               .FirstOrDefault(x => x.Key.ToLower().Equals("token"))
                               .Value
                               .ToString();

            if (string.IsNullOrWhiteSpace(chave))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var usuario = await usuarioServico.BuscarTokens(chave);
            if (usuario.Status is false)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            context.Items["usuario"] = usuario.objeto;
            await _next.Invoke(context);
        }
    }
}
