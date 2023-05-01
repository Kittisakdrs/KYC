using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LoxleyOrbit.FaceScan.Models;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;

namespace LoxleyOrbit.FaceScan
{
    public static partial class APIHelper
    {
        public static int StatusCodeSame = 0;
        public static String _APIBaseUrl = "http://35.213.185.137/OneMLAPI";
        //public static String _APIBaseUrl = "http://localhost:6935";
        //public static String _APIBaseUrl = "https://test-kiosk.chulacareapp.com/OneMLAPI";
        public static String _FaceDetectorFromBase64 => _APIBaseUrl + "/api/FeceDetaction/FaceDetectorFromBase64";
        public static String _FaceDetectorFromPath => _APIBaseUrl + "/api/FeceDetaction/FaceDetectorFromPath";

        public static async System.Threading.Tasks.Task<FaceModel> FaceDetectorFromBase64(FaceDetectorFromBase64 req)
        {
            FaceModel face = new FaceModel();

            String responseBody = String.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.Timeout = new TimeSpan(0, 3, 0);

                    String content = String.Empty;

                    content = JsonConvert.SerializeObject(req);

                    HttpResponseMessage responseMessage = await client.PostAsync(APIHelper._FaceDetectorFromBase64, new StringContent(content, Encoding.UTF8, "application/json"));

                    responseBody = await responseMessage.Content.ReadAsStringAsync();

                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        face = JsonConvert.DeserializeObject<FaceModel>(responseBody);
                    }
                    else
                    {
                        face.Status = -100;
                    }
                }
            }
            catch (Exception ex)
            {
                face.Status = -100;
                face.Distance = ex.Message;
            }
            finally
            {
            }
            return face;
        }

        public static async System.Threading.Tasks.Task<FaceModel> FaceDetectorFromPath(RequestFaceDetectorFromPath req)
        {
            FaceModel face = new FaceModel();

            String responseBody = String.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.Timeout = new TimeSpan(0, 3, 0);

                    String content = String.Empty;

                    content = JsonConvert.SerializeObject(req);

                    HttpResponseMessage responseMessage = await client.PostAsync(APIHelper._FaceDetectorFromPath, new StringContent(content, Encoding.UTF8, "application/json"));
                    
                    responseBody = await responseMessage.Content.ReadAsStringAsync();

                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        face = JsonConvert.DeserializeObject<FaceModel>(responseBody);
                    }
                    else
                    {
                        face.Status = -100;
                    }
                }
            }
            catch (Exception ex)
            {
                face.Status = -100;
                face.Distance = ex.Message;
            }
            finally
            {
            }
            return face;
        }

        public static String _GetUserInformation => _APIBaseUrl + "/api/WebSiteCUH/GetUserInformation";
        public static String _GetRightApproved => _APIBaseUrl + "/api/WebSiteCUH/GetRightApproved";
        public static String _GetRight => _APIBaseUrl + "/api/WebSiteCUH/GetRight";
        public static async System.Threading.Tasks.Task<ResponseModel> Post(string url, RequestModel req)
        {
            ResponseModel response = new ResponseModel();

            String responseBody = String.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.Timeout = new TimeSpan(0, 0, 30);

                    String content = String.Empty;

                    content = JsonConvert.SerializeObject(req);

                    HttpResponseMessage responseMessage = await client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

                    responseBody = await responseMessage.Content.ReadAsStringAsync();

                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        response = JsonConvert.DeserializeObject<ResponseModel>(responseBody);
                    }
                    else
                    {
                        response.StatusCode = -100;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = -100;
                response.Message = ex.Message;
            }
            finally
            {
            }
            return response;
        }
    }
}
