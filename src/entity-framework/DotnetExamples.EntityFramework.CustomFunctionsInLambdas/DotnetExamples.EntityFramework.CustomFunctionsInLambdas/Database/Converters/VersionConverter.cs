using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Version = DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models.ValueObjects.Version;

namespace DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Database.Converters;

public sealed class VersionConverter() : ValueConverter<Version, string>(
    convertToProviderExpression: v => v.ToString(),
    convertFromProviderExpression: dbv => new Version(dbv),
    mappingHints: new ConverterMappingHints(size: 32));