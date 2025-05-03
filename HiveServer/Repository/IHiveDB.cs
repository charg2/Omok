using Shared;

public interface IHiveDB : IDisposable
{
    //public Task< Tuple< ErrorCode, long > > AuthCheck( string userId, string pw );
    public Task< ErrorCode > Login( string userId, string password );
    public Task< ErrorCode > Register( string userId, string password );
    public Task< ErrorCode > VerifyToken( string userId, string password );
}
