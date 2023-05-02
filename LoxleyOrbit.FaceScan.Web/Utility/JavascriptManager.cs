using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace LoxleyOrbit.FaceScan.Web.Utility
{
    public class JavascriptManager
    {
        public static void RegisterStartupScript(string name, string script)
        {
            ScriptManager.RegisterStartupScript
                (
                   (Page)HttpContext.Current.Handler
                   , HttpContext.Current.Handler.GetType()
                   , name
                   , script
                   , true
                );
        }

        public static string LaunchCamera()
        {
            try
            {
                return ""
                //    "function LaunchCamera()"
                //+ "{"
                //+ "    try"
                //+ "    {"
                //+ "        return window.external.LaunchCamera();"
                //+ "    }"
                //+ "    catch (Error)"
                //+ "    {"
                //+ "        return 0;"
                //+ "    }"
                //+ "}"

                + "LaunchCamera();";
            }
            catch (Exception ex)
            {
                return "alert('LaunchCamera error:" + ex.Message + "')";
            }
        }
    }
}