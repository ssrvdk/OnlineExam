using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;
using log4net;
using System.Reflection;

namespace ExceptionManager
{
    /// <summary>
    /// Class to handle exceptions generated in any WEB API call
    /// </summary>
    public class CustomHttpExceptionAttribute: ExceptionFilterAttribute
    {
        //Property to log errors
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// Automatically called when there is any exception in WEB API
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is BusinessLogicException) //Handles business logic exceptions of system
            {
                StringBuilder sbErrorContent = new StringBuilder();
                BusinessLogicException blExp = context.Exception as BusinessLogicException;
                foreach(BusinessError be in blExp.BusinessErrors)
                {
                    sbErrorContent.Append(be.Description + ";");
                }
                var error = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                     Content = new StringContent(sbErrorContent.ToString()),
                     ReasonPhrase = "Business Logic Exception"
                 };
                context.Response = error;
            }
            else if(context.Exception is Exception) //Code to handle technical errors
            {
                var error = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Empty),
                    ReasonPhrase = "Internal Server Error"
                };
                //To log unhandled exceptions
                this.log.Error(context.Exception);
                context.Response = error;
            }
        }
    }
}
