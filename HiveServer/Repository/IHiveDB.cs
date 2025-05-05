using Shared;
using System.Data;

public interface IHiveDB : IDisposable
{
    public IDbTransaction BeginTxAsync();
    public Task< ErrorCode > CreateUser( string userId, string password );
    public Task< ErrorCode > CreateToken( string userId );
    public Task< ErrorCode > SaveToken( string userId, string token );
    public Task< ErrorCode > VerifyAccount( string userId, string password );
    public Task< ErrorCode > VerifyToken( string userId, string token );
}
