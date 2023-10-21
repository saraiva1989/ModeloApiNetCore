using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ModeloNetCoreApi.Configuracacoes
{
    public class TokenHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation is null)
                throw new Exception("operação inválida");
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Name = "chave",
                Description = "token para autenticar a API",
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }

}
