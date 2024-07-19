namespace DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models.ValueObjects;

/// <summary>
/// Value object representing a semantic version.
/// Validation methods are not implemented here for brevity, but usually boxing like this is done
/// in order to add some extra validation to primitive types.
/// </summary>
/// <param name="value">The string representation of a semantic version.</param>
public sealed class Version(string value)
{
    private string Value { get; } = value;

    public override string ToString()
    {
        return Value;
    }
}

public static class VersionExtensions
{
    // This method can have no implementation, if you want it to be called only from Entity Framework
    // or you can provide your own implementation for using it within C#.
    public static bool IsPrerelease(this Version version)
    {
        throw new NotImplementedException(message: "This method is to be used by Entity Framework only.");
    }
}