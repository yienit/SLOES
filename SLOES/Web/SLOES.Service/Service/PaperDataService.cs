using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLOES.DAL;
using SLOES.DTO;
using SLOES.Model;
using SLOES.Core;

namespace SLOES.Service
{
    /// <summary>
    /// 试卷数据管理服务，提供模拟试卷、历年试卷管理服务
    /// </summary>
    public class PaperDataService
    {
        private PaperDAL paperDAL = DALFactory.Instance.PaperDAL;
        private CourseDAL courseDAL = DALFactory.Instance.CourseDAL;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ItemDataService));

        #region Paper

        /// <summary>
        /// 以分页的形式查询考卷信息
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<Paper>> QueryPaper(QueryArgsDTO<Paper> queryDTO)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<Paper>> result = null;
            try
            {
                QueryResultDTO<Paper> resultData = paperDAL.Query(queryDTO);
                result = new ServiceInvokeDTO<QueryResultDTO<Paper>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<Paper>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取试卷信息
        /// </summary>
        public ServiceInvokeDTO<Paper> GetPaperByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<Paper> result = null;
            try
            {
                Paper paper = paperDAL.GetByID(id);
                result = new ServiceInvokeDTO<Paper>(InvokeCode.SYS_INVOKE_SUCCESS, paper);
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
        /// 添加试卷
        /// </summary>
        public ServiceInvokeDTO AddPaper(Paper paper)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // Check user name
                //Paper dbPaper = paperDAL.GetByID();
                //if (dbPaper == null)
                //{
                //    teacher.Password = SecurityUtil.MD5(teacher.Password + Constant.TEACHER_SALT_KEY);
                //    teacherDAL.Insert(teacher);
                //    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                //}
                //else
                //{
                //    result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_TEACHER_ACCOUNT_EXIST_ERROR);
                //}
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
