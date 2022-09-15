using DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline
{
    public partial class verification : System.Web.UI.Page
    {
        NameValueCollection keywordsToReplace = new NameValueCollection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request != null && Request.UrlReferrer != null)
                {
                    ViewState["PreviousPage"] = Request.UrlReferrer.ToString();
                }
                string Code = Request.QueryString["Username"].ToString();
                AdminDL objUserDL = new AdminDL();
                string verificationCode = CommonControl.Decrypt(Code);
                DataSet ds = objUserDL.GetVerificationCodeById(verificationCode);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(verificationCode))
                    {
                        if (objUserDL.AdminVerification(verificationCode))
                        {
                            pnlFailed.Visible = false;
                            string EmailIdTo = Convert.ToString(ds.Tables[0].Rows[0]["EmailAddress"]);
                            lblMessage.Text = "Your Email verified successfully.";
                            lblHMessage.Text = "Congratulation";
                            DataSet ds1 = objUserDL.GetAllClientMaster();
                            string host = "", fromMail = "", password = "";
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                host = Convert.ToString(ds1.Tables[0].Rows[0]["host"]);
                                fromMail = Convert.ToString(ds1.Tables[0].Rows[0]["fromEmail"]);
                                password = Convert.ToString(ds1.Tables[0].Rows[0]["sPassword"]);
                            }
                            if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(fromMail) && !string.IsNullOrEmpty(password))
                            {
                                string URL = ConfigurationManager.AppSettings["RegistartionUrl"].ToString();
                                string Url = URL;
                                string emailTo = EmailIdTo;
                                string subject = "Registartion process";
                                string body = "<br>You are almost there!. To complete the process, please click on the link below to registration process completed." + "<br>" + "<a href=" + Url + ">" + Url + "</a>"; ;
                                CommonControl.SendEmail(emailTo, subject, body, host, fromMail, password);
                            }
                        }
                        else
                        {
                            pnlSuccess.Visible = false;
                            lblMsg.Text = "Your Email verified links is Expired.";
                            lblHMsg.Text = "Error";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Your Email verified links is Expired.";
                        lblHMsg.Text = "Error";
                    }
                }
                else
                {
                    lblMsg.Text = "Your Email verified links is Expired.";
                    lblHMsg.Text = "Error";
                }

            }
        }
    }
}