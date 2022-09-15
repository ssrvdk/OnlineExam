using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionManager
{
    public static class ExceptionPolicyName
    {
        public const string DataAccess = "Data Access Exception Policy";

        public const string ServiceInterface = "Service Interface Exception Policy";

        public const string ClientUserInput = "Client User Input Exception Policy";

        public const string ClientUnhandledException = "Client Unhandled Exception Policy";

        public const string ClientServiceAgent = "Client Service Agent Exception Policy";

    }
}
