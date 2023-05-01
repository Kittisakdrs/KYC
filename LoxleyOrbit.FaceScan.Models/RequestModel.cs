using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoxleyOrbit.FaceScan.Models
{
    public class RequestModel
    {
        public string hn { get; set; }
        public string id { get; set; }
        public string approveDate { get; set; }
        public string appointmentDate { get; set; }
        public int referExpireMonth { get; set; }
        public bool validation
        {
            get
            {
                if (!string.IsNullOrEmpty(hn))
                {
                    return true;
                }
                else if (!string.IsNullOrEmpty(id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
