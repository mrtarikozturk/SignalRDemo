using Microsoft.AspNetCore.ResponseCompression;

namespace SignalRDemo.Hubs
{
    public static class SignalRConfiguration
    {
        public static IServiceCollection AddCustomSignalR(this IServiceCollection services)
        {
            services.AddSignalR();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSignalR(this IApplicationBuilder app)
        {
            app.UseResponseCompression(); // Middleware doğru sırada çalışmalı

            app.UseRouting();             // Eğer başka route'lar da olacaksa
            app.UseAntiforgery();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MyHub>("/chathub");
            });

            return app;
        }
    }
}

