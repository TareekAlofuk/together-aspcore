namespace together_aspcore.App.Service
{
    public enum ServiceErrorCode
    {
        NOT_ALLOWED = 201,
        NOT_ENOUGH_IN_STORE = 202,
        SERVICE_NOT_FOUND = 203,
        CANNOT_DECREASE_SERVICE = 204,
        CANNOT_UNREGISTER_UNLIMITED_SERVICE = 205,
        MEMBER_IS_ARCHIVED = 206,
        MEMBER_IS_DISABLED = 207
    }
}