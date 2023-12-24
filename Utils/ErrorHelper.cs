using fin_manager.Utils.Enum;

namespace fin_manager.Utils
{
    public class ErrorHelper
    {
        public string GetErrorMsg (ApiError typeError, string item)
        {
            switch(typeError)
            {
                case ApiError.NoneWereFound:
                    return $"Error finding {item}.";                
                case ApiError.OneNotFound:
                    return $"{item} not found.";
                case ApiError.NotCreated:
                    return $"{item} not created.";
                case ApiError.NotUptaded:
                    return $"{item} not updated.";
                case ApiError.NotDeleted:
                    return $"{item} not deleted.";
                default:
                    return "Try again later.";

            }
        }
    }
}
