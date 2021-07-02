using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SaveService.Common.SwaggerFilters
{
    public class SwaggerFilterForResources : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.RelativePath.StartsWith("Auth"))
                {
                    swaggerDoc.Paths.Remove("/" + apiDescription.RelativePath.TrimEnd('/'));
                }
            }
        }
    }
}
