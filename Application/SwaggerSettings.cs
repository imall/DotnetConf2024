namespace Application;

public static class SwaggerSettings
{
    /// <summary>
    /// Swagger 相關註冊內容
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwaggerSettings(this IServiceCollection services)
    {
        services.AddSwaggerGen();
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
