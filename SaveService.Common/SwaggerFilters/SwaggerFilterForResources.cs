using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

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
