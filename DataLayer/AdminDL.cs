using ELHelper;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdminDL
    {
        public DataSet AdminLogin(string Username, string Password)
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[2];
                parameter[0] = new LbSprocParameter("UserName", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, Username);
                parameter[1] = new LbSprocParameter("Password", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, Password);
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_AdminLogin", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public bool ValidateAdmin(string Email)
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[1];
                parameter[0] = new LbSprocParameter("Email", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, Email);
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_ValidateAdmin", parameter);
                return (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && Convert.ToString(ds.Tables[0].Rows[0][0]) == "1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                Response = elhelper.ExecuteNonQuery("sp_ResetPassword", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Response;
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

        public int RegisterAdmin(AdminCls admin)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[5];
            parameter[0] = new LbSprocParameter("@Name", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.sName);
            parameter[1] = new LbSprocParameter("@EmailAddress", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.EmailAddress);
            parameter[2] = new LbSprocParameter("@Password", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.Password);
            parameter[3] = new LbSprocParameter("@VerificationCode", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.VerificationCode);
            parameter[4] = new LbSprocParameter("@UserName", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.Username);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();

            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("sp_RegisterAdmin", parameter));
            return Response;
        }

        public DataSet GetVerificationCodeById(string verificationCode)
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;

                parameter = new LbSprocParameter[1];
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                parameter[0] = new LbSprocParameter("@verificationCode", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, verificationCode);
                ds = elhelper.ExecuteDataset("sp_GetVerificationCodeById", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public bool AdminVerification(string verificationCode)
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[1];
                parameter[0] = new LbSprocParameter("verificationCode", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, verificationCode);
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_AdminVerification", parameter);
                return (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && Convert.ToString(ds.Tables[0].Rows[0][0]) == "1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AdminChangePassword(AdminCls admin)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[3];
            parameter[0] = new LbSprocParameter("id", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, admin.idAdmin);
            parameter[1] = new LbSprocParameter("Password", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.ConfirmPassword);
            parameter[2] = new LbSprocParameter("CurrentPassword", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, admin.Password);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("sp_AdminChangePassword", parameter));
            return Response;
        }

        public DataSet GetAllSectionMaster()
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[0];
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_AdminGetAllSectionMaster", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public int SetSectionMaster(SectionMaster category)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[3];
            parameter[0] = new LbSprocParameter("Id", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, category.IdSectionMaster);
            parameter[1] = new LbSprocParameter("SectionName", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, category.SectionName);
            parameter[2] = new LbSprocParameter("bActive", DbType.Boolean, LbSprocParameter.LbParameterDirection.INPUT, category.bActive);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();

            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("sp_AdminSetSectionMaster", parameter));
            return Response;
        }

        public int DeleteSectionMaster(int idSectionMaster)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[1];
            parameter[0] = new LbSprocParameter("IdCategory", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, idSectionMaster);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();

            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("sp_AdminDeleteSectionMaster", parameter));
            return Response;
        }

        public DataSet GetAllQuestionPaperMaster()
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[0];
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_AdminGetAllQuestionPaperMaster", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public int SetQuestionPaperMaster(QuestionPaperMaster category)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[5];
            parameter[0] = new LbSprocParameter("IdQuestionPaperMaster", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, category.QuestionPaperMasterId);
            parameter[1] = new LbSprocParameter("sName", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, category.sName);
            parameter[2] = new LbSprocParameter("bActive", DbType.Boolean, LbSprocParameter.LbParameterDirection.INPUT, category.bActive);
            parameter[3] = new LbSprocParameter("NumberofQuestions", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, category.NumberofQuestions);
            parameter[4] = new LbSprocParameter("Time", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, category.Time);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();

            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("sp_AdminSetQuestionPaperMaster", parameter));
            return Response;
        }

        public int DeleteQuestionPaperMaster(int idQuestionPaperMaster)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[1];
            parameter[0] = new LbSprocParameter("IdQuestionPaperMaster", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, idQuestionPaperMaster);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();

            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("sp_AdminDeleteQuestionPaperMaster", parameter));
            return Response;
        }

        public DataSet GetAllQuestionMaster()
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[0];
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_AdminGetAllQuestionMaster", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public MessageResponse SetQuestionMaster(QuestionMaster question)
        {
            LbSprocParameter[] parameter = new LbSprocParameter[11];
            parameter[0] = new LbSprocParameter("IdQuestionMaster", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, question.QuestionMasterId);
            parameter[1] = new LbSprocParameter("Question", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, question.Question);
            parameter[2] = new LbSprocParameter("Option1", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, question.Option1);
            parameter[3] = new LbSprocParameter("Option2", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, question.Option2);
            parameter[4] = new LbSprocParameter("Option3", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, question.Option3);
            parameter[5] = new LbSprocParameter("Option4", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, question.Option4);
            parameter[6] = new LbSprocParameter("SectionId", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, question.SectionId);
            parameter[7] = new LbSprocParameter("bActive", DbType.Boolean, LbSprocParameter.LbParameterDirection.INPUT, question.bActive);
            parameter[8] = new LbSprocParameter("Answer", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, question.Answer);
            parameter[9] = new LbSprocParameter("nError", DbType.Int32, LbSprocParameter.LbParameterDirection.OUTPUT, 255);
            parameter[10] = new LbSprocParameter("sMsg", DbType.String, LbSprocParameter.LbParameterDirection.OUTPUT, 500);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            MessageResponse objResponse = new MessageResponse();
            elhelper.ExecuteNonQuery("sp_SetQuestionMaster", parameter);
            objResponse.nError = Convert.IsDBNull(elhelper.GetParameterValue("@nError")) ? 0 : Convert.ToInt32(elhelper.GetParameterValue("@nError"));
            objResponse.sMsg = Convert.IsDBNull(elhelper.GetParameterValue("@sMsg")) ? "" : Convert.ToString(elhelper.GetParameterValue("@sMsg"));
            return objResponse;
        }

        public MessageResponse DeleteQuestionMaster(int idQuestionMaster)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[3];
            parameter[0] = new LbSprocParameter("IdQuestion", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, idQuestionMaster);
            parameter[1] = new LbSprocParameter("nError", DbType.Int32, LbSprocParameter.LbParameterDirection.OUTPUT, 255);
            parameter[2] = new LbSprocParameter("sMsg", DbType.String, LbSprocParameter.LbParameterDirection.OUTPUT, 500);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            MessageResponse objResponse = new MessageResponse();
            elhelper.ExecuteNonQuery("sp_AdminDeleteQuestionMaster", parameter);
            objResponse.nError = Convert.IsDBNull(elhelper.GetParameterValue("@nError")) ? 0 : Convert.ToInt32(elhelper.GetParameterValue("@nError"));
            objResponse.sMsg = Convert.IsDBNull(elhelper.GetParameterValue("@sMsg")) ? "" : Convert.ToString(elhelper.GetParameterValue("@sMsg"));
            return objResponse;
        }

        public DataSet GetAllStudentDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[0];
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_AdminGetAllStudentDetails", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public MessageResponse SetStudentDetails(StudentDetails category)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[10];
            parameter[0] = new LbSprocParameter("Id", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, category.Id);
            parameter[1] = new LbSprocParameter("Name", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, category.sName);
            parameter[2] = new LbSprocParameter("bActive", DbType.Boolean, LbSprocParameter.LbParameterDirection.INPUT, category.bActive);
            parameter[3] = new LbSprocParameter("Address", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, category.Address);
            parameter[4] = new LbSprocParameter("City", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, category.City);
            parameter[5] = new LbSprocParameter("EmailAddress", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, category.EmailAddress);
            parameter[6] = new LbSprocParameter("State", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, category.State);
            parameter[7] = new LbSprocParameter("MobileNumber", DbType.String, LbSprocParameter.LbParameterDirection.INPUT, category.MobileNumber);
            parameter[8] = new LbSprocParameter("nError", DbType.Int32, LbSprocParameter.LbParameterDirection.OUTPUT, 255);
            parameter[9] = new LbSprocParameter("sMsg", DbType.String, LbSprocParameter.LbParameterDirection.OUTPUT, 500);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            MessageResponse objResponse = new MessageResponse();
            elhelper.ExecuteNonQuery("sp_AdminSetStudentDetails", parameter);
            objResponse.nError = Convert.IsDBNull(elhelper.GetParameterValue("@nError")) ? 0 : Convert.ToInt32(elhelper.GetParameterValue("@nError"));
            objResponse.sMsg = Convert.IsDBNull(elhelper.GetParameterValue("@sMsg")) ? "" : Convert.ToString(elhelper.GetParameterValue("@sMsg"));
          //  int Response = 0;
          //  Response = Convert.ToInt32(elhelper.ExecuteNonQuery("sp_AdminSetStudentDetails", parameter));
            return objResponse;
        }

        public int DeleteStudentDetails(int id)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[1];
            parameter[0] = new LbSprocParameter("Id", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, id);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();

            int Response = 0;
            Response = Convert.ToInt32(elhelper.ExecuteScalar("sp_AdminDeleteStudentDetails", parameter));
            return Response;
        }

        public MessageResponse DeleteQuePprSectionMapping(int IdQuePprSectionMapping)
        {
            LbSprocParameter[] parameter;
            parameter = new LbSprocParameter[3];
            parameter[0] = new LbSprocParameter("IdQuePprSectionMapping", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, IdQuePprSectionMapping);
            parameter[1] = new LbSprocParameter("nError", DbType.Int32, LbSprocParameter.LbParameterDirection.OUTPUT, 255);
            parameter[2] = new LbSprocParameter("sMsg", DbType.String, LbSprocParameter.LbParameterDirection.OUTPUT, 500);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            MessageResponse objResponse = new MessageResponse();
            elhelper.ExecuteNonQuery("sp_AdminDeleteQuePprSectionMapping", parameter);
            objResponse.nError = Convert.IsDBNull(elhelper.GetParameterValue("@nError")) ? 0 : Convert.ToInt32(elhelper.GetParameterValue("@nError"));
            objResponse.sMsg = Convert.IsDBNull(elhelper.GetParameterValue("@sMsg")) ? "" : Convert.ToString(elhelper.GetParameterValue("@sMsg"));
            return objResponse;
        }

        public MessageResponse SetQuePprSectionMapping(QuePprSectionMapping question)
        {
            LbSprocParameter[] parameter = new LbSprocParameter[7];
            parameter[0] = new LbSprocParameter("QuePprSectionMappingId", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, question.QuePprSectionMappingId);
            parameter[1] = new LbSprocParameter("Sectionid", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, question.Sectionid);
            parameter[2] = new LbSprocParameter("QuestionPaperid", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, question.QuestionPaperid);
            parameter[3] = new LbSprocParameter("NumberOfQuestion", DbType.Int32, LbSprocParameter.LbParameterDirection.INPUT, question.NumberOfQuestion);
            parameter[4] = new LbSprocParameter("bActive", DbType.Boolean, LbSprocParameter.LbParameterDirection.INPUT, question.bActive);
            parameter[5] = new LbSprocParameter("nError", DbType.Int32, LbSprocParameter.LbParameterDirection.OUTPUT, 255);
            parameter[6] = new LbSprocParameter("sMsg", DbType.String, LbSprocParameter.LbParameterDirection.OUTPUT, 500);
            ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
            MessageResponse objResponse = new MessageResponse();
            elhelper.ExecuteNonQuery("sp_AdminSetQuePprSectionMapping", parameter);
            objResponse.nError = Convert.IsDBNull(elhelper.GetParameterValue("@nError")) ? 0 : Convert.ToInt32(elhelper.GetParameterValue("@nError"));
            objResponse.sMsg = Convert.IsDBNull(elhelper.GetParameterValue("@sMsg")) ? "" : Convert.ToString(elhelper.GetParameterValue("@sMsg"));
            return objResponse;
        }

        public DataSet GetAllQuePprSectionMapping()
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[0];
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_AdminGetAllQuePprSectionMapping", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetQuestions()
        {
            DataSet ds = new DataSet();
            try
            {
                LbSprocParameter[] parameter;
                parameter = new LbSprocParameter[0];
                ELHelper.ELHelper elhelper = new ELHelper.ELHelper();
                ds = elhelper.ExecuteDataset("sp_GetQuestions", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

    }
}
