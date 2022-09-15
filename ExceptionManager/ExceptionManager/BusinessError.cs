using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace ExceptionManager
{
    [DataContract(Namespace = "Microsoft.Patterns.Exceptions")]
    public class BusinessError
    {
        private static BusinessError _success = new BusinessError("SUCCESS", "Success");

        public static BusinessError Success
        {
            get
            {
                return _success;
            }
        }

        public BusinessError(string code, string description)
        {
            _code = code;
            _description = description;
        }

        public BusinessError(string id, string code, string description)
        {
            _id = id;
            _code = code;
            _description = description;
        }

        private string _id;

        [DataMember]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _code;

        [DataMember]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _description;

        [DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
