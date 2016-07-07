using System;
using System.Collections.Generic;
using SLOES.Core;
using SLOES.DAL;
using SLOES.DTO;
using SLOES.Model;

namespace SLOES.Service
{
    /// <summary>
    /// 题库管理服务，提供对课程、章节、题库等数据管理服务
    /// </summary>
    public class ItemDataService
    {
        private CourseDAL courseDAL = DALFactory.Instance.CourseDAL;
        private ChapterDAL chapterDAL = DALFactory.Instance.ChapterDAL;

        private SingleItemDAL singleDAL = DALFactory.Instance.SingleItemDAL;
        private MultipleItemDAL multipleDAL = DALFactory.Instance.MultipleItemDAL;
        private JudgeItemDAL judgeDAL = DALFactory.Instance.JudgeItemDAL;
        private BlankItemDAL blankDAL = DALFactory.Instance.BlankItemDAL;
        private WordItemDAL wordDAL = DALFactory.Instance.WordItemDAL;

        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ItemDataService));

        #region Course

        /// <summary>
        /// 以分页的形式查询科目信息
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<Course>> QueryCourse(QueryArgsDTO<Course> queryDTO)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<Course>> result = null;
            try
            {
                QueryResultDTO<Course> resultData = courseDAL.Query(queryDTO);
                result = new ServiceInvokeDTO<QueryResultDTO<Course>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<Course>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 获取所有考试科目信息
        /// </summary>
        public ServiceInvokeDTO<List<Course>> GetAllCourse()
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<List<Course>> result = null;
            try
            {
                List<Course> courses = courseDAL.GetAll();
                result = new ServiceInvokeDTO<List<Course>>(InvokeCode.SYS_INVOKE_SUCCESS, courses);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据科目主键ID获取科目信息
        /// </summary>
        public ServiceInvokeDTO<Course> GetCourseByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<Course> result = null;
            try
            {
                Course course = courseDAL.GetByID(id);
                result = new ServiceInvokeDTO<Course>(InvokeCode.SYS_INVOKE_SUCCESS, course);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加科目信息
        /// </summary>
        public ServiceInvokeDTO AddCourse(Course course)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // check name
                Course dbCourse = courseDAL.GetByName(course.Name);
                if (dbCourse == null)
                {
                    courseDAL.Insert(course);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ITEM_COURSE_NAME_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新科目信息
        /// </summary>
        public ServiceInvokeDTO UpdateCourse(Course course)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // check name
                Course dbCourse = courseDAL.GetByName(course.Name);
                if (dbCourse != null && dbCourse.ID != course.ID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.ITEM_COURSE_NAME_EXIST_ERROR);
                }
                else
                {
                    courseDAL.Update(course);
                }
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除科目信息
        /// </summary>
        public ServiceInvokeDTO DeleteCourse(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                courseDAL.DeleteByID(id);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除科目信息(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteCourse(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                courseDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Chapter

        /// <summary>
        /// 根据课程主键ID获取考试课程下的所有章节信息
        /// </summary>
        public ServiceInvokeDTO<List<Chapter>> GetAgencyChapters(int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<List<Chapter>> result = null;
            try
            {
                List<Chapter> chapters = chapterDAL.GetByCourseID(courseID);
                result = new ServiceInvokeDTO<List<Chapter>>(InvokeCode.SYS_INVOKE_SUCCESS, chapters);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取章节
        /// </summary>
        public ServiceInvokeDTO<Chapter> GetChapterByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<Chapter> result = null;
            try
            {
                Chapter chapter = chapterDAL.GetByID(id);
                result = new ServiceInvokeDTO<Chapter>(InvokeCode.SYS_INVOKE_SUCCESS, chapter);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加章节
        /// </summary>
        public ServiceInvokeDTO AddChapter(Chapter chapter)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // 检测章节名称是否存在
                Chapter dbChapter = chapterDAL.GetByName(chapter.CourseID, chapter.Name);
                if (dbChapter == null)
                {
                    // 叠加章节序号
                    chapter.ChapterIndex = chapterDAL.GetLastChapterIndex(chapter.CourseID) + 1;

                    chapterDAL.Insert(chapter);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ITEM_CHAPTER_NAME_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新章节
        /// </summary>
        public ServiceInvokeDTO UpdateChapter(Chapter chapter)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Chapter dbChapter = chapterDAL.GetByID(chapter.ID);
                if (dbChapter != null)
                {
                    // 检测章节名称是否存在
                    Chapter chapterWithName = chapterDAL.GetByName(dbChapter.CourseID, chapter.Name);
                    if (chapterWithName != null && chapterWithName.ID != chapter.ID)
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ITEM_CHAPTER_NAME_EXIST_ERROR);
                    }
                    else
                    {
                        chapter.ChapterIndex = dbChapter.ChapterIndex;
                        chapterDAL.Update(chapter);
                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 上调章节序号
        /// </summary>
        public ServiceInvokeDTO UpChapter(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Chapter currentChapter = chapterDAL.GetByID(id);
                if (currentChapter != null)
                {
                    // 前一个章节
                    Chapter foreChapter = chapterDAL.GetForeChapter(currentChapter.CourseID, currentChapter.ChapterIndex);

                    if (foreChapter != null)
                    {
                        // 互换章节序号
                        chapterDAL.UpdateChapterIndex(currentChapter.ID, foreChapter.ChapterIndex);
                        chapterDAL.UpdateChapterIndex(foreChapter.ID, currentChapter.ChapterIndex);
                    }

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 下调章节序号
        /// </summary>
        public ServiceInvokeDTO DownChapter(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Chapter currentChapter = chapterDAL.GetByID(id);
                if (currentChapter != null)
                {
                    // 后一个章节
                    Chapter afterChapter = chapterDAL.GetAfterChapter(currentChapter.CourseID, currentChapter.ChapterIndex);

                    if (afterChapter != null)
                    {
                        // 互换章节序号
                        chapterDAL.UpdateChapterIndex(currentChapter.ID, afterChapter.ChapterIndex);
                        chapterDAL.UpdateChapterIndex(afterChapter.ID, currentChapter.ChapterIndex);
                    }

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除章节
        /// </summary>
        public ServiceInvokeDTO DeleteChapter(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Chapter dbChapter = chapterDAL.GetByID(id);
                if (dbChapter != null)
                {
                    chapterDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除章节(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteChapter(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                chapterDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Single

        /// <summary>
        /// 以分页的形式单选题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<SingleItemDTO>> QuerySingle(QueryArgsDTO<SingleItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<SingleItemDTO>> result = null;
            try
            {
                QueryResultDTO<SingleItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<SingleItem> queryData = singleDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<SingleItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<SingleItemDTO> dtos = new List<SingleItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var single in queryData.List)
                        {
                            SingleItemDTO singleDTO = new SingleItemDTO(single);
                            singleDTO.ChapterName = chapterDAL.GetByID(single.ChapterID).Name;
                            dtos.Add(singleDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<SingleItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<SingleItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取单选题
        /// </summary>
        public ServiceInvokeDTO<SingleItemDTO> GetSingleByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<SingleItemDTO> result = null;
            try
            {
                SingleItemDTO singleDTO = null;

                // --> DTO
                SingleItem single = singleDAL.GetByID(id);
                if (single != null)
                {
                    singleDTO = new SingleItemDTO(single);
                    singleDTO.ChapterName = chapterDAL.GetByID(single.ChapterID).Name;
                }
                result = new ServiceInvokeDTO<SingleItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, singleDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加单选题
        /// </summary>
        public ServiceInvokeDTO AddSingle(SingleItem single)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                singleDAL.Insert(single);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加单选题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddSingle(List<SingleItem> singles)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                singleDAL.Insert(singles);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新单选题
        /// </summary>
        public ServiceInvokeDTO UpdateSingle(SingleItem single)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                singleDAL.Update(single);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除单选题
        /// </summary>
        public ServiceInvokeDTO DeleteSingle(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                singleDAL.DeleteByID(id);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除单选题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteSingle(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                singleDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Multiple

        /// <summary>
        /// 以分页的形式多选题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<MultipleItemDTO>> QueryMultiple(QueryArgsDTO<MultipleItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<MultipleItemDTO>> result = null;
            try
            {
                QueryResultDTO<MultipleItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<MultipleItem> queryData = multipleDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<MultipleItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<MultipleItemDTO> dtos = new List<MultipleItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var multiple in queryData.List)
                        {
                            MultipleItemDTO multipleDTO = new MultipleItemDTO(multiple);
                            multipleDTO.ChapterName = chapterDAL.GetByID(multiple.ChapterID).Name;
                            dtos.Add(multipleDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<MultipleItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<MultipleItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取多选题
        /// </summary>
        public ServiceInvokeDTO<MultipleItemDTO> GetMultipleByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<MultipleItemDTO> result = null;
            try
            {
                MultipleItemDTO multipleDTO = null;

                // --> DTO
                MultipleItem multiple = multipleDAL.GetByID(id);
                if (multiple != null)
                {
                    multipleDTO = new MultipleItemDTO(multiple);
                    multipleDTO.ChapterName = chapterDAL.GetByID(multiple.ChapterID).Name;
                }
                result = new ServiceInvokeDTO<MultipleItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, multipleDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加多选题
        /// </summary>
        public ServiceInvokeDTO AddMultiple(MultipleItem multiple)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                multipleDAL.Insert(multiple);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加多选题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddMultiple(List<MultipleItem> multiples)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                multipleDAL.Insert(multiples);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新多选题
        /// </summary>
        public ServiceInvokeDTO UpdateMultiple(MultipleItem multiple)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                multipleDAL.Update(multiple);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除多选题
        /// </summary>
        public ServiceInvokeDTO DeleteMultiple(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                multipleDAL.DeleteByID(id);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除多选题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteMultiple(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                multipleDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Judge

        /// <summary>
        /// 以分页的形式判断题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<JudgeItemDTO>> QueryJudge(QueryArgsDTO<JudgeItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<JudgeItemDTO>> result = null;
            try
            {
                QueryResultDTO<JudgeItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<JudgeItem> queryData = judgeDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<JudgeItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<JudgeItemDTO> dtos = new List<JudgeItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var judge in queryData.List)
                        {
                            JudgeItemDTO judgeDTO = new JudgeItemDTO(judge);
                            judgeDTO.ChapterName = chapterDAL.GetByID(judge.ChapterID).Name;
                            dtos.Add(judgeDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<JudgeItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<JudgeItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取判断题
        /// </summary>
        public ServiceInvokeDTO<JudgeItemDTO> GetJudgeByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<JudgeItemDTO> result = null;
            try
            {
                JudgeItemDTO judgeDTO = null;

                // --> DTO
                JudgeItem judge = judgeDAL.GetByID(id);
                if (judge != null)
                {
                    judgeDTO = new JudgeItemDTO(judge);
                    judgeDTO.ChapterName = chapterDAL.GetByID(judge.ChapterID).Name;
                }
                result = new ServiceInvokeDTO<JudgeItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, judgeDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加判断题
        /// </summary>
        public ServiceInvokeDTO AddJudge(JudgeItem judge)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                judgeDAL.Insert(judge);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加判断题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddJudge(List<JudgeItem> judges)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                judgeDAL.Insert(judges);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新判断题
        /// </summary>
        public ServiceInvokeDTO UpdateJudge(JudgeItem judge)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                judgeDAL.Update(judge);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除判断题
        /// </summary>
        public ServiceInvokeDTO DeleteJudge(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                judgeDAL.DeleteByID(id);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除判断题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteJudge(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                judgeDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Blank

        /// <summary>
        /// 以分页的形式查询填空题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<BlankItemDTO>> QueryBlank(QueryArgsDTO<BlankItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<BlankItemDTO>> result = null;
            try
            {
                QueryResultDTO<BlankItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<BlankItem> queryData = blankDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<BlankItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<BlankItemDTO> dtos = new List<BlankItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var blank in queryData.List)
                        {
                            BlankItemDTO blankDTO = new BlankItemDTO(blank);
                            blankDTO.ChapterName = chapterDAL.GetByID(blank.ChapterID).Name;
                            blankDTO.Answers = blankDAL.GetAnswers(blank.ID);
                            dtos.Add(blankDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<BlankItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<BlankItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取填空题
        /// </summary>
        public ServiceInvokeDTO<BlankItemDTO> GetBlankByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<BlankItemDTO> result = null;
            try
            {
                BlankItemDTO blankDTO = null;

                // --> DTO
                BlankItem blank = blankDAL.GetByID(id);
                if (blank != null)
                {
                    blankDTO = new BlankItemDTO(blank);
                    blankDTO.ChapterName = chapterDAL.GetByID(blank.ChapterID).Name;
                    blankDTO.Answers = blankDAL.GetAnswers(blank.ID);
                }
                result = new ServiceInvokeDTO<BlankItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, blankDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加填空题
        /// </summary>
        public ServiceInvokeDTO AddBlank(BlankItem blank, List<BlankAnswer> answers)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                blankDAL.Insert(blank, answers);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新填空题
        /// </summary>
        public ServiceInvokeDTO UpdateBlank(BlankItem blank, List<BlankAnswer> answers)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                blankDAL.Update(blank, answers);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除填空题
        /// </summary>
        public ServiceInvokeDTO DeleteBlank(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                blankDAL.DeleteByID(id);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除填空题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteBlank(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                blankDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Word

        /// <summary>
        /// 以分页的形式查询简答题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<WordItemDTO>> QueryWord(QueryArgsDTO<WordItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<WordItemDTO>> result = null;
            try
            {
                QueryResultDTO<WordItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<WordItem> queryData = wordDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<WordItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<WordItemDTO> dtos = new List<WordItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var word in queryData.List)
                        {
                            WordItemDTO wordDTO = new WordItemDTO(word);
                            wordDTO.ChapterName = chapterDAL.GetByID(word.ChapterID).Name;
                            dtos.Add(wordDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<WordItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<WordItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取简答题
        /// </summary>
        public ServiceInvokeDTO<WordItemDTO> GetWordByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<WordItemDTO> result = null;
            try
            {
                WordItemDTO wordDTO = null;

                // --> DTO
                WordItem word = wordDAL.GetByID(id);
                if (word != null)
                {
                    wordDTO = new WordItemDTO(word);
                    wordDTO.ChapterName = chapterDAL.GetByID(word.ChapterID).Name;
                }
                result = new ServiceInvokeDTO<WordItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, wordDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加简答题
        /// </summary>
        public ServiceInvokeDTO AddWord(WordItem word)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                wordDAL.Insert(word);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加简答题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddWord(List<WordItem> words)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                wordDAL.Insert(words);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新简答题
        /// </summary>
        public ServiceInvokeDTO UpdateWord(WordItem word)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                wordDAL.Update(word);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除简答题
        /// </summary>
        public ServiceInvokeDTO DeleteWord(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                wordDAL.DeleteByID(id);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除简答题
        /// </summary>
        public ServiceInvokeDTO DeleteWord(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                wordDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

    }
}
