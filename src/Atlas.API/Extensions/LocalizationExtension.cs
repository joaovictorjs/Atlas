namespace Atlas.API.Extensions
{
    public static class LocalizationExtension
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            const string DefaultCulture = "en";
            var supportedCultures = new[] { "en", "pt" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(DefaultCulture)
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);
            return app;
        }
    }
}
