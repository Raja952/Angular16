using FurnitureBLL;
using FurnitureDO;
using GoogleAuthentication.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;
using Razorpay.Api;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Angular16.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        string NewsessionId = string.Empty;


        [HttpGet]
        public ActionResult Login()
        {
            string NewUrl = string.Empty;
            TempData["Image"] = null;
            ModelState.Clear();
            if (TempData["Login"] == null)
            {
                TempData["Login"] = "";
            }
            string HostUrl = string.Empty;
            string _sessionId = string.Empty;
            string Url = Request.ServerVariables["SERVER_NAME"];
            try
            {
                ViewBag.ClientId = "Ov23liAsZOtxrg8tdkjE"; // Your GitHub Client ID
                ViewBag.RedirectUrl = "http://localhost/Furniture/Admin/Login";

                var clientid = "875393347473-an27d3qq5r9ejubnlkskr24b786qv9nd.apps.googleusercontent.com";
                var url = "http://localhost/Furniture/Admin/GoogleLoginCallback";

                var response = GoogleAuth.GetAuthUrl(clientid, url);
                ViewBag.response = response;


                HostUrl = Request.Url.Host;
            }
            catch
            { }
            NewUrl = new LoginBLL().GetNewUrlPage(HostUrl);
            string[] NewUrl1 = NewUrl.Split('@');
            if (TempData["Image"] == null)
            {
                TempData["Image"] = NewUrl1[1];
            }
            return View(NewUrl1[0]);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(string UserName, string EmailId)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(EmailId))
                {
                    return Json(new { success = false, message = "Username and Email are required." });
                }

                // Get user ID
                int UserId = new LoginBLL().GetUserId(UserName, EmailId);

                // Store user ID in session
                Session["UserId"] = UserId;

                if (UserId > 0)
                {
                    // Call password reset method
                    bool IsMailSent = new LoginBLL().ForgetPassword(UserName, EmailId);
                    if (IsMailSent)
                    {
                        return Json(new { IsMailSent, message = "Password reset link sent to your email." });
                    }
                    else
                    {
                        return Json(new { IsMailSent, message = "Password reset link does not sent to your email." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "User not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                System.Diagnostics.Debug.WriteLine("ForgetPassword Error: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("Stack Trace: " + ex.StackTrace);

                // Return a user-friendly message
                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection formData)
        {
            try
            {
                string userEmail = formData["UserEmail"];
                string password = formData["Password"];

                AISessionDO aisession = new LoginBLL().Authenticate(userEmail, password);

                if (SessionContext.Current.AISession.SessionConnected == true)
                {
                    string sessionId = Session["LogSessionId"]?.ToString();
                    bool isOTP = false; // Replace with actual logic to determine if OTP is required

                    return Json(new
                    {
                        Success = true,
                        IsOTP = isOTP,
                        SessionId = sessionId,
                        Message = "Login successful"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "User ID or Password is incorrect."
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = "An error occurred: " + ex.Message
                });
            }
        }


        //[HttpPost]
        //public ActionResult Login(FormCollection formData)
        //{
        //    string res = string.Empty;
        //    LoginDetailDO objLogindetails = new LoginDetailDO();

        //    objLogindetails.UserName = Convert.ToString(formData["UserEmail"]);
        //    objLogindetails.Password = Convert.ToString(formData["Password"]);

        //    // Store in session
        //    System.Web.HttpContext.Current.Session["UserName"] = objLogindetails.UserName;
        //    System.Web.HttpContext.Current.Session["Password"] = objLogindetails.Password;

        //    AISessionDO aisession = new LoginBLL().Authenticate(objLogindetails.UserName, objLogindetails.Password);

        //    try
        //    {
        //        if (SessionContext.Current.AISession.SessionConnected == true)
        //        {
        //            FormsAuthentication.RedirectFromLoginPage(objLogindetails.UserName, true);
        //            string sessionId = Session["LogSessionId"].ToString();

        //            // Store IsOTP in TempData
        //            TempData["IsOTPActive"] = objLogindetails.IsOTP ? "True" : "False";

        //            if (objLogindetails.IsOTP)
        //            {
        //                // Return view with OTP modal trigger
        //                TempData["ShowOTPModal"] = true;
        //                return View("Login", objLogindetails);
        //            }

        //            return RedirectToAction("Home", "Home", new { Identity = sessionId });
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = "User ID or Password is incorrect.";
        //            return View("Login", objLogindetails);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = "Login failed. Please try again.";
        //        return View("Login", objLogindetails);
        //    }
        //}

        public async Task<ActionResult> GoogleLoginCallback(string code)
        {
            LoginDetailDO objGoogleLogindetails = new LoginDetailDO();
            var clientid = "875393347473-an27d3qq5r9ejubnlkskr24b786qv9nd.apps.googleusercontent.com";
            var url = "http://localhost/Furniture/Admin/GoogleLoginCallback";
            var clientsecret = "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";
            var token = await GoogleAuth.GetAuthAccessToken(code, clientid, clientsecret, url);
            string userProfile = await GoogleAuth.GetProfileResponseAsync(token.AccessToken.ToString());

            // JObject objJson = new JObject.Parse()
            JObject jsonObject = JObject.Parse(userProfile);
            GoogleSignIn objGoogledetails = new GoogleSignIn();

            objGoogledetails.Email = Convert.ToString(jsonObject.SelectToken("email"));
            objGoogledetails.verified_email = Convert.ToBoolean(jsonObject.SelectToken("verified_email"));
            objGoogledetails.Name = Convert.ToString(jsonObject.SelectToken("given_name"));
            objGoogledetails.FullName = Convert.ToString(jsonObject.SelectToken("name"));
            objGoogledetails.SurName = Convert.ToString(jsonObject.SelectToken("family_name"));
            objGoogledetails.Picture = Convert.ToString(jsonObject.SelectToken("picture"));

            string res = string.Empty;
            try
            {

                new LoginBLL().GoogleLoginSign(objGoogledetails);
                objGoogleLogindetails.UserName = objGoogledetails.Email;
                objGoogleLogindetails.Email = objGoogledetails.Email;



                //objGoogleLogindetails = new LoginBLL().Authentication(out string _sessionId, true, objGoogleLogindetails);
                //if (_sessionId != null)
                //{
                //    return RedirectToAction("Home", "Home", new { Identity = _sessionId });
                //}
            }
            catch (Exception ex)
            {
                res = "failed";

            }

            return RedirectToAction("Home", "Home", new { Identity = "5D47A" });
        }

        public async Task<ActionResult> GithubLogin(string code)
        {
            var client = new HttpClient();
            var parameters = new Dictionary<string, string>
    {
        { "client_id", "Ov23liAsZOtxrg8tdkjE" },
        { "client_secret", "5f92b631547632a0a70d4ce9eaf06897ac94d096" },
        { "code", code },
        { "redirect_uri", "http://localhost/Furniture/Admin/GithubLogin" } // Ensure this matches your GitHub app settings
    };

            var content = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync("https://github.com/login/oauth/access_token", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var values = HttpUtility.ParseQueryString(responseContent);
            var access_token = values["access_token"];

            var client1 = new GitHubClient(new ProductHeaderValue("Code2night"));
            var tokenAuth = new Credentials(access_token);
            client1.Credentials = tokenAuth;

            var user = await client1.User.Current();
            var email = user.Email;

            return View(user);
        }


        public ActionResult RedirectLinkedIN(string code, string state)
        {
            try
            {
                var client = new RestSharp.RestClient("https://www.linkedin.com/oauth/v2/accessToken");
                var request = new RestRequest("Post"); // Use Method.Post

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Use Tls12 directly

                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", Request.QueryString["code"].ToString());
                request.AddParameter("redirect_uri", "http://localhost:8080/Home/RedirectLinkedIN");
                request.AddParameter("client_id", "86v6tyzuu0157l"); // Replace with your Client ID
                request.AddParameter("client_secret", "WPL_AP1.Zab2rkHvSnopvYr0.FoOmgA=="); // Replace with your Client Secret

                // Execute the request and get the response
                var response = client.Execute(request);
                var content = response.Content;
                var res = JObject.Parse(content); // Use JObject.Parse instead of JsonConvert.DeserializeObject

                var client2 = new RestSharp.RestClient("https://api.linkedin.com/v2/userinfo");
                request = new RestRequest("Get"); // Use Method.Get
                request.AddHeader("Authorization", $"Bearer {res["access_token"]}");

                var response2 = client2.Execute(request);
                var content2 = response2.Content;
                var user = JObject.Parse(content); // Use JObject.Parse instead of JsonConvert.DeserializeObject
                var useremail = JObject.Parse(content2); // Use JObject.Parse instead of JsonConvert.DeserializeObject
            }
            catch (Exception ex)
            {
                // Consider logging the exception instead of rethrowing
                throw ex;
            }

            return View();
        }

        public ActionResult Loginredirection(string sessionId)
        {
            string str = string.Format(sessionId, 5);

            //return RedirectToAction("Admin", "Home", new { Identity = sessionId });
            return RedirectToAction("Home", "Home", new { Identity = sessionId });


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Registration(RegisterDO objreg)
        {
            if (objreg == null)
            {
                return Json(new { success = false, message = "Invalid data received." });
            }

            new LoginBLL().UserRegistration(objreg);

            return Json(new { success = true, message = "User registered successfully" });
        }

    }
}