using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoxleyOrbit.FaceScan.Models;
using LoxleyOrbit.FaceScan.Models.Right;
using LoxleyOrbit.FaceScan.Web.Utility;
using Newtonsoft.Json;

namespace LoxleyOrbit.FaceScan.Web
{
    public partial class SelectUserType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //RegisterUser();
        }
        protected void btn_child_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Hn.aspx");
        }
        protected void btn_noncard_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Hn_eng.aspx");
        }

        protected void btn_foreign_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Hn_eng.aspx");
        }

        [WebMethod, ScriptMethod]
        public static UserInformation RegisterUser(string hn, string id)
        {
            UserInformation userInformation = new UserInformation();
            HttpContext.Current.Session["userInfo"] = null;
            userInformation.StatusCode = 0;

            try
            {
                return HelperUtility.RegisterUser(hn, id);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["userInfo"] = null;
                userInformation.StatusCode = -1;
                return userInformation;
            }
        }
    }
}