using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Zigzag.Application;

namespace Zigzag.Api;

public class AddSwaggerResponseExamples : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {

        // Handle different responses for different endpoints
        switch (operation.OperationId)
        {
            case "GetBooks":
                ApplyGetBooksExample(operation);
                break;

            case "Post":
                ApplyAddBookExample(operation);
                break;

            case "GetBook":
                ApplyGetByIdBookExample(operation);
                break;

            case "Put":
                ApplyUpdateBookExample(operation);
                break;
        }
        
    }

    private void ApplyGetBooksExample(OpenApiOperation operation)
    {
        var response200 = operation.Responses.FirstOrDefault(r => r.Key == "200");
        if (response200.Value != null)
        {
            if (!response200.Value.Content.ContainsKey("application/json"))
            {
                response200.Value.Content.Add("application/json", new OpenApiMediaType());
            }

            var example = new OpenApiArray
            {
                new OpenApiObject
                {
                    ["id"] = new OpenApiInteger(0),
                    ["title"] = new OpenApiString("string"),
                    ["author"] = new OpenApiString("string"),
                    ["isbn"] = new OpenApiString("string"),
                    ["pblishedDate"] = new OpenApiString("2024-08-19")
                }
            };
            response200.Value.Content["application/json"].Example = example;
        }
    }

    private void ApplyAddBookExample(OpenApiOperation operation)
    {
        var response201 = operation.Responses.FirstOrDefault(r => r.Key == "201");
        if (response201.Value != null)
        {
            if (!response201.Value.Content.ContainsKey("application/json"))
            {
                response201.Value.Content.Add("application/json", new OpenApiMediaType());
            }

            response201.Value.Content["application/json"].Example = new OpenApiObject
            {
                ["id"] = new OpenApiInteger(0),
                ["title"] = new OpenApiString("string"),
                ["author"] = new OpenApiString("string"),
                ["isbn"] = new OpenApiString("string"),
                ["pblishedDate"] = new OpenApiString("2024-08-19")
            };
        }
    }

    private void ApplyGetByIdBookExample(OpenApiOperation operation)
    {
        var response200 = operation.Responses.FirstOrDefault(r => r.Key == "201");
        if (response200.Value != null)
        {
            if (!response200.Value.Content.ContainsKey("application/json"))
            {
                response200.Value.Content.Add("application/json", new OpenApiMediaType());
            }

            response200.Value.Content["application/json"].Example = new OpenApiObject
            {
                ["id"] = new OpenApiInteger(0),
                ["title"] = new OpenApiString("string"),
                ["author"] = new OpenApiString("string"),
                ["isbn"] = new OpenApiString("string"),
                ["pblishedDate"] = new OpenApiString("2024-08-19")
            };
        }

        var response404 = operation.Responses.FirstOrDefault(r => r.Key == "404");
        if (response404.Value != null)
        {
            response404.Value.Description = "No books found";
        }
    }

    private void ApplyUpdateBookExample(OpenApiOperation operation)
    {
        var response200 = operation.Responses.FirstOrDefault(r => r.Key == "201");
        if (response200.Value != null)
        {
            if (!response200.Value.Content.ContainsKey("application/json"))
            {
                response200.Value.Content.Add("application/json", new OpenApiMediaType());
            }

            response200.Value.Content["application/json"].Example = new OpenApiObject
            {
                ["id"] = new OpenApiInteger(0),
                ["title"] = new OpenApiString("string"),
                ["author"] = new OpenApiString("string"),
                ["isbn"] = new OpenApiString("string"),
                ["pblishedDate"] = new OpenApiString("2024-08-19")
            };
        }

        var response404 = operation.Responses.FirstOrDefault(r => r.Key == "404");
        if (response404.Value != null)
        {
            response404.Value.Description = "No books found";
        }
    }
}
