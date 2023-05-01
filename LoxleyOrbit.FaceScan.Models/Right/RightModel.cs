using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoxleyOrbit.FaceScan.Models.Right
{
    public class RightModel
    {
        public RightGroup RightGroup { get; set; }
        public RightGroup SubRightGroup { get; set; }
        public string RightGroupDescription { get; set; }
        public IEnumerable<ReferRight> ReferRightActives { get; set; }
        public IEnumerable<PatientRightModel> PatientRightActives { get; set; }
        public PatientRightModel LatestPatientRightExpire { get; set; }
        public ReferRight LatestReferRightActive { get; set; }
        public ReferRight LatestReferRightExpire { get; set; }
        public RightModel()
        {
            ReferRightActives = new List<ReferRight>();
            PatientRightActives = new List<PatientRightModel>();
        }
    }
}
