using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoxleyOrbit.FaceScan.Models.Right;

namespace LoxleyOrbit.FaceScan.Models
{
    public class UserInformation : UserInformationModel
    {
        private List<PatientRightModel> patientRightModels = new List<PatientRightModel>();
        private int statuscode = 0;
        private string message = "";
        private string stacktrace = "";
        public List<PatientRightModel> PatientRightModels { get => patientRightModels; set => patientRightModels = value; }
        public int StatusCode { get => statuscode; set => statuscode = value; }
        public string Message { get => message; set => message = value; }
        public string StackTrace { get => stacktrace; set => stacktrace = value; }
    }

    public class UserInformationModel
    {
        public string idCard { get; set; }
        public string hn { get; set; }
        public string formattedHN { get; set; }
        public string noType { get; set; }
        public string isPateint { get; set; }
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
        public string pateintName { get; set; }
        public string pateintDob { get; set; }
        public string hnexpireFlag { get; set; }
        public string pname { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string sex { get; set; }
        public string base64 { get; set; }

        public int Age
        {
            get
            {
                int userold = 0;
                if (!string.IsNullOrEmpty(pateintDob))
                {
                    try
                    {
                        DateTime zeroTime = new DateTime(1, 1, 1);

                        var arrydate = pateintDob.Replace("/", "-").Split(' ')[0].Split('-').Select(Int32.Parse).ToList();
                        DateTime bDate = DateTime.Now;
                        if (arrydate[2] > 1000)
                        {
                            bDate = new DateTime(arrydate[2], arrydate[1], arrydate[0]);
                        }
                        else
                        {
                            bDate = new DateTime(arrydate[0], arrydate[1], arrydate[2]);
                        }

                        if (bDate >= DateTime.Now)
                            bDate = bDate.AddYears(-543);

                        TimeSpan span = DateTime.Now - bDate;

                        userold = (zeroTime + span).Year - 1;
                    }
                    catch
                    {
                        userold = 20;
                    }
                }
                return userold;
            }
        }
    }
}
