using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using ProductMgt.Entity;
using ProductMgt.Config;
using System.Web;

namespace ProductMgt.Service
{
    public class AuthService : IAuthService
    {

        public bool Authenticate(string username, string password)
        {
            string url = AppConfig.Instance.CraServiceUrl;
            if (string.IsNullOrEmpty(url))
                return false;

            LoginData loginData = new LoginData { userName = username, password = password };
            string jsonData = new JavaScriptSerializer().Serialize(loginData);
            byte[] data = Encoding.Default.GetBytes(jsonData);

            url = url.TrimEnd(new char[] { '/' }) + "/api/auth/login";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    Guid token;
                                    string resContent = reader.ReadToEnd();
                                    LoginResult result = new JavaScriptSerializer().Deserialize<LoginResult>(resContent);
                                    return result != null && !string.IsNullOrWhiteSpace(result.token)
                                        && Guid.TryParse(result.token, out token) && token != Guid.Empty;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }            
            return false;
        }

        public void SetSession(HttpContext context)
        {
            context.Session[Constants.LOGIN_KEY] = true;
            context.Session.Timeout = Convert.ToInt32(AppConfig.Instance.SessionTimeout);
        }

        public bool ValidateToken(string token)
        {
            string url = AppConfig.Instance.CraServiceUrl;
            if (string.IsNullOrEmpty(url))
                return false;

            url = url.TrimEnd(new char[] { '/' }) + "/api/auth/validatetoken";
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("X-RM-API-TOKEN", token);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    string resContent = reader.ReadToEnd();
                                    ValidateTokenResult result = new JavaScriptSerializer().Deserialize<ValidateTokenResult>(resContent);
                                    return result != null && result.IsTokenValid;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }            
            return false;
        }
    }
}
