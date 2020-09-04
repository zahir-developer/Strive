using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperAdmin.API
{
    public class ApplySwaggerImplementationNotesFilterAttributes : IOperationFilter
    {
        public void Apply(Microsoft.AspNetCore.JsonPatch.Operations.Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            //var attr = apiDescription.GetControllerAndActionAttributes<SwaggerImplementationNotesAttribute>().FirstOrDefault();
            //if (attr != null)
            //{
            //    operation.description = attr.ImplementationNotes;
            //}
        }

        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class ApplySwaggerOperationSummaryFilterAttributes : IOperationFilter
    {
        public void Apply(Microsoft.AspNetCore.JsonPatch.Operations.Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            //var attr = apiDescription.GetControllerAndActionAttributes<SwaggerOperationSummaryAttribute>().FirstOrDefault();
            //if (attr != null)
            //{
            //    operation.summary = attr.OperationSummary;
            //}
        }

        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
