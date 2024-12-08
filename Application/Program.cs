using Application;
using Application.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlSerializerFormatters();
builder.Services.AddSingleton<AuthorRepository>();
builder.Services.AddSingleton<BookRepository>();

// 註冊 Swagger 設定
builder.Services.AddSwaggerSettings();

var app = builder.Build();

// 使用 swgger 中介軟體
app.UseSwaggerSettings();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
