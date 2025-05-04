using Shared;

public interface IHiveDB : IDisposable
{
    public Task< ErrorCode > CreateAccount( string userId, string password );
    public Task< ErrorCode > CreateToken( string userId );
    public Task< ErrorCode > SaveToken( string userId, string token );
    public Task< ErrorCode > VerifyAccount( string userId, string password );
    public Task< ErrorCode > VerifyToken( string userId, string token );
}
