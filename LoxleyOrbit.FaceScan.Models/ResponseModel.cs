using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoxleyOrbit.FaceScan.Models;
using LoxleyOrbit.FaceScan.Models.Right;

namespace LoxleyOrbit.FaceScan.Models
{
    public class ResponseModel
    {
        private UserInformationModel userInformation = new UserInformationModel();
        private RightApproveModel rightApproveModel = new RightApproveModel();
        private RightModel right = new RightModel();
        private int statuscode = 0;
        private string message = "";
        private string stacktrace = "";
        public UserInformationModel UserInformation { get => userInformation; set => userInformation = value; }
        public RightApproveModel RightApproveModel { get => rightApproveModel; set => rightApproveModel = value; }
        public RightModel Right { get => right; set => right = value; }
        public int StatusCode { get => statuscode; set => statuscode = value; }
        public string Message { get => message; set => message = value; }
        public string StackTrace { get => stacktrace; set => stacktrace = value; }
    }
}
