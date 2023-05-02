using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoxleyOrbit.FaceScan.Models;
using LoxleyOrbit.FaceScan.Web.Utility;

namespace LoxleyOrbit.FaceScan.Web
{
 
    public partial class User_Hn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btn_login_Click(object sender, ImageClickEventArgs e)
        {
            //string script = "RegisterUser(" + txt_hn.Text.Trim() + ");";
            //string script = "RegisterUser();";
            //JavascriptManager.RegisterStartupScript("background_script_Redirect", script);
            try
            {
                var res = HelperUtility.RegisterUser(HelperUtility.ConvertHN(txt_hn.Text.Trim()), "");
                if (res.StatusCode == 0)
                {
                    string script = "Redirect();";
                    JavascriptManager.RegisterStartupScript("background_script_Redirect", script);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Session["userInfo"] = null;
            }
        }

        protected void btn_cancel_Click(object sender, ImageClickEventArgs e)
        {
            txt_hn.Text = "";
        }

        [WebMethod, ScriptMethod]
        public static UserInformation RegisterUser(string hn, string id)
        {
            UserInformation userInformation = new UserInformation();
            HttpContext.Current.Session["userInfo"] = null;
            userInformation.StatusCode = 0;

            try
            {
                return HelperUtility.RegisterUser(HelperUtility.ConvertHN(hn), id);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["userInfo"] = null;
                userInformation.StatusCode = -1;
                return userInformation;
            }
        }

        protected void btn_back_Click(object sender, ImageClickEventArgs e)
        {
            Session["userInfo"] = null;
            Response.Redirect("SelectUserType.aspx");
        }
    }
}