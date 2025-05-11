using FakeHiveServer.Services;
using GameServer.Repository;
using GameServer.Services;
using GameServer.Services.Interface;
using System.Runtime.CompilerServices;
using ZLogger;

var builder = WebApplication.CreateBuilder( args );
IConfiguration configuration = builder.Configuration;
builder.Services.Configure< DBConfig >( configuration.GetSection( nameof( DBConfig ) ) );
builder.Services.Configure< ServiceConfig >( configuration.GetSection( nameof( ServiceConfig ) ) );

builder.Services.AddControllers();
builder.Services.AddTransient< IGameDB, GameDB >();
builder.Services.AddTransient< IMemoryDB, MemoryDB >();
builder.Services.AddTransient< IAuthService, AuthService >();
builder.Services.AddTransient< IChatService, ChatService >();
builder.Services.AddTransient< IFriendService, FriendService >();
//builder.Services.AddTransient< IItemService, ItemService >();
builder.Services.AddTransient< IMailService, MailService >();
builder.Services.AddTransient< IMatchService, MatchService >();
builder.Services.AddTransient< IPlayerService, PlayerService >();

/// Http통신
builder.Services.AddHttpClient< IAuthService, AuthService >();
builder.Services.AddHttpClient< IMatchService, MatchService >();

// 기본 로거 설정 제외
builder.Logging.ClearProviders();
builder.Logging.AddZLoggerConsole();    // 콘솔 출력

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
