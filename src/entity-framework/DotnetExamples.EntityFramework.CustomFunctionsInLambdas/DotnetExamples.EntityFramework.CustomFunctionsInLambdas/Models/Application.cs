using Version = DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models.ValueObjects.Version;

namespace DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models;

public record Application(string Name, Version Version)
{
    public int Id { get; }
}