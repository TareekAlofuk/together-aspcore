using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace together_aspcore.Shared
{
    public class BadRequestResponse
    {
        public object ErrorCode { set; get; }
        public string Message { set; get; }
        public object Extra { set; get; }


        public static BadRequestObjectResult GenerateBadRequestObject(object errorCode, string message = null,
            object extra = null)
        {
            var model = new BadRequestResponse {ErrorCode = errorCode, Message = message, Extra = extra};
            return new BadRequestObjectResult(model);
        }

        public static BadRequestResponse Create(object errorCode, string message = null, object extra = null)
        {
            return new BadRequestResponse {ErrorCode = errorCode, Message = message, Extra = extra};
        }
    }
}