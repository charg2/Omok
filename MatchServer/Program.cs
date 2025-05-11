using MatchServer.Services;
using MatchServer.Services.Interface;
using ZLogger;

var builder = WebApplication.CreateBuilder( args );
IConfiguration configuration = builder.Configuration;
builder.Services.Configure< DBConfig >( configuration.GetSection( nameof( DBConfig ) ) );
builder.Services.Configure< ServiceConfig >( configuration.GetSection( nameof( ServiceConfig ) ) );

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient< IMatchService, MatchService >();

var app = builder.Build();

// Configure the HTTP request pipeline.

// 기본 로거 설정 제외
builder.Logging.ClearProviders();
builder.Logging.AddZLoggerConsole();    // 콘솔 출력

app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
