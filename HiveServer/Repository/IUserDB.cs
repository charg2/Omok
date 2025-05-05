using Shared;
using System.Data;

public interface IUserDB : IDisposable
{
    public IDbTransaction BeginTxAsync();
    public Task< ErrorCode > CreateUser( string account, string password );
    public Task< ErrorCode > CreateToken( string account );
    public Task< ErrorCode > SaveToken( string account, string token );
    public Task< ErrorCode > VerifyAccount( string account, string password );
    public Task< ( ErrorCode, long userId ) > VerifyToken( string account, string token );
}
