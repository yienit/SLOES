using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SLOES.Core;
using SLOES.DTO;
using SLOES.Model;
using SLOES.Service;
using SLOES.Util;
using SLOES.Web.Filters;

namespace SLOES.Web.Controllers
{
    /// <summary>
    /// 课程、章节、题库数据管理控制器
    /// </summary>
    [SessionTimeOutFilter]
    public class ItemController : Controller
    {
        private const string VIEW_INDEX = "index";

        private const string VIEW_COURSE_LIST = @"course\course-list";
        private const string VIEW_COURSE_ADD = @"course\course-add";
        private const string VIEW_COURSE_UPDATE = @"course\course-update";
        private const string VIEW_COURSE_DETAIL = @"course\course-detail";
        private const string VIEW_COURSE_QUERY = @"course\course-query";

        private const string VIEW_CHAPTER_LIST = @"chapter\chapter-list";
        private const string VIEW_CHAPTER_ADD = @"chapter\chapter-add";
        private const string VIEW_CHAPTER_UPDATE = @"chapter\chapter-update";
        private const string VIEW_CHAPTER_DETAIL = @"chapter\chapter-detail";

        private const string VIEW_SINGLE_LIST = @"single\single-list";
        private const string VIEW_SINGLE_ADD = @"single\single-add";
        private const string VIEW_SINGLE_UPDATE = @"single\single-update";
        private const string VIEW_SINGLE_DETAIL = @"single\single-detail";
        private const string VIEW_SINGLE_QUERY = @"single\single-query";
        private const string VIEW_SINGLE_IMPORT = @"single\single-import";

        private const string VIEW_MULTIPLE_LIST = @"multiple\multiple-list";
        private const string VIEW_MULTIPLE_ADD = @"multiple\multiple-add";
        private const string VIEW_MULTIPLE_UPDATE = @"multiple\multiple-update";
        private const string VIEW_MULTIPLE_DETAIL = @"multiple\multiple-detail";
        private const string VIEW_MULTIPLE_QUERY = @"multiple\multiple-query";
        private const string VIEW_MULTIPLE_IMPORT = @"multiple\multiple-import";

        private const string VIEW_JUDGE_LIST = @"judge\judge-list";
        private const string VIEW_JUDGE_ADD = @"judge\judge-add";
        private const string VIEW_JUDGE_UPDATE = @"judge\judge-update";
        private const string VIEW_JUDGE_DETAIL = @"judge\judge-detail";
        private const string VIEW_JUDGE_QUERY = @"judge\judge-query";
        private const string VIEW_JUDGE_IMPORT = @"judge\judge-import";

        private const string VIEW_BLANK_LIST = @"blank\blank-list";
        private const string VIEW_BLANK_ADD = @"blank\blank-add";
        private const string VIEW_BLANK_UPDATE = @"blank\blank-update";
        private const string VIEW_BLANK_DETAIL = @"blank\blank-detail";
        private const string VIEW_BLANK_QUERY = @"blank\blank-query";

        private const string VIEW_WORD_LIST = @"word\word-list";
        private const string VIEW_WORD_ADD = @"word\word-add";
        private const string VIEW_WORD_UPDATE = @"word\word-update";
        private const string VIEW_WORD_DETAIL = @"word\word-detail";
        private const string VIEW_WORD_QUERY = @"word\word-query";

        private ItemDataService itemDataService = ServiceFactory.Instance.ItemDataService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ItemController));

        #region View

        /// <summary>
        /// 索引页面
        /// </summary>
        public ActionResult Index()
        {
            return View(VIEW_INDEX);
        }

        #region Course

        /// <summary>
        /// 科目列表管理页面
        /// </summary>
        public ActionResult CourseList()
        {
            return View(VIEW_COURSE_LIST);
        }

        /// <summary>
        /// 科目添加页面
        /// </summary>
        public ActionResult CourseAddTemplate()
        {
            return View(VIEW_COURSE_ADD);
        }

        /// <summary>
        /// 科目修改页面
        /// </summary>
        public ActionResult CourseUpdateTemplate()
        {
            return View(VIEW_COURSE_UPDATE);
        }

        /// <summary>
        /// 科目详情页面
        /// </summary>
        public ActionResult CourseDetailTemplate()
        {
            return View(VIEW_COURSE_DETAIL);
        }

        /// <summary>
        /// 科目查询页面
        /// </summary>
        public ActionResult CourseQueryTemplate()
        {
            return View(VIEW_COURSE_QUERY);
        }

        #endregion

        #region Chapter

        /// <summary>
        /// 章节列表管理页面
        /// </summary>
        public ActionResult ChapterList()
        {
            return View(VIEW_CHAPTER_LIST);
        }

        /// <summary>
        /// 章节添加页面
        /// </summary>
        public ActionResult ChapterAddTemplate()
        {
            return View(VIEW_CHAPTER_ADD);
        }

        /// <summary>
        /// 章节修改页面
        /// </summary>
        public ActionResult ChapterUpdateTemplate()
        {
            return View(VIEW_CHAPTER_UPDATE);
        }

        /// <summary>
        /// 章节详情页面
        /// </summary>
        public ActionResult ChapterDetailTemplate()
        {
            return View(VIEW_CHAPTER_DETAIL);
        }

        #endregion

        #region Single

        /// <summary>
        /// 单选题列表管理页面
        /// </summary>
        public ActionResult SingleList()
        {
            return View(VIEW_SINGLE_LIST);
        }

        /// <summary>
        /// 单选题添加页面
        /// </summary>
        public ActionResult SingleAddTemplate()
        {
            return View(VIEW_SINGLE_ADD);
        }

        /// <summary>
        /// 单选题修改页面
        /// </summary>
        public ActionResult SingleUpdateTemplate()
        {
            return View(VIEW_SINGLE_UPDATE);
        }

        /// <summary>
        /// 单选题详情页面
        /// </summary>
        public ActionResult SingleDetailTemplate()
        {
            return View(VIEW_SINGLE_DETAIL);
        }

        /// <summary>
        /// 单选题查询页面
        /// </summary>
        public ActionResult SingleQueryTemplate()
        {
            return View(VIEW_SINGLE_QUERY);
        }

        /// <summary>
        /// 单选题导入页面
        /// </summary>
        public ActionResult SingleImportTemplate()
        {
            return View(VIEW_SINGLE_IMPORT);
        }

        #endregion

        #region Multiple

        /// <summary>
        /// 多选题列表管理页面
        /// </summary>
        public ActionResult MultipleList()
        {
            return View(VIEW_MULTIPLE_LIST);
        }

        /// <summary>
        /// 多选题添加页面
        /// </summary>
        public ActionResult MultipleAddTemplate()
        {
            return View(VIEW_MULTIPLE_ADD);
        }

        /// <summary>
        /// 多选题修改页面
        /// </summary>
        public ActionResult MultipleUpdateTemplate()
        {
            return View(VIEW_MULTIPLE_UPDATE);
        }

        /// <summary>
        /// 多选题详情页面
        /// </summary>
        public ActionResult MultipleDetailTemplate()
        {
            return View(VIEW_MULTIPLE_DETAIL);
        }

        /// <summary>
        /// 多选题查询页面
        /// </summary>
        public ActionResult MultipleQueryTemplate()
        {
            return View(VIEW_MULTIPLE_QUERY);
        }

        /// <summary>
        /// 多选题导入页面
        /// </summary>
        public ActionResult MultipleImportTemplate()
        {
            return View(VIEW_MULTIPLE_IMPORT);
        }

        #endregion

        #region Judge

        /// <summary>
        /// 判断题列表管理页面
        /// </summary>
        public ActionResult JudgeList()
        {
            return View(VIEW_JUDGE_LIST);
        }

        /// <summary>
        /// 判断题添加页面
        /// </summary>
        public ActionResult JudgeAddTemplate()
        {
            return View(VIEW_JUDGE_ADD);
        }

        /// <summary>
        /// 判断题修改页面
        /// </summary>
        public ActionResult JudgeUpdateTemplate()
        {
            return View(VIEW_JUDGE_UPDATE);
        }

        /// <summary>
        /// 判断题详情页面
        /// </summary>
        public ActionResult JudgeDetailTemplate()
        {
            return View(VIEW_JUDGE_DETAIL);
        }

        /// <summary>
        /// 判断题查询页面
        /// </summary>
        public ActionResult JudgeQueryTemplate()
        {
            return View(VIEW_JUDGE_QUERY);
        }

        /// <summary>
        /// 判断题导入页面
        /// </summary>
        public ActionResult JudgeImportTemplate()
        {
            return View(VIEW_JUDGE_IMPORT);
        }

        #endregion

        #region Blank

        /// <summary>
        /// 填空题列表管理页面
        /// </summary>
        public ActionResult BlankList()
        {
            return View(VIEW_BLANK_LIST);
        }

        /// <summary>
        /// 填空题添加页面
        /// </summary>
        public ActionResult BlankAddTemplate()
        {
            return View(VIEW_BLANK_ADD);
        }

        /// <summary>
        /// 填空题修改页面
        /// </summary>
        public ActionResult BlankUpdateTemplate()
        {
            return View(VIEW_BLANK_UPDATE);
        }

        /// <summary>
        /// 填空题详情页面
        /// </summary>
        public ActionResult BlankDetailTemplate()
        {
            return View(VIEW_BLANK_DETAIL);
        }

        /// <summary>
        /// 填空题查询页面
        /// </summary>
        public ActionResult BlankQueryTemplate()
        {
            return View(VIEW_BLANK_QUERY);
        }

        #endregion

        #region Word

        /// <summary>
        /// 简答题列表管理页面
        /// </summary>
        public ActionResult WordList()
        {
            return View(VIEW_WORD_LIST);
        }

        /// <summary>
        /// 简答题添加页面
        /// </summary>
        public ActionResult WordAddTemplate()
        {
            return View(VIEW_WORD_ADD);
        }

        /// <summary>
        /// 简答题修改页面
        /// </summary>
        public ActionResult WordUpdateTemplate()
        {
            return View(VIEW_WORD_UPDATE);
        }

        /// <summary>
        /// 简答题详情页面
        /// </summary>
        public ActionResult WordDetailTemplate()
        {
            return View(VIEW_WORD_DETAIL);
        }

        /// <summary>
        /// 简答题查询页面
        /// </summary>
        public ActionResult WordQueryTemplate()
        {
            return View(VIEW_WORD_QUERY);
        }

        #endregion

        #endregion

        #region Handler

        #region Course

        /// <summary>
        /// 切换当前课程科目
        /// </summary>
        [HttpPost]
        public ActionResult ExchangeCourse()
        {
            log.Debug(Constant.DEBUG_START);

            string courseIDString = ApiQueryUtil.QueryArgByPost("course_id");
            ServiceInvokeDTO result = null;
            try
            {
                HttpCookie cookie = Request.Cookies[Constant.COOKIE_NAME];
                if (cookie == null)
                {
                    cookie = new HttpCookie(Constant.COOKIE_NAME);
                    cookie.Expires = DateTime.Now.AddDays(Constant.COOKIE_EXPIRES_DAY);
                    cookie.Values.Add(Constant.COOKIE_KEY_COURSE_ID, courseIDString);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie.Values[Constant.COOKIE_KEY_COURSE_ID] = courseIDString;
                    Response.Cookies.Set(cookie);
                }

                int courseID = Convert.ToInt32(cookie[Constant.COOKIE_KEY_COURSE_ID]);
                Session[Constant.SESSION_KEY_COURSE] = itemDataService.GetCourseByID(courseID).Data;

                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
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
        /// 分页查询课程
        /// </summary>
        [HttpGet]
        public ActionResult QueryCourse()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string name = ApiQueryUtil.QueryArgByGet("name");

            QueryResultDTO<Course> queryData = null;
            try
            {
                QueryArgsDTO<Course> queryDTO = new QueryArgsDTO<Course>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;
                queryDTO.Model.Name = name;

                queryData = itemDataService.QueryCourse(queryDTO).Data;
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
        /// 添加课程
        /// </summary>
        [HttpPost]
        public ActionResult AddCourse()
        {
            log.Debug(Constant.DEBUG_START);

            string name = ApiQueryUtil.QueryArgByPost("name");
            string description = ApiQueryUtil.QueryArgByPost("description");

            ServiceInvokeDTO result = null;
            try
            {
                Course course = new Course();
                course.Name = name;
                course.Description = description;

                result = itemDataService.AddCourse(course);
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
        /// 更新课程
        /// </summary>
        [HttpPost]
        public ActionResult UpdateCourse()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string name = ApiQueryUtil.QueryArgByPost("name");
            string description = ApiQueryUtil.QueryArgByPost("description");

            ServiceInvokeDTO result = null;
            try
            {
                Course course = new Course();
                course.ID = Convert.ToInt32(idString);
                course.Name = name;
                course.Description = description;

                result = itemDataService.UpdateCourse(course);
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
        /// 删除课程
        /// </summary>
        [HttpPost]
        public ActionResult DeleteCourse()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.DeleteCourse(id);
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
        /// 删除科目信息(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteCourseInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);
                result = itemDataService.DeleteCourse(idList);
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

        #region Chapter

        /// <summary>
        /// 获取所有章节
        /// </summary>
        [HttpGet]
        public ActionResult GetAllChapter()
        {
            log.Debug(Constant.DEBUG_START);

            List<Chapter> chapters = null;
            try
            {
                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;
                ServiceInvokeDTO<List<Chapter>> result = itemDataService.GetAgencyChapters(courseID);
                if (result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    chapters = result.Data;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            string json = JsonConvert.SerializeObject(new { data = chapters }, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 添加章节
        /// </summary>
        [HttpPost]
        public ActionResult AddChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string name = ApiQueryUtil.QueryArgByPost("name");

            ServiceInvokeDTO result = null;
            try
            {
                Chapter chapter = new Chapter();
                chapter.CourseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;
                chapter.Name = name;

                result = itemDataService.AddChapter(chapter);
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
        /// 更新章节
        /// </summary>
        [HttpPost]
        public ActionResult UpdateChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string name = ApiQueryUtil.QueryArgByPost("name");

            ServiceInvokeDTO result = null;
            try
            {
                Chapter chapter = new Chapter();
                chapter.ID = Convert.ToInt32(idString);
                chapter.Name = name;

                result = itemDataService.UpdateChapter(chapter);
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
        /// 上调章节序号
        /// </summary>
        [HttpPost]
        public ActionResult UpChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.UpChapter(id);
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
        /// 下调章节序号
        /// </summary>
        [HttpPost]
        public ActionResult DownChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.DownChapter(id);
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
        /// 删除章节
        /// </summary>
        [HttpPost]
        public ActionResult DeleteChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.DeleteChapter(id);
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
        /// 删除章节(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteChapterInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);
                result = itemDataService.DeleteChapter(idList);
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

        #region Single

        /// <summary>
        /// 分页查询单选题
        /// </summary>
        [HttpGet]
        public ActionResult QuerySingle()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<SingleItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<SingleItem> queryDTO = new QueryArgsDTO<SingleItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QuerySingle(queryDTO, courseID).Data;
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
        /// 添加单选题
        /// </summary>
        [HttpPost]
        public ActionResult AddSingle()
        {
            log.Debug(Constant.DEBUG_START);

            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string a = ApiQueryUtil.QueryArgByPost("a");
            string b = ApiQueryUtil.QueryArgByPost("b");
            string c = ApiQueryUtil.QueryArgByPost("c");
            string d = ApiQueryUtil.QueryArgByPost("d");
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                SingleItem single = new SingleItem();
                single.ChapterID = Convert.ToInt32(chapterIDString);
                single.Title = title;
                single.A = a;
                single.B = b;
                single.C = c;
                single.D = d;
                single.Answer = answer;
                single.Annotation = annotation;
                single.Difficulty = Convert.ToInt32(difficultyString);
                single.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as Teacher).ChineseName;

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    single.Image = imageBytes;
                }

                result = itemDataService.AddSingle(single);
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
        /// 更新单选题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateSingle()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string a = ApiQueryUtil.QueryArgByPost("a");
            string b = ApiQueryUtil.QueryArgByPost("b");
            string c = ApiQueryUtil.QueryArgByPost("c");
            string d = ApiQueryUtil.QueryArgByPost("d");
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                SingleItem single = new SingleItem();
                single.ID = Convert.ToInt32(idString);
                single.ChapterID = Convert.ToInt32(chapterIDString);
                single.Title = title;
                single.A = a;
                single.B = b;
                single.C = c;
                single.D = d;
                single.Answer = answer;
                single.Annotation = annotation;
                single.Difficulty = Convert.ToInt32(difficultyString);

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    single.Image = imageBytes;
                }

                result = itemDataService.UpdateSingle(single);
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
        /// 删除单选题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteSingle()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.DeleteSingle(id);
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
        /// 删除单选题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteSingleInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);
                result = itemDataService.DeleteSingle(idList);
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
        /// 下载单选题题库模板文件
        /// </summary>
        [HttpGet]
        public ActionResult DownloadSingleTemplateFile()
        {
            string templatePath = Server.MapPath("/") + @"Files\Template\template_item_single.xls";
            return DownloadUtil.Download("single_template.xls", templatePath);
        }

        /// <summary>
        /// 上传单选题题库Excel文件至服务器
        /// </summary>
        [HttpPost]
        public ActionResult UploadSingleExcelFile()
        {
            log.Debug(Constant.DEBUG_START);

            // 接收上传后的文件
            HttpPostedFileBase file = Request.Files["Filedata"];

            ServiceInvokeDTO<string> result = null;
            try
            {
                if (file != null)
                {
                    // 判断临时文件夹是否存在，不存在则创建
                    string tempFolder = Server.MapPath("/") + @"Files\TempData";
                    if (!System.IO.Directory.Exists(tempFolder))
                    {
                        System.IO.Directory.CreateDirectory(tempFolder);
                    }

                    // 保存文件
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (ext.Equals(".xls") || ext.Equals(".xlsx"))
                    {
                        string tempFileName = string.Format("{0}{1}", Guid.NewGuid(), ext);
                        file.SaveAs(tempFolder + @"\" + tempFileName);
                        result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INVOKE_SUCCESS, tempFileName);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<string>(InvokeCode.ITEM_FILE_FORMAT_ERROR, null);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INNER_ERROR, null);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 开始导入单选题题库
        /// </summary>
        [HttpPost]
        public ActionResult StartLoadSingleExcelFile()
        {
            log.Debug(Constant.DEBUG_START);

            // 题库Excel文件
            string fileName = Request["file_name"];

            ServiceInvokeDTO result = null;
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFilePath = Server.MapPath("/") + @"Files\TempData\" + fileName;

                    if (System.IO.File.Exists(tempFilePath))
                    {
                        // 1.处理数据并校验
                        Course currentCourse = (Course)Session[Constant.SESSION_KEY_COURSE];
                        Teacher currentUser = (Teacher)Session[Constant.SESSION_KEY_ADMIN];
                        List<SingleItem> singles = TemplateUtil.ReadSingleTemplate(currentUser, currentCourse.ID, tempFilePath, true);

                        // 2.批量添加
                        result = itemDataService.AddSingle(singles);

                        // 3.删除文件
                        System.IO.File.Delete(tempFilePath);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ITEM_FILE_NOT_EXIST_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ITEM_FILE_FORMAT_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR, ex.Message);

                // 删除文件
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFilePath = Server.MapPath("/") + @"Files\TempData\" + fileName;
                    System.IO.File.Delete(tempFilePath);
                }
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        #endregion

        #region Multiple

        /// <summary>
        /// 分页查询多选题
        /// </summary>
        [HttpGet]
        public ActionResult QueryMultiple()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<MultipleItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<MultipleItem> queryDTO = new QueryArgsDTO<MultipleItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryMultiple(queryDTO, courseID).Data;
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
        /// 添加多选题
        /// </summary>
        [HttpPost]
        public ActionResult AddMultiple()
        {
            log.Debug(Constant.DEBUG_START);

            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string a = ApiQueryUtil.QueryArgByPost("a");
            string b = ApiQueryUtil.QueryArgByPost("b");
            string c = ApiQueryUtil.QueryArgByPost("c");
            string d = ApiQueryUtil.QueryArgByPost("d");
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                MultipleItem multiple = new MultipleItem();
                multiple.ChapterID = Convert.ToInt32(chapterIDString);
                multiple.Title = title;
                multiple.A = a;
                multiple.B = b;
                multiple.C = c;
                multiple.D = d;
                multiple.Answer = answer;
                multiple.Annotation = annotation;
                multiple.Difficulty = Convert.ToInt32(difficultyString);
                multiple.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as Teacher).ChineseName;

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    multiple.Image = imageBytes;
                }

                result = itemDataService.AddMultiple(multiple);
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
        /// 更新多选题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateMultiple()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string a = ApiQueryUtil.QueryArgByPost("a");
            string b = ApiQueryUtil.QueryArgByPost("b");
            string c = ApiQueryUtil.QueryArgByPost("c");
            string d = ApiQueryUtil.QueryArgByPost("d");
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                MultipleItem multiple = new MultipleItem();
                multiple.ID = Convert.ToInt32(idString);
                multiple.ChapterID = Convert.ToInt32(chapterIDString);
                multiple.Title = title;
                multiple.A = a;
                multiple.B = b;
                multiple.C = c;
                multiple.D = d;
                multiple.Answer = answer;
                multiple.Annotation = annotation;
                multiple.Difficulty = Convert.ToInt32(difficultyString);

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    multiple.Image = imageBytes;
                }

                result = itemDataService.UpdateMultiple(multiple);
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
        /// 删除多选题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteMultiple()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.DeleteMultiple(id);
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
        /// 删除多选题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteMultipleInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);
                result = itemDataService.DeleteMultiple(idList);
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
        /// 下载多选题题库模板文件
        /// </summary>
        [HttpGet]
        public ActionResult DownloadMultipleTemplateFile()
        {
            string templatePath = Server.MapPath("/") + @"Files\Template\template_item_multiple.xls";
            return DownloadUtil.Download("multiple_template.xls", templatePath);
        }

        /// <summary>
        /// 上传多选题题库Excel文件至服务器
        /// </summary>
        [HttpPost]
        public ActionResult UploadMultipleExcelFile()
        {
            log.Debug(Constant.DEBUG_START);

            // 接收上传后的文件
            HttpPostedFileBase file = Request.Files["Filedata"];

            ServiceInvokeDTO<string> result = null;
            try
            {
                if (file != null)
                {
                    // 判断临时文件夹是否存在，不存在则创建
                    string tempFolder = Server.MapPath("/") + @"Files\TempData";
                    if (!System.IO.Directory.Exists(tempFolder))
                    {
                        System.IO.Directory.CreateDirectory(tempFolder);
                    }

                    // 保存文件
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (ext.Equals(".xls") || ext.Equals(".xlsx"))
                    {
                        string tempFileName = string.Format("{0}{1}", Guid.NewGuid(), ext);
                        file.SaveAs(tempFolder + @"\" + tempFileName);
                        result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INVOKE_SUCCESS, tempFileName);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<string>(InvokeCode.ITEM_FILE_FORMAT_ERROR, null);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INNER_ERROR, null);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 开始导入多选题题库
        /// </summary>
        [HttpPost]
        public ActionResult StartLoadMultipleExcelFile()
        {
            log.Debug(Constant.DEBUG_START);

            // 题库Excel文件
            string fileName = Request["file_name"];

            ServiceInvokeDTO result = null;
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFilePath = Server.MapPath("/") + @"Files\TempData\" + fileName;

                    if (System.IO.File.Exists(tempFilePath))
                    {
                        // 1.处理数据并校验
                        Course currentCourse = (Course)Session[Constant.SESSION_KEY_COURSE];
                        Teacher currentUser = (Teacher)Session[Constant.SESSION_KEY_ADMIN];
                        List<MultipleItem> multiples = TemplateUtil.ReadMultipleTemplate(currentUser, currentCourse.ID, tempFilePath, true);

                        // 2.批量添加
                        result = itemDataService.AddMultiple(multiples);

                        // 3.删除文件
                        System.IO.File.Delete(tempFilePath);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ITEM_FILE_NOT_EXIST_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ITEM_FILE_FORMAT_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR, ex.Message);

                // 删除文件
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFilePath = Server.MapPath("/") + @"Files\TempData\" + fileName;
                    System.IO.File.Delete(tempFilePath);
                }
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        #endregion

        #region Judge

        /// <summary>
        /// 分页查询判断题
        /// </summary>
        [HttpGet]
        public ActionResult QueryJudge()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<JudgeItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<JudgeItem> queryDTO = new QueryArgsDTO<JudgeItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryJudge(queryDTO, courseID).Data;
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
        /// 添加判断题
        /// </summary>
        [HttpPost]
        public ActionResult AddJudge()
        {
            log.Debug(Constant.DEBUG_START);

            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string answerString = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                JudgeItem judge = new JudgeItem();
                judge.ChapterID = Convert.ToInt32(chapterIDString);
                judge.Title = title;
                judge.Answer = Convert.ToInt32(answerString);
                judge.Annotation = annotation;
                judge.Difficulty = Convert.ToInt32(difficultyString);
                judge.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as Teacher).ChineseName;

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    judge.Image = imageBytes;
                }

                result = itemDataService.AddJudge(judge);
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
        /// 更新判断题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateJudge()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string answerString = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                JudgeItem judge = new JudgeItem();
                judge.ID = Convert.ToInt32(idString);
                judge.ChapterID = Convert.ToInt32(chapterIDString);
                judge.Title = title;
                judge.Answer = Convert.ToInt32(answerString);
                judge.Annotation = annotation;
                judge.Difficulty = Convert.ToInt32(difficultyString);

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    judge.Image = imageBytes;
                }
                result = itemDataService.UpdateJudge(judge);
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
        /// 删除判断题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteJudge()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.DeleteJudge(id);
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
        /// 删除判断题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteJudgeInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);
                result = itemDataService.DeleteJudge(idList);
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
        /// 下载判断题题库模板文件
        /// </summary>
        [HttpGet]
        public ActionResult DownloadJudgeTemplateFile()
        {
            string templatePath = Server.MapPath("/") + @"Files\Template\template_item_judge.xls";
            return DownloadUtil.Download("judge_template.xls", templatePath);
        }

        /// <summary>
        /// 上传判断题题库Excel文件至服务器
        /// </summary>
        [HttpPost]
        public ActionResult UploadJudgeExcelFile()
        {
            log.Debug(Constant.DEBUG_START);

            // 接收上传后的文件
            HttpPostedFileBase file = Request.Files["Filedata"];

            ServiceInvokeDTO<string> result = null;
            try
            {
                if (file != null)
                {
                    // 判断临时文件夹是否存在，不存在则创建
                    string tempFolder = Server.MapPath("/") + @"Files\TempData";
                    if (!System.IO.Directory.Exists(tempFolder))
                    {
                        System.IO.Directory.CreateDirectory(tempFolder);
                    }

                    // 保存文件
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (ext.Equals(".xls") || ext.Equals(".xlsx"))
                    {
                        string tempFileName = string.Format("{0}{1}", Guid.NewGuid(), ext);
                        file.SaveAs(tempFolder + @"\" + tempFileName);
                        result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INVOKE_SUCCESS, tempFileName);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<string>(InvokeCode.ITEM_FILE_FORMAT_ERROR, null);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INNER_ERROR, null);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 开始导入判断题题库
        /// </summary>
        [HttpPost]
        public ActionResult StartLoadJudgeExcelFile()
        {
            log.Debug(Constant.DEBUG_START);

            // 题库Excel文件
            string fileName = Request["file_name"];

            ServiceInvokeDTO result = null;
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFilePath = Server.MapPath("/") + @"Files\TempData\" + fileName;

                    if (System.IO.File.Exists(tempFilePath))
                    {
                        // 1.处理数据并校验
                        Course currentCourse = (Course)Session[Constant.SESSION_KEY_COURSE];
                        Teacher currentUser = (Teacher)Session[Constant.SESSION_KEY_ADMIN];
                        List<JudgeItem> multiples = TemplateUtil.ReadJudgeTemplate(currentUser, currentCourse.ID, tempFilePath, true);

                        // 2.批量添加
                        result = itemDataService.AddJudge(multiples);

                        // 3.删除文件
                        System.IO.File.Delete(tempFilePath);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ITEM_FILE_NOT_EXIST_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ITEM_FILE_FORMAT_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR, ex.Message);

                // 删除文件
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFilePath = Server.MapPath("/") + @"Files\TempData\" + fileName;
                    System.IO.File.Delete(tempFilePath);
                }
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        #endregion

        #region Blank

        /// <summary>
        /// 分页查询填空题
        /// </summary>
        [HttpGet]
        public ActionResult QueryBlank()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<BlankItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<BlankItem> queryDTO = new QueryArgsDTO<BlankItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryBlank(queryDTO, courseID).Data;
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
        /// 添加填空题
        /// </summary>
        [HttpPost]
        public ActionResult AddBlank()
        {
            log.Debug(Constant.DEBUG_START);

            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            string answersJsonString = ApiQueryUtil.QueryArgByPost("answers");

            ServiceInvokeDTO result = null;
            try
            {
                BlankItem blank = new BlankItem();
                blank.ChapterID = Convert.ToInt32(chapterIDString);
                blank.Title = title;
                blank.Difficulty = Convert.ToInt32(difficultyString);
                blank.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as Teacher).ChineseName;

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    blank.Image = imageBytes;
                }

                List<BlankAnswer> answers = JsonConvert.DeserializeObject<List<BlankAnswer>>(answersJsonString);

                result = itemDataService.AddBlank(blank, answers);
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
        /// 更新填空题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateBlank()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            string answersJsonString = ApiQueryUtil.QueryArgByPost("answers");

            ServiceInvokeDTO result = null;
            try
            {
                BlankItem blank = new BlankItem();
                blank.ID = Convert.ToInt32(idString);
                blank.ChapterID = Convert.ToInt32(chapterIDString);
                blank.Title = title;
                blank.Difficulty = Convert.ToInt32(difficultyString);

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    blank.Image = imageBytes;
                }

                List<BlankAnswer> answers = JsonConvert.DeserializeObject<List<BlankAnswer>>(answersJsonString);

                result = itemDataService.UpdateBlank(blank, answers);
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
        /// 删除填空题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteBlank()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.DeleteBlank(id);
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
        /// 删除填空题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteBlankInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);
                result = itemDataService.DeleteBlank(idList);
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

        #region Word

        /// <summary>
        /// 分页查询简答题
        /// </summary>
        [HttpGet]
        public ActionResult QueryWord()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<WordItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<WordItem> queryDTO = new QueryArgsDTO<WordItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryWord(queryDTO, courseID).Data;
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
        /// 添加简答题
        /// </summary>
        [HttpPost]
        public ActionResult AddWord()
        {
            log.Debug(Constant.DEBUG_START);

            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                WordItem word = new WordItem();
                word.ChapterID = Convert.ToInt32(chapterIDString);
                word.Title = title;
                word.Answer = answer;
                word.Difficulty = Convert.ToInt32(difficultyString);
                word.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as Teacher).ChineseName;

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    word.Image = imageBytes;
                }

                result = itemDataService.AddWord(word);
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
        /// 更新简答题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateWord()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                WordItem word = new WordItem();
                word.ID = Convert.ToInt32(idString);
                word.ChapterID = Convert.ToInt32(chapterIDString);
                word.Title = title;
                word.Answer = answer;
                word.Difficulty = Convert.ToInt32(difficultyString);

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    word.Image = imageBytes;
                }
                result = itemDataService.UpdateWord(word);
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
        /// 删除简答题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteWord()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);
                result = itemDataService.DeleteWord(id);
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
        /// 删除简答题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteWordInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);
                result = itemDataService.DeleteWord(idList);
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
