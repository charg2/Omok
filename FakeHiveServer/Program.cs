using FakeHiveServer.Repository;
using FakeHiveServer.Services;
using FakeHiveServer.Services.Interface;
using ZLogger;

var builder = WebApplication.CreateBuilder( args );
IConfiguration configuration = builder.Configuration;
builder.Services.Configure< DBConfig >( configuration.GetSection( nameof( DBConfig ) ) );
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient< IUserDB, UserDB >();
builder.Services.AddTransient< IAuthService, AuthService >();
builder.Services.AddTransient< IUserService, UserService >();

// 기본 로거 설정 제외
builder.Logging.ClearProviders();
builder.Logging.AddZLoggerConsole();    // 콘솔 출력

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
