using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi().AddControllers();

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

const string DefaultCulture = "en";
var supportedCultures = new[] { "en", "pt" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(DefaultCulture)
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
