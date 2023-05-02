using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LoxleyOrbit.FaceScan.Models;
using LoxleyOrbit.FaceScan.Models.Right;

namespace LoxleyOrbit.FaceScan.Web.Utility
{
    public static class HelperUtility
    {
        public static string ConvertHN(string pHN)
        {
            if (string.IsNullOrEmpty(pHN) || pHN.Length < 2)
                return "";

            string[] components = pHN.Split('/');

            if (components.Length == 2)
            {
                // 1. components[0] - รหัส HN
                // 2. components[1] - ปี
                return components[1] + components[0].PadLeft(6, '0');
            }
            else
            {
                // XXYYYYYY ; XX = ปี พ.ศ. 2 หลักสุดท้าย, YYYYYY รหัส HN หกหลักที่มีการเติม 0 ให้ข้างหน้า
                if (pHN.Length >= 2)
                {
                    string year = pHN.Substring(pHN.Length - 2, 2);
                    string hn = pHN.Substring(0, pHN.Length - 2);

                    return year + hn.PadLeft(6, '0');
                }
                else
                    return pHN;
            }
        }
        public static String FormatToFullHN(String formatHN)
        {
            Int32 running = Int32.Parse(formatHN.Substring(0, formatHN.IndexOf("/")));
            Int32 year = Int32.Parse(formatHN.Substring(formatHN.IndexOf("/") + 1, 2));
            return (year + running.ToString().PadLeft(6, '0'));
        }
        public static string CovertHNNoToFormat(string hn)
        {
            string result = "";
            var year = hn.Substring(0, 2);
            var no = hn.Substring(2, 6);
            if (no.Substring(0, 1).Equals("0"))
            {
                no = no.Substring(1, 5);
                if (no.Substring(0, 1).Equals("0"))
                {
                    no = no.Substring(1, 4);
                    if (no.Substring(0, 1).Equals("0"))
                    {
                        no = no.Substring(1, 3);
                        if (no.Substring(0, 1).Equals("0"))
                        {
                            no = no.Substring(1, 2);
                            if (no.Substring(0, 1).Equals("0"))
                            {
                                no = no.Substring(1, 1);
                            }
                        }
                    }
                }
            }

            result = no + "/" + year;
            return result;
        }
        public static String RemoveCharFromHN(String rawHN)
        {
            if (String.IsNullOrEmpty(rawHN)) return rawHN;
            try
            {
                return new string((from c in rawHN
                                   where char.IsDigit(c)
                                   select c).ToArray());
            }
            catch (Exception ex)
            {
                return rawHN;
            }
        }
        public static UserInformation RegisterUser(string hn, string id)
        {
            UserInformation userInformation = new UserInformation();
            HttpContext.Current.Session["userInfo"] = null;
            userInformation.StatusCode = -1;
            try
            {
                var userInfo = Task.Run(() => APIHelper.Post(APIHelper._GetUserInformation, new RequestModel() {hn = hn,  id = id })).Result;

                if (userInfo.StatusCode == 0)
                {
                    userInformation.StatusCode = 0;
                    userInformation.idCard = userInfo.UserInformation.idCard;
                    userInformation.hn = userInfo.UserInformation.hn;
                    userInformation.formattedHN = userInfo.UserInformation.formattedHN;
                    userInformation.noType = userInfo.UserInformation.noType;
                    userInformation.isPateint = userInfo.UserInformation.isPateint;
                    userInformation.errorCode = userInfo.UserInformation.errorCode;
                    userInformation.errorDesc = userInfo.UserInformation.errorDesc;
                    userInformation.pateintName = userInfo.UserInformation.pateintName;
                    userInformation.pateintDob = userInfo.UserInformation.pateintDob;
                    userInformation.hnexpireFlag = userInfo.UserInformation.hnexpireFlag;
                    userInformation.pname = userInfo.UserInformation.pname;
                    userInformation.fname = userInfo.UserInformation.fname;
                    userInformation.lname = userInfo.UserInformation.lname;
                    userInformation.sex = userInfo.UserInformation.sex;
                    userInformation.base64 = userInfo.UserInformation.base64;

                    var rightApproved = Task.Run(() => APIHelper.Post(APIHelper._GetRightApproved, new RequestModel()
                    {
                        hn = userInfo.UserInformation.hn,
                        approveDate = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd", new CultureInfo("en-US"))
                    })).Result;

                    if (rightApproved.StatusCode == 0 && rightApproved.RightApproveModel.RightStatus == "Y")
                    {
                        userInformation.PatientRightModels = new List<PatientRightModel>();

                        foreach (RightApproved right in rightApproved.RightApproveModel.RightApproved)
                        {
                            PatientRightModel patientRightModel = new PatientRightModel();

                            patientRightModel.HasRight = right.HasRightApprove;
                            patientRightModel.ErrorCode = "";
                            patientRightModel.ErrorDesc = "";
                            patientRightModel.RightGroupCode = right.RightGroupCode;
                            patientRightModel.RightGroupName = right.RightGroupName;
                            patientRightModel.RightCode = right.RightCode;
                            patientRightModel.RightName = right.RightName;
                            patientRightModel.RightTypeCode = right.RightTypeCode;
                            patientRightModel.ReferTypeName = right.RightTypeName;
                            patientRightModel.ContactType = right.ContactType;
                            patientRightModel.RightContCode = right.RightContCode;
                            patientRightModel.RightContName = right.RightContName;
                            patientRightModel.RightStartDate = "";
                            patientRightModel.RightRefNo = "";
                            patientRightModel.RightEDate = "";
                            patientRightModel.RightLastDate = "";
                            patientRightModel.ChannelGroupId = right.ChannelGroupId;

                            userInformation.PatientRightModels.Add(patientRightModel);
                        }
                    }
                    else
                    {
                        var right = Task.Run(() => APIHelper.Post(APIHelper._GetRight,
                            new RequestModel()
                            {
                                hn = userInfo.UserInformation.hn,
                                id = "",
                                referExpireMonth = 6,
                                appointmentDate = DateTime.Now.AddDays(0).ToString("dd/MM/yyyy", new CultureInfo("en-US"))
                            })).Result;

                        if (right.StatusCode == 0)
                        {
                            if (right.Right.RightGroup == RightGroup.SI2_302)
                            {
                                foreach (ReferRight r in right.Right.ReferRightActives)
                                {
                                    PatientRightModel patientRightModel = new PatientRightModel();

                                    patientRightModel.HasRight = r.HasRefer;
                                    patientRightModel.ErrorCode = r.ErrorCode;
                                    patientRightModel.ErrorDesc = r.ErrorDesc;
                                    patientRightModel.RightGroupCode = r.ReferGroupCode;
                                    patientRightModel.RightGroupName = r.ReferGroupName;
                                    patientRightModel.RightCode = r.ReferCode;
                                    patientRightModel.RightName = r.ReferName;
                                    patientRightModel.RightTypeCode = r.ReferTypeCode;
                                    patientRightModel.ReferTypeName = r.ReferTypeName;
                                    patientRightModel.ContactType = r.FlagConcatType;
                                    patientRightModel.RightContCode = r.ReferHosFromCode;
                                    patientRightModel.RightContName = r.ReferHosFromName;
                                    patientRightModel.RightStartDate = r.ReferStartDate;
                                    patientRightModel.RightRefNo = "";
                                    patientRightModel.RightEDate = r.ReferEDate;
                                    patientRightModel.RightLastDate = r.ReferLastDate;
                                    patientRightModel.ChannelGroupId = r.ChannelGroupId;

                                    userInformation.PatientRightModels.Add(patientRightModel);
                                }
                                if (userInformation.PatientRightModels.Count == 0)
                                {
                                    PatientRightModel patientRightModel = new PatientRightModel();

                                    patientRightModel.HasRight = right.Right.LatestReferRightExpire.HasRefer;
                                    patientRightModel.ErrorCode = right.Right.LatestReferRightExpire.ErrorCode;
                                    patientRightModel.ErrorDesc = right.Right.LatestReferRightExpire.ErrorDesc;
                                    patientRightModel.RightGroupCode = right.Right.LatestReferRightExpire.ReferGroupCode;
                                    patientRightModel.RightGroupName = right.Right.LatestReferRightExpire.ReferGroupName;
                                    patientRightModel.RightCode = right.Right.LatestReferRightExpire.ReferCode;
                                    patientRightModel.RightName = right.Right.LatestReferRightExpire.ReferName;
                                    patientRightModel.RightTypeCode = right.Right.LatestReferRightExpire.ReferTypeCode;
                                    patientRightModel.ReferTypeName = right.Right.LatestReferRightExpire.ReferTypeName;
                                    patientRightModel.ContactType = right.Right.LatestReferRightExpire.FlagConcatType;
                                    patientRightModel.RightContCode = right.Right.LatestReferRightExpire.ReferHosFromCode;
                                    patientRightModel.RightContName = right.Right.LatestReferRightExpire.ReferHosFromName;
                                    patientRightModel.RightStartDate = right.Right.LatestReferRightExpire.ReferStartDate;
                                    patientRightModel.RightRefNo = "";
                                    patientRightModel.RightEDate = right.Right.LatestReferRightExpire.ReferEDate;
                                    patientRightModel.RightLastDate = right.Right.LatestReferRightExpire.ReferLastDate;
                                    patientRightModel.ChannelGroupId = right.Right.LatestReferRightExpire.ChannelGroupId;

                                    userInformation.PatientRightModels.Add(patientRightModel);
                                }

                            }
                            else if (right.Right.RightGroup == RightGroup.SI1_301)
                                foreach (PatientRightModel r in right.Right.PatientRightActives)
                                {
                                    PatientRightModel patientRightModel = new PatientRightModel();

                                    patientRightModel.HasRight = r.HasRight;
                                    patientRightModel.ErrorCode = r.ErrorCode;
                                    patientRightModel.ErrorDesc = r.ErrorDesc;
                                    patientRightModel.RightGroupCode = r.RightGroupCode;
                                    patientRightModel.RightGroupName = r.RightGroupName;
                                    patientRightModel.RightCode = r.RightCode;
                                    patientRightModel.RightName = r.RightName;
                                    patientRightModel.RightTypeCode = r.RightTypeCode;
                                    patientRightModel.ReferTypeName = r.ReferTypeName;
                                    patientRightModel.ContactType = r.ContactType;
                                    patientRightModel.RightContCode = r.RightContCode;
                                    patientRightModel.RightContName = r.RightContName;
                                    patientRightModel.RightStartDate = r.RightStartDate;
                                    patientRightModel.RightRefNo = r.RightRefNo;
                                    patientRightModel.RightEDate = r.RightEDate;
                                    patientRightModel.RightLastDate = r.RightLastDate;
                                    patientRightModel.ChannelGroupId = r.ChannelGroupId;

                                    userInformation.PatientRightModels.Add(patientRightModel);
                                }
                            else
                            {
                                //ไม่พบสิทธิ ผู้ป่วยทั่วไป
                                PatientRightModel patientRightModel = new PatientRightModel();

                                patientRightModel.RightName = "ผู้ป่วยทั่วไป";
                                patientRightModel.RightContName = "";

                                userInformation.PatientRightModels.Add(patientRightModel);
                            }
                        }
                        else
                        {
                            //ไม่พบสิทธิ ผู้ป่วยทั่วไป
                            PatientRightModel patientRightModel = new PatientRightModel();

                            patientRightModel.RightName = "ผู้ป่วยทั่วไป";
                            patientRightModel.RightContName = "";

                            userInformation.PatientRightModels.Add(patientRightModel);
                        }
                    }

                    HttpContext.Current.Session["userInfo"] = userInformation;
                }
                else
                {
                    //ไม่พบข้อมูล
                    HttpContext.Current.Session["userInfo"] = null;
                }

                //HttpContext.Current.Response.Redirect("User_detail.aspx");
                return userInformation;
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