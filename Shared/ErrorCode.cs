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
    HiveVerifyTokenInvalid,

    HiveLoginCreateTokenFail,
    HiveLoginCreateAccountFail,

    /// <summary>
    /// 게임 플레이어 관련
    /// </summary>
    GamePlayerIsNull,
    GamePlayerAlreadyExists,

    /// <summary>
    /// 메일 시스템
    /// </summary>
    GameMailSendFail,

    /// <summary>
    /// 친구 시스템
    /// </summary>
    GameFriendAddFail,
    GameFriendRemoveFail,

    UnknownError,
    InvalidUserId,
}

public static class ErrorCodeEx
{
    public static bool IsSuccess( ref this ErrorCode errorCode )
        => errorCode == ErrorCode.None;
}
