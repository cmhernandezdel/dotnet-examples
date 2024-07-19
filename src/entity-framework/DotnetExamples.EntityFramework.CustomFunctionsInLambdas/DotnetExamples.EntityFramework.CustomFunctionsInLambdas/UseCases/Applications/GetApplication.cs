using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Database;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models.ValueObjects;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.UseCases.Shared;

namespace DotnetExamples.EntityFramework.CustomFunctionsInLambdas.UseCases.Applications;

public sealed class GetApplication
{
    private record Request(bool IncludePrerelease);
    private record Response(int Id, string Name, string Version);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(pattern: "/applications", handler: Handler)
                .WithTags("Applications");
        }
    }

    private static IResult Handler([AsParameters] Request request, DatabaseContext context,
        CancellationToken cancellationToken)
    {
        IQueryable<Application> applications = context.Applications.AsQueryable();
        if (!request.IncludePrerelease)
        {
            applications = applications.Where(a => !a.Version.IsPrerelease());
        }
        return Results.Ok(applications.Select(a => new Response(a.Id, a.Name, a.Version.ToString())));
    }
}