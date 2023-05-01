using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoxleyOrbit.FaceScan.Models.Right
{
    public class RightApproveModel
    {
        private List<RightApproved> rightapproved = new List<RightApproved>();
        public RightGroup RightGroup { get; set; }
        public RightGroup SubRightGroup { get; set; }
        public string RightGroupDescription { get; set; }
        public string RightStatus { get; set; }
        public List<RightApproved> RightApproved { get => rightapproved; set => rightapproved = value; }
    }
    public class RightApproved
    {
        public string HasRightApprove { get; set; }
        public string SpecificClinicFlag { get; set; }
        public string SpecificClinicCode { get; set; }
        public string SpecificClinicName { get; set; }
        public string SpecificNClinicFlag { get; set; }
        public string SpecificNClinicCode { get; set; }
        public string SpecificNClinicName { get; set; }
        public string Disease { get; set; }
        public string Remark { get; set; }
        public string RightApproveDttm { get; set; }
        public string RightGroupCode { get; set; }
        public string RightGroupName { get; set; }
        public string RightCode { get; set; }
        public string RightName { get; set; }
        public string RightTypeCode { get; set; }
        public string RightTypeName { get; set; }
        public string ContactType { get; set; }
        public string RightContCode { get; set; }
        public string RightContName { get; set; }
        public string FlagConcatType { get; set; }
        public string ChannelGroupId { get; set; }
        public string spclctflag { get; set; }
        public int sort { get; set; }
    }
}
