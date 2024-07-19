using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Database;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.UseCases.Shared;
using Microsoft.AspNetCore.Mvc;
using Version = DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models.ValueObjects.Version;

namespace DotnetExamples.EntityFramework.CustomFunctionsInLambdas.UseCases.Applications;

public sealed class CreateApplication
{
    private record Request(string Name, string Version);

    private record Response(int Id, string Name, string Version);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(pattern: "/applications", handler: Handler)
                .WithTags("Applications");
        }
    }

    // Validation is not included here for the sake of brevity, but you could add it
    private static async Task<IResult> Handler([FromBody] Request request, DatabaseContext context, CancellationToken cancellationToken)
    {
        Application application = new(Name: request.Name, Version: new Version(request.Version));
        context.Applications.Add(application);
        await context.SaveChangesAsync(cancellationToken);
        return Results.Ok(
            new Response(
                Id: application.Id,
                Name: application.Name,
                Version: application.Version.ToString()
            )
        );
    }
}