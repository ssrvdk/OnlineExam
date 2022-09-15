using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace ExceptionManager
{
    public class BusinessLogicException : Exception
    {
        private List<BusinessError> _businessErrors;

        public BusinessLogicException(BusinessError businessError)
        {
            _businessErrors = new List<BusinessError>();
            _businessErrors.Add(businessError);
        }

        public BusinessLogicException(List<BusinessError> businessErrors)
        {
            _businessErrors = businessErrors;
        }

        public BusinessLogicException(string lbSqlResultXML)
        {
            string _id = "";
            string _code = "", _desc = "";
            DataSet dsBusinessErrors = new DataSet();

            _businessErrors = new List<BusinessError>();

            if (lbSqlResultXML != "")
            {
                //Test this code
                lbSqlResultXML = "<Root>" + lbSqlResultXML + "</Root>";
                //******************

                XmlTextReader _xmlReader = new XmlTextReader(new StringReader(lbSqlResultXML));
                dsBusinessErrors.ReadXml(_xmlReader);
                if (dsBusinessErrors.Tables.Count != 0)
                {
                    for (int i = 0; i < dsBusinessErrors.Tables[0].Rows.Count; i++)
                    {
                        _id = dsBusinessErrors.Tables[0].Rows[i]["EntityId"].ToString();
                        _code = dsBusinessErrors.Tables[0].Rows[i]["ErrorNo"].ToString();
                        _desc = dsBusinessErrors.Tables[0].Rows[i]["ErrorDetail"].ToString();

                        BusinessError _businessError = new BusinessError(_id, _code, _desc);
                        _businessErrors.Add(_businessError);
                    }
                }
                _xmlReader = null;

            } // End if  

            dsBusinessErrors.Dispose();
        }

        public List<BusinessError> BusinessErrors
        {
            get { return _businessErrors; }
        }
    }
}
