using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.IO;
using ExceptionManager;

namespace ELHelper
{
    //=========================================================================
    /// <summary>
    /// Lb Data access healper class
    /// </summary>
    //========================================================================= 
    public class ELHelper
    {
        //=====================================================================
        // Declarations
        //=====================================================================
        Database _lbDatabase;
        DbCommand _LbCommand;
        IDataReader _lbReader;
        DataSet _lbDataset;
        Object _lbReturnValue;
        int _lbRowsEffected;
        bool _maintainTransaction;
        object _lbSqlResult;

        enum _execute
        {
            NonQuery = 1,
            Scalar = 2,
            Reader = 3,
            Dataset = 4
        }

        //=====================================================================
        /// <summary>
        /// Execute the stored procedure with the parmeters passed, and retuns IDataReader
        /// </summary>
        /// <param name="sprocName">Stored Procedure Name</param>
        /// <param name="parameterlist">list of stored procedure parameter 
        /// (Array of LbSprocParameter object)</param>
        //=====================================================================
        private void Execute(string sprocName,
            LbSprocParameter[] parameterlist,
            _execute execute)
        {
            //=====================================================================
            // Create connection
            //=====================================================================
            _lbDatabase = DatabaseFactory.CreateDatabase();
            //_LbCommand.CommandTimeout = 60;
            //=====================================================================
            // Create command
            //=====================================================================
            _LbCommand = _lbDatabase.GetStoredProcCommand(sprocName);
            _LbCommand.CommandTimeout = 0;

            if (parameterlist != null && parameterlist.Length > 0)
            {
                //=================================================================
                // For the stored procedure parameters
                //=================================================================
                foreach (LbSprocParameter parameter in parameterlist)
                {
                    switch (parameter.Direction)
                    {
                        case LbSprocParameter.LbParameterDirection.INPUT:
                            _lbDatabase.AddInParameter(_LbCommand, parameter.Name,
                                parameter.DataType, parameter.Value);
                            break;
                        case LbSprocParameter.LbParameterDirection.OUTPUT:
                            _lbDatabase.AddOutParameter(_LbCommand, parameter.Name,
                                parameter.DataType, parameter.Size);
                            break;
                    }
                }
            }

            try
            {
                //=============================================================
                // Create the transaction 
                //=============================================================
                if (_maintainTransaction)
                {

                    using (TransactionScope scope = new TransactionScope())
                    {
                        //=============================================================
                        // Execute the command
                        //=============================================================
                        switch (execute)
                        {
                            case _execute.NonQuery:
                                _lbRowsEffected = _lbDatabase.ExecuteNonQuery(_LbCommand);
                                break;
                            case _execute.Scalar:
                                _lbReturnValue = _lbDatabase.ExecuteScalar(_LbCommand);
                                break;
                            case _execute.Reader:
                                _lbReader = _lbDatabase.ExecuteReader(_LbCommand);
                                break;
                            case _execute.Dataset:
                                _lbDataset = _lbDatabase.ExecuteDataSet(_LbCommand);
                                break;
                        }

                        //=====================================================
                        // Commit the transaction if no execution errors
                        // NOTE:If there is any exception in the above execution block
                        //      it will be fall in catch block and the transaction
                        //      will automatically rolled back.
                        //=====================================================
                        scope.Complete();
                    }

                }
                else
                {
                    //=============================================================
                    // Execute the command
                    //=============================================================
                    switch (execute)
                    {
                        case _execute.NonQuery:
                            _lbRowsEffected = _lbDatabase.ExecuteNonQuery(_LbCommand);
                            break;
                        case _execute.Scalar:
                            _lbReturnValue = _lbDatabase.ExecuteScalar(_LbCommand);
                            break;
                        case _execute.Reader:
                            _lbReader = _lbDatabase.ExecuteReader(_LbCommand);
                            break;
                        case _execute.Dataset:
                            _lbDataset = _lbDatabase.ExecuteDataSet(_LbCommand);
                            break;

                    }
                }

                //=============================================================
                // Check for any business exception from stored sproc
                //=============================================================
                _lbSqlResult = GetParameterValue("BSErrorResult");

                if (_lbSqlResult != null && _lbSqlResult.ToString() != "")
                {
                    throw new BusinessLogicException(_lbSqlResult.ToString());
                }
            }
            catch (SqlException exp)
            {

                if (ExceptionPolicy.HandleException(exp, ExceptionPolicyName.DataAccess))
                    throw;
            }
        }

        //=====================================================================
        /// <summary>
        /// Execute the stored procedure with the parmeters passed, returns rows effected
        /// </summary>
        /// <param name="sprocName">Stored Procedure Name</param>
        /// <param name="parameterlist">list of stored procedure parameter 
        /// (Array of LbSprocParameter object)</param>
        //=====================================================================
        public int ExecuteNonQuery(string sprocName, LbSprocParameter[] parameterlist)
        {

            Execute(sprocName, parameterlist, _execute.NonQuery);
            return _lbRowsEffected;
        }

        //=====================================================================
        /// <summary>
        /// Execute the stored procedure with the parmeters passed, and retun
        /// of flbt column in the flbt row of the resultset.
        /// </summary>
        /// <param name="sprocName">Stored Procedure Name</param>
        /// <param name="parameterlist">list of stored procedure parameter 
        /// (Array of LbSprocParameter object)</param>
        //=====================================================================
        public object ExecuteScalar(string sprocName, LbSprocParameter[] parameterlist)
        {
            Execute(sprocName, parameterlist, _execute.Scalar);
            return _lbReturnValue;
        }

        //=====================================================================
        /// <summary>
        /// Execute the stored procedure with the parmeters passed, and retuns IDataReader
        /// </summary>
        /// <param name="sprocName">Stored Procedure Name</param>
        /// <param name="parameterlist">list of stored procedure parameter 
        /// (Array of LbSprocParameter object)</param>
        //=====================================================================
        public IDataReader ExecuteReader(string sprocName, LbSprocParameter[] parameterlist)
        {
            Execute(sprocName, parameterlist, _execute.Reader);
            return _lbReader;
        }

        //=====================================================================
        /// <summary>
        /// Execute the stored procedure with the parmeters passed, and retuns Dataset
        /// </summary>
        /// <param name="sprocName">Stored Procedure Name</param>
        /// <param name="parameterlist">list of stored procedure parameter 
        /// (Array of LbSprocParameter object)</param>
        //=====================================================================
        public DataSet ExecuteDataset(string sprocName, LbSprocParameter[] parameterlist)
        {
            //if (!File.Exists(URLHelper.GetApplicationRootPath() + "Log.txt"))
            //{
            //    File.Create(URLHelper.GetApplicationRootPath() + "Log.txt");
            //}
            //if (sprocName.ToLower() == "status_getuserstatusupdates")
            //{
            //    System.IO.StreamWriter file = new System.IO.StreamWriter(URLHelper.GetApplicationRootPath() + "Log.txt", true);
            //    file.WriteLine(sprocName + "-------------" + DateTime.Now.ToString());
            //    Execute(sprocName, parameterlist, _execute.Dataset);
            //    file.WriteLine(sprocName + "-------------" + DateTime.Now.ToString());
            //    file.Close();
            //}
            //else { 
            Execute(sprocName, parameterlist, _execute.Dataset);
            //}
            return _lbDataset;
        }

        //=====================================================================
        /// <summary>
        /// Gets the parameter value
        /// </summary>
        /// <param name="ParameterName">Name of the Parameter</param>        
        //=====================================================================
        public object GetParameterValue(string ParameterName)
        {
            //=================================================================
            // 'Parameters.Contains' method requires exact name of the parameter
            // as in the stored procedure. (ie. '@LbSQLResult')
            //=================================================================
            if (!ParameterName.StartsWith("@"))
            {
                ParameterName = "@" + ParameterName;
            }

            if (_LbCommand.Parameters.Contains(ParameterName))
            {
                return _lbDatabase.GetParameterValue(_LbCommand, ParameterName);
            }
            else
            {
                return null;
            }

        }

        //=====================================================================
        /// <summary>
        /// Gets or Sets the flag, if the transaction is to be maintained
        /// </summary>
        //=====================================================================
        public bool MaintainTransaction
        {
            get
            {
                return _maintainTransaction;
            }
            set
            {
                _maintainTransaction = value;
            }

        }

    }

    //=====================================================================
    /// <summary>
    /// Lb Stored procedure parameter class
    /// </summary>
    //=====================================================================
    public class LbSprocParameter
    {
        //=====================================================================
        // Class memebers
        //=====================================================================
        string _name;
        DbType _dbType;
        LbParameterDirection _direction;
        Object _value;
        int _size;

        public enum LbParameterDirection
        {
            INPUT = 1,
            OUTPUT = 2
        }

        //=====================================================================
        // Class constructure - Input parameter
        //=====================================================================
        public LbSprocParameter(string sProcName,
                                    DbType dataType,
                                    LbParameterDirection direction,
                                    Object value)
        {
            _name = sProcName;
            _dbType = dataType;
            _direction = direction;
            _value = value;
        }


        //=====================================================================
        // Class constructure - output parameter
        //=====================================================================
        public LbSprocParameter(string sProcName,
                                    DbType dataType,
                                    LbParameterDirection direction,
                                    int size)
        {
            _name = sProcName;
            _dbType = dataType;
            _direction = direction;

            //=====================================================================
            // In case the value parameter is int for an input parameter type
            //=====================================================================
            if (direction == LbParameterDirection.INPUT)
            {
                _value = size;
            }
            else
            {
                _size = size;
            }
        }
        //=====================================================================
        /// <summary>
        /// Parameter Name
        /// </summary>
        //=====================================================================
        public string Name
        {
            get
            {
                return _name;
            }
        }

        //=====================================================================
        /// <summary>
        /// Datatype of the parmeter
        /// </summary>
        //=====================================================================
        public DbType DataType
        {
            get
            {
                return _dbType;
            }
        }

        //=====================================================================
        /// <summary>
        /// Direction of the parameter IN/OUT
        /// </summary>
        //=====================================================================
        public LbParameterDirection Direction
        {
            get
            {
                return _direction;
            }
        }

        //=====================================================================
        /// <summary>
        /// Value of the parameter
        /// </summary>
        //=====================================================================
        public object Value
        {
            get
            {
                return _value;
            }
        }

        //=====================================================================
        /// <summary>
        /// Size of the output parameter
        /// </summary>
        //=====================================================================
        public int Size
        {
            get
            {
                return _size;
            }
        }

    }
}
