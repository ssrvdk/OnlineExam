using ELHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace DataLayer
{
    public class StudentDL
    {
        public int RegisterStudent(StudentDetails student)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[3];
            parameter[0] = new LbSprocParameter("@Name", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, student.sName);
            parameter[1] = new LbSprocParameter("@EmailAddress", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, student.EmailAddress);
            parameter[2] = new LbSprocParameter("@Password", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, student.Password);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("USP_InsertStudentDetail", parameter));
            return Response;
        }

        public int VerifyStudent(int id)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[1];
            parameter[0] = new LbSprocParameter("@id", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, id);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("USP_VerifyStudent", parameter));
            return Response;
        }

        public DataSet LoginStudent(StudentDetails student)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[2];
            parameter[0] = new LbSprocParameter("@EmailAddress", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, student.EmailAddress);
            parameter[1] = new LbSprocParameter("@Password", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, student.Password);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            DataSet Response;
            Response = elhelper.ExecuteDataset("USP_LoginStudent", parameter);
            return Response;
        }

        public DataSet GetExamForStudent(int id)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[1];
            parameter[0] = new LbSprocParameter("@studentId", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, id);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            DataSet Response;
            Response = elhelper.ExecuteDataset("USP_GetExamForStudent", parameter);
            return Response;
        }

        public bool ValidateStudent(string Email)
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[1];
                parameter[0] = new LbSprocParameter("Email", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, Email);
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("USP_ValidateStudent", parameter);
                return (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && Convert.ToString(ds.Tables[0].Rows[0][0]) == "1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetAllClientMaster()
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[0];
            // parameter[0] = new LbSprocParameter("idCountry", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, idCountry);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            DataSet ds = elhelper.ExecuteDataset("sp_GetAllClientMaster", parameter);
            return ds;
        }

        public int ResetPassword(string Username, string Password)
        {
            int Response = 0;
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[2];
                parameter[0] = new LbSprocParameter("username", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, Username);
                parameter[1] = new LbSprocParameter("Password", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, Password);
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                Response = elhelper.ExecuteNonQuery("USP_ResetPasswordStudent", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Response;
        }

        public int StudentChangePassword(StudentDetails admin)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[3];
            parameter[0] = new LbSprocParameter("id", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, admin.Id);
            parameter[1] = new LbSprocParameter("Password", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.ConfirmPassword);
            parameter[2] = new LbSprocParameter("CurrentPassword", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.Password);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("USP_ChangePasswordStudent", parameter));
            return Response;
        }
        // 
        public DataSet GetAllAnswerDetails()
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[0];
            // parameter[0] = new LbSprocParameter("idCountry", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, idCountry);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            DataSet ds = elhelper.ExecuteDataset("USP_GetAllAnswerDetails", parameter);
            return ds;
        }

        public DataSet GetAllQuestionDetails()
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[0];
            // parameter[0] = new LbSprocParameter("idCountry", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, idCountry);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            DataSet ds = elhelper.ExecuteDataset("USP_GetAllQuestionsDetails", parameter);
            return ds;
        }

        public int InsertStudentAnswer(List<EntityLayer.StudentAnswer> answeredTable)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[1];
            parameter[0] = new LbSprocParameter("@xml", DbType.Xml, LbSprocParameter.LbParameterDirection.INPUT, answeredTable.ToXML());
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();

            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("USP_InsertStudentAnswer", parameter));
            return Response;
        }
    }
}
