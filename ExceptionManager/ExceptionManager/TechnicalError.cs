using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ExceptionManager
{

    [DataContract(Namespace = "Microsoft.Patterns.Exceptions")]
    public class TechnicalError
    {
        private Guid _errorId;

        [DataMember]
        public Guid ErrorId
        {
            get { return _errorId; }
            set { _errorId = value; }
        }
    }
}
