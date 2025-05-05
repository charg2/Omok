using GameServer.Repository;
using GameServer.Services;
using GameServer.Services.Interface;
using HiveServer.Services;
using ZLogger;

var builder = WebApplication.CreateBuilder( args );
IConfiguration configuration = builder.Configuration;
builder.Services.Configure< DBConfig >( configuration.GetSection( nameof( DBConfig ) ) );


builder.Services.AddControllers();
builder.Services.AddTransient< IGameDB, GameDB >();
builder.Services.AddTransient< IMemoryDB, MemoryDB >();
builder.Services.AddTransient< IAuthService, AuthService >();
builder.Services.AddTransient< IPlayerService, PlayerService >();

/// Http통신
builder.Services.AddHttpClient< IAuthService, AuthService >();

// 기본 로거 설정 제외
builder.Logging.ClearProviders();
builder.Logging.AddZLoggerConsole();    // 콘솔 출력

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
