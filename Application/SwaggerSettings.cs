using Microsoft.OpenApi.Models;

namespace Application;

public static class SwaggerSettings
{
    /// <summary>
    /// Swagger 相關註冊內容
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwaggerSettings(this IServiceCollection services)
    {
        services.AddSwaggerGen(setupAction =>
        {
            setupAction.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Dotnet conf 2024 範例專案",
                    Version = "v1",
                    Description = "Dotnet conf 2024 Taiwan Swagger 範例",
                    TermsOfService = new Uri("https://dotnetconf.study4.tw/Code-Of-Conduct"),
                    Contact = new OpenApiContact
                    {
                        Name = "Study4tw",
                        Url = new Uri("https://www.facebook.com/groups/study4tw/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    },
                });
        });
    }
    
    /// <summary>
    /// Swagger 中介軟體設定
    /// </summary>
    public static void UseSwaggerSettings(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
