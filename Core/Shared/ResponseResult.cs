
namespace Core
{
    public class ResponseResult
    {
        /// <summary>
        /// Empty Ctor
        /// For The Default Use Of ResponseResult
        /// </summary>
        public ResponseResult()
        {
        }

        /// <summary>
        /// Customized To MVC Project
        /// Ctor Take IsSuccess
        /// ApiStatusCode = NULL , ReturnData = NULL , ErrorMessage = NULL
        /// </summary>
        /// <param name="isSucceeded"></param>
        public ResponseResult(bool isSucceeded)
        {
            IsSucceeded = isSucceeded;
            ApiStatusCode = null;
            ErrorMessage = null;
            ReturnData = null;
        }

        /// <summary>
        /// Customized To MVC Project
        /// Ctor Take IsSuccess And ErrorMessage
        /// ApiStatusCode = NULL , ReturnData = NULL
        /// </summary>
        /// <param name="isSucceeded"></param>
        /// <param name="errorMessage"></param>
        public ResponseResult(bool isSucceeded, string errorMessage)
        {
            IsSucceeded = isSucceeded;
            ApiStatusCode = null;
            ErrorMessage = errorMessage;
            ReturnData = null;
        }
        /// <summary>
        /// Ctor Take ReturnDate And StatusCode
        /// IsSuccess = True , ErrorMessage = ""
        /// </summary>
        /// <param name="returnData"></param>
        /// <param name="apiStatusCode"></param>

        public ResponseResult(object returnData, int apiStatusCode)
        {
            IsSucceeded = true;
            ApiStatusCode = apiStatusCode;
            ErrorMessage = "";
            ReturnData = returnData;
        }
        /// <summary>
        /// Ctor Take ErrorMessage And StatusCode
        /// IsSuccess = False , ReturnDate = NULL
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="apiStatusCode"></param>

        public ResponseResult(string errorMessage, int apiStatusCode)
        {
            IsSucceeded = false;
            ApiStatusCode = apiStatusCode;
            ErrorMessage = errorMessage;
            ReturnData = null;
        }
        public bool IsSucceeded { get; set; }
        public int? ApiStatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public object ReturnData { get; set; }
        public object ExtraReturnData { get; set; }

    }
}