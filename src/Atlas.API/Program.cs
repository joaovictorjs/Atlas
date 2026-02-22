using Atlas.API.Extensions;
using Atlas.API.Filters.ExceptionFilter;
using Atlas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddOpenApi();
builder.Services.AddControllers(options => options.Filters.Add<CustomExceptionFilter>());
builder.Services.AddDbContext<AtlasDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException("ConnectionStrings:Default is not configured.");
    }
    options.UseNpgsql(connectionString, pgOptions => pgOptions.SetPostgresVersion(9, 5));
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Atlas API")
            .WithClassicLayout()
            .SortTagsAlphabetically()
            .SortOperationsByMethod()
            .PreserveSchemaPropertyOrder();
    });
}

using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetService<AtlasDbContext>();
    context?.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseLocalization();
app.MapControllers();
app.Run();
