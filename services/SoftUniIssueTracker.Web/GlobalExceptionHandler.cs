
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Web.Mvc;
using ExceptionContext = System.Web.Mvc.ExceptionContext;

namespace SIT.Web
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.Exception.Message);
            context.Result = new ResponseMessageResult(response);
            //var exceptionMessage = new StringContent(context.Exception.Message);
            //context.Result = new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.BadRequest)
            //{
            //    ReasonPhrase = context.Exception.Message
            //});
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        //public override void OnException(ExceptionContext filterContext)
        //{            
        //    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, filterContext.Exception.Message);
        //}
    }
}
