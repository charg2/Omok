namespace Shared;

public enum ErrorCode
{
    None,

    /// <summary>
    /// 하이브 로그인
    /// </summary>
    HiveLoginNullUserId,
    HiveLoginInvalidPassword,
    HiveLoginExceptionOccur,
    HiveLoginSaveTokenFail,

    /// <summary>
    /// 하이브 토큰 인증
    /// </summary>
    HiveVerifyTokenExecptionOccur,
    HiveVerifyTokenNullToken,

    UnknownError,
    HiveLoginCreateTokenFail,
    HiveLoginCreateAccountFail,
}

public static class ErrorCodeEx
{
    public static bool HasError( ref this ErrorCode errorCode )
        => errorCode != ErrorCode.None;
}
