using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Application;

/// <summary>
/// Swagger 設定
/// </summary>
public static class SwaggerSettings
{
    /// <summary>
    /// Swagger 相關註冊內容
    /// </summary>
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

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            // 第二個參數設為 true，可開啟 Controller 的說明
            setupAction.IncludeXmlComments(xmlPath, true);
        });
    }

    /// <summary>
    /// Swagger 中介軟體設定
    /// </summary>
    public static void UseSwaggerSettings(this WebApplication app)
    {
        app.UseSwagger(setupAction =>
        {
            // Open API 規格進入點，如果不需要異動，可以不用設定
            setupAction.RouteTemplate = "swagger/{documentName}/swagger.json";
        });

        app.UseSwaggerUI(setupAction =>
        {
            // UI 讀取 OpenAPI 規格路徑
            setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet conf 2024 範例專案");

            // UI 進入點
            setupAction.RoutePrefix = string.Empty;
            
            // 顯示請求時間
            setupAction.DisplayRequestDuration(); 
            
            // 顯示 Model 而非範例
            setupAction.DefaultModelRendering(ModelRendering.Model); 
            
            // 啟用深度連結 (錨點連結)
            setupAction.EnableDeepLinking();
            
            // 啟用篩選框
            setupAction.EnableFilter(); 
            
            // 模型展開深度，0 表示不展開，預設為 1
            setupAction.DefaultModelExpandDepth(3); 
            
            // 文件展開設定
            setupAction.DocExpansion(DocExpansion.List);
            
        });
    }
}
