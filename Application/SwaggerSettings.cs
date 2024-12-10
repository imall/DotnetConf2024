using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
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
        services.AddApiVersioning(setupAction =>
            {
                // 讓 API 回應中包含支援和棄用的版本資訊。
                // api-supported-versions: 2.0
                // api-deprecated-versions: 1.0
                setupAction.ReportApiVersions = true;
                
                // 設定當 Client 端未指定版本時，會使用的預設 API 版本。
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
                
                // 當設為 true 時，若 Client 端未指定版本，系統將使用 DefaultApiVersion 作為預設版本。
                // 若設為 false，則未指定版本的請求可能會收到錯誤回應。
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
            })
            .AddApiExplorer(setupAction =>
            {
                // 將 URL 路徑中的版本 placeholder 替換為實際版本號（如 v{version} 會變成 v1）。
                setupAction.SubstituteApiVersionInUrl = true;
            });

        services.AddSwaggerGen(setupAction =>
        {
            var apiVersionDescriptionProvider = services.BuildServiceProvider()
                .GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                setupAction.SwaggerDoc($"{description.GroupName}",
                    new OpenApiInfo
                    {
                        Title = $"Dotnet conf 2024 範例專案 第{description.GroupName}版",
                        Version = $"{description.GroupName}",
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
            }
            
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
            foreach (var description in app.DescribeApiVersions())
            {
                // UI 讀取 OpenAPI 規格路徑
                setupAction.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"Dotnet conf 2024 範例專案 {description.GroupName}");
            }

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
