using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace ExceptionManager
{
    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class ClientExceptionHandler : IExceptionHandler
    {
        public ClientExceptionHandler(NameValueCollection ignore)
        {
        }

        #region IExceptionHandler Members

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            ValidationException ve = exception as ValidationException;
            if (ve != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (BusinessError be in ve.ValidationErrors)
                {
                    sb.AppendFormat("{0}: {1}", be.Code, be.Description);
                    sb.AppendLine();
                }
                MessageBox.Show(sb.ToString(), "Validation error");
            }
            else
            {
                MessageBox.Show(exception.Message, "Input error");
            }

            return exception;
        }

        #endregion
    }
}
