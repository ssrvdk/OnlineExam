using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionManager
{
    // TODO: This is not a fully implemented exception, see Microsoft Design Guidelines for information.

    public class ValidationException : Exception
    {
        private List<BusinessError> _validationErrors;

        public ValidationException(BusinessError validationError)
        {
            _validationErrors = new List<BusinessError>();
            _validationErrors.Add(validationError);
        }

        public ValidationException(List<BusinessError> validationErrors)
        {
            _validationErrors = validationErrors;
        }

        public List<BusinessError> ValidationErrors
        {
            get { return _validationErrors; }
        }
    }
}
