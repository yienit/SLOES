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
using SLOES.Web.Filters;

namespace SLOES.Web.Controllers
{
    /// <summary>
    /// 账号数据控制器
    /// </summary>
    [SessionTimeOutFilter]
    public class AccountController : Controller
    {
        private const string VIEW_INDEX = "index";

        private const string VIEW_TEACHER_LIST = @"teacher\teacher-list";
        private const string VIEW_TEACHER_ADD = @"teacher\teacher-add";
        private const string VIEW_TEACHER_UPDATE = @"teacher\teacher-update";
        private const string VIEW_TEACHER_DETAIL = @"teacher\teacher-detail";

        private const string VIEW_STUDENT_LIST = @"student\student-list";

        private AccountDataService accountDataService = ServiceFactory.Instance.AccountDataService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AccountController));

        #region View

        /// <summary>
        /// 索引页面
        /// </summary>
        public ActionResult Index()
        {
            return View(VIEW_INDEX);
        }

        #region Teacher

        /// <summary>
        /// 老师账号管理页面
        /// </summary>
        public ActionResult TeacherList()
        {
            return View(VIEW_TEACHER_LIST);
        }

        /// <summary>
        /// 老师添加页面
        /// </summary>
        public ActionResult TeacherAddTemplate()
        {
            return View(VIEW_TEACHER_ADD);
        }

        /// <summary>
        /// 老师更新页面
        /// </summary>
        public ActionResult TeacherUpdateTemplate()
        {
            return View(VIEW_TEACHER_UPDATE);
        }

        /// <summary>
        /// 老师详情页面
        /// </summary>
        public ActionResult TeacherDetailTemplate()
        {
            return View(VIEW_TEACHER_DETAIL);
        }

        #endregion

        #region Student

        /// <summary>
        /// 学生账号管理页面
        /// </summary>
        public ActionResult StudentList()
        {
            return View(VIEW_STUDENT_LIST);
        }

        #endregion

        #endregion

        #region Hanlder

        #region Teacher

        /// <summary>
        /// 老师修改密码
        /// </summary>
        [HttpPost]
        public ActionResult UpdateTeacherPwd()
        {
            log.Debug(Constant.DEBUG_START);

            string oldPwd = ApiQueryUtil.QueryArgByPost("old_pwd");
            string newPwd = ApiQueryUtil.QueryArgByPost("new_pwd");

            ServiceInvokeDTO result = null;
            try
            {
                int teacherID = (Session[Constant.SESSION_KEY_ADMIN] as Teacher).ID;
                result = accountDataService.UpdateTeacherPassword(teacherID, oldPwd, newPwd);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 分页查询老师信息
        /// </summary>
        [HttpGet]
        public ActionResult QueryTeacher()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chineseName = ApiQueryUtil.QueryArgByGet("chinese_name");
            QueryResultDTO<Teacher> queryData = null;
            try
            {
                QueryArgsDTO<Teacher> queryDTO = new QueryArgsDTO<Teacher>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.ChineseName = chineseName;
                queryDTO.Model.Level = (TeacherLevel)(-1);

                queryData = accountDataService.QueryTeacher(queryDTO).Data;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            string json = JsonConvert.SerializeObject(queryData, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 添加老师
        /// </summary>
        [HttpPost]
        public ActionResult AddTeacher()
        {
            log.Debug(Constant.DEBUG_START);

            string chineseName = ApiQueryUtil.QueryArgByPost("chinese_name");
            string userName = ApiQueryUtil.QueryArgByPost("user_name");
            string password = ApiQueryUtil.QueryArgByPost("password");
            string isCanMarkPaperString = ApiQueryUtil.QueryArgByPost("is_can_mark_paper");

            ServiceInvokeDTO result = null;
            try
            {
                Teacher teacher = new Teacher();
                teacher.ChineseName = chineseName;
                teacher.UserName = userName;
                teacher.Password = password;
                teacher.Level = TeacherLevel.ItemAdmin;
                teacher.IsCanMarkPaper = Convert.ToInt32(isCanMarkPaperString);

                result = accountDataService.AddTeacher(teacher);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 更新老师
        /// </summary>
        [HttpPost]
        public ActionResult UpdateTeacher()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string chineseName = ApiQueryUtil.QueryArgByPost("chinese_name");
            string userName = ApiQueryUtil.QueryArgByPost("user_name");
            string isCanMarkPaperString = ApiQueryUtil.QueryArgByPost("is_can_mark_paper");

            ServiceInvokeDTO result = null;
            try
            {
                Teacher teacher = new Teacher();
                teacher.ID = Convert.ToInt32(idString);
                teacher.ChineseName = chineseName;
                teacher.UserName = userName;
                teacher.IsCanMarkPaper = Convert.ToInt32(isCanMarkPaperString);
                result = accountDataService.UpdateTeacher(teacher);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 重置老师密码
        /// </summary>
        [HttpPost]
        public ActionResult UpdateTeacherPassword()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = accountDataService.ResetTeacherPassword(id);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 删除老师
        /// </summary>
        [HttpPost]
        public ActionResult DeleteTeacher()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int adminID = Convert.ToInt32(idString);
                result = accountDataService.DeleteTeacher(adminID);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 删除老师(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteTeacherInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);
                result = accountDataService.DeleteTeacher(idList);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        #endregion

        #endregion
    }
}
