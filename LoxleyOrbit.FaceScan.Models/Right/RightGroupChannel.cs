using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoxleyOrbit.FaceScan.Models.Right
{
    public class RightGroupChannel
    {
        public string right_channel_seq { get; set; }
        public string right_group_code { get; set; }
        public string right_code { get; set; }
        public string right_type_code { get; set; }
        public string flag_type_all { get; set; }
        public string flag_concat_type { get; set; }
        public string flag_contact_staff_only { get; set; }
        public string flag_not_check_expire { get; set; }
        public string flag_not_use_right { get; set; }
        public string group_channel_seq { get; set; }
        public string active_flag { get; set; }
        public DateTime crtd_dttm { get; set; }
        public string crtd_by { get; set; }
        public string upd_dttm { get; set; }
        public string upd_by { get; set; }
    }
}
