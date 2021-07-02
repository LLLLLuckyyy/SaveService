using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SaveService.Common.SwaggerFilters
{
    public class SwaggerFilterForAuth : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.RelativePath.StartsWith("Message")
                    || apiDescription.RelativePath.StartsWith("File"))
                {
                    swaggerDoc.Paths.Remove("/" + apiDescription.RelativePath.TrimEnd('/'));
                }
            }
        }
    }
}
