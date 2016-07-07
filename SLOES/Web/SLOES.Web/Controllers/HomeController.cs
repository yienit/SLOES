using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SLOES.Util;
using SLOES.DTO;
using SLOES.Model;
using Newtonsoft.Json;
using SLOES.Core;
using SLOES.Service;

namespace SLOES.Web.Controllers
{
    /// <summary>
    /// 官网主页控制器
    /// </summary>
    public class HomeController : Controller
    {
        private const string VIEW_LOGIN = "login";

        private AccountDataService accountDataService = ServiceFactory.Instance.AccountDataService;
        private ItemDataService itemDataService = ServiceFactory.Instance.ItemDataService;
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        /// <summary>
        /// 登录页面
        /// </summary>
        public ActionResult Login()
        {
            string tipMsg = TempData[Constant.LOG_TIP_VIEW_AND_TEMP_KEY] == null ? string.Empty : TempData[Constant.LOG_TIP_VIEW_AND_TEMP_KEY].ToString();
            ViewData[Constant.LOG_TIP_VIEW_AND_TEMP_KEY] = tipMsg;

            return View(VIEW_LOGIN);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        [HttpPost]
        public ActionResult LoginEx()
        {
            log.Debug(Constant.DEBUG_START);

            string userName = ApiQueryUtil.QueryArgByPost("user_name");
            string password = ApiQueryUtil.QueryArgByPost("pwd_hidden");

            try
            {
                // Write cookies
                HttpCookie cookie = Request.Cookies[Constant.COOKIE_NAME];
                if (cookie == null)
                {
                    cookie = new HttpCookie(Constant.COOKIE_NAME);
                    cookie.Expires = DateTime.Now.AddDays(Constant.COOKIE_EXPIRES_DAY);
                    cookie.Values.Add(Constant.COOKIE_KEY_USER_NAME, userName);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie.Values[Constant.COOKIE_KEY_USER_NAME] = userName;
                    Response.Cookies.Set(cookie);
                }

                ServiceInvokeDTO<Teacher> loginResult = accountDataService.TeacherLogin(userName, password);
                if (loginResult != null && loginResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    // Write session
                    Session[Constant.SESSION_KEY_ADMIN] = loginResult.Data;

                    int courseID = 0;
                    List<Course> courses = itemDataService.GetAllCourse().Data;
                    if (courses != null && courses.Count > 0)
                    {
                        courseID = courses[0].ID;
                    }
                    if (cookie != null && cookie[Constant.COOKIE_KEY_COURSE_ID] != null)
                    {
                        courseID = Convert.ToInt32(cookie[Constant.COOKIE_KEY_COURSE_ID]);
                    }
                    Session[Constant.SESSION_KEY_COURSE] = itemDataService.GetCourseByID(courseID).Data;

                    // To dashboard
                    if (loginResult.Data.Level == TeacherLevel.SystemAdmin)
                    {
                        return RedirectToAction("index", "account");
                    }
                    else
                    {
                        return RedirectToAction("index", "item");
                    }
                }
                else
                {
                    //Pass result string to login action.
                    TempData[Constant.LOG_TIP_VIEW_AND_TEMP_KEY] = loginResult.Message;
                    return RedirectToAction("login", "home");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return View("~/Views/Shared/error.cshtml");
            }
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        [HttpGet]
        public ActionResult Logout()
        {
            log.Debug(Constant.DEBUG_START);

            try
            {
                // Remove session
                if (Session[Constant.SESSION_KEY_ADMIN] != null)
                {
                    Session[Constant.SESSION_KEY_ADMIN] = null;
                }
                if (Session[Constant.SESSION_KEY_COURSE] != null)
                {
                    Session[Constant.SESSION_KEY_COURSE] = null;
                }
                return RedirectToAction("login", "home");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return View("~/Views/Shared/error.cshtml");
            }
        }
    }
}
