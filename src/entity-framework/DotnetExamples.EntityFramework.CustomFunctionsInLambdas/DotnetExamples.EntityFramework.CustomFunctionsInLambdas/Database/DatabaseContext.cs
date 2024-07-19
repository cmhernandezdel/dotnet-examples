using System.Data;
using System.Reflection;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Database.Converters;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Version = DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models.ValueObjects.Version;

namespace DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Database;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options): DbContext(options)
{
    private const string ConnectionString = "Server=mysqldb;Port=3306;Database=customfunctionsinlambdas;Uid=root;Pwd=admin;";

    public DbSet<Application> Applications => Set<Application>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString: ConnectionString, serverVersion: ServerVersion.AutoDetect(ConnectionString));
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<Version>().HaveConversion<VersionConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Application>().HasKey(e => e.Id);

        MethodInfo methodInfo = typeof(VersionExtensions)
            .GetMethod(name: nameof(VersionExtensions.IsPrerelease), types: [typeof(Version)])!;
        
        modelBuilder.HasDbFunction(methodInfo: methodInfo,
            builderAction: builder =>
            {
                builder.HasParameter(name: "version",
                        buildAction: parameterBuilder => parameterBuilder.HasStoreType("varchar(32)"))
                    .HasTranslation(args =>
                    {
                        ISqlExpressionFactory sqlExpressionFactory = this.GetService<ISqlExpressionFactory>();
                        SqlConstantExpression likePattern = sqlExpressionFactory.Constant(value: "%-%");
                        SqlUnaryExpression castExpression = sqlExpressionFactory.Convert(
                            operand: args[0],
                            type: typeof(string),
                            typeMapping: new StringTypeMapping(storeType: "varchar", dbType: DbType.String, size: 32));
                        LikeExpression expression =
                            sqlExpressionFactory.Like(match: castExpression, pattern: likePattern);
                        return expression;
                    });
            });

    }
}