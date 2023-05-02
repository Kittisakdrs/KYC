using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoxleyOrbit.FaceScan.Models;
using LoxleyOrbit.FaceScan.Models.Right;
using LoxleyOrbit.FaceScan.Web.Utility;

namespace Face_scan
{
    public partial class User_detail_Eng : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var res = (UserInformation)Session["userInfo"];
            if (res != null)
            {
                lb_hn.Text = res.hn;
                //lb_id_card.Text = res.idCard;
                lb_name.Text = res.pateintName;

                string right = "";
                foreach (PatientRightModel n in res.PatientRightModels)
                {
                    right = right + n.RightName + " " + n.RightContName + "<br>";
                }
                //lb_rights.Text = right;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //string script2 = "LaunchCamera('" + lb_hn.Text + "');";
            var res = (UserInformation)Session["userInfo"];
            string script2 = "LaunchCamera('" + lb_hn.Text + "', '" + res.base64 + "');";
            JavascriptManager.RegisterStartupScript("queue_background_script_LaunchCamera", script2);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Session["userInfo"] = null;
            Response.Redirect("SelectUserType.aspx");
        }
    }
}