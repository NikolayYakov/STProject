using STProject.Singleton;

namespace STProject.DTOs
{
    public class BaseResponseDTO
    {
        public BaseResponseDTO(ErrorCode error)
        {
            ErrorCode = (int)error;
            Success = ErrorCode == (int)Singleton.ErrorCode.OK;

            if (!ErrorCodes.Messages.TryGetValue(ErrorCode, out var message))
                message = "ERROR: UNPARSABLE ERROR";

            ErrorString = message;
        }
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorString { get; set; }
        
    }
}
