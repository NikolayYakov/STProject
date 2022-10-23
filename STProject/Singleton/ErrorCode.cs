namespace STProject.Singleton
{
    public enum ErrorCode
    {
        OK = 0,
        DefaultError = -1,
        AccessDenied = -1001,
        InvalidParameters = -1003,
    }

    public static class ErrorCodes
    {
        public static Dictionary<int, string> Messages { get; set; } = new Dictionary<int, string>()
        {
            [(int)ErrorCode.OK] = "OK",
            [(int)ErrorCode.DefaultError] = "ERROR: INTERNAL EXCEPTION",
            [(int)ErrorCode.AccessDenied] = "ERROR: ACCESS DENIED",
            [(int)ErrorCode.InvalidParameters] = "ERROR: INVALID PARAMETERS",
        };
    }
}
