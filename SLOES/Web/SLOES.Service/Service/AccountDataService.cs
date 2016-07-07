using System;
using System.Collections.Generic;
using FluentValidation.Results;
using SLOES.Core;
using SLOES.DAL;
using SLOES.DTO;
using SLOES.Model;
using SLOES.Util;

namespace SLOES.Service
{
    /// <summary>
    /// 账号数据服务，提供老师账号、学生账号、学生数据等数据管理服务
    /// </summary>
    public class AccountDataService
    {
        private TeacherDAL teacherDAL = DALFactory.Instance.TeacherDAL;
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AccountDataService));

        #region Teacher

        /// <summary>
        /// 老师登录
        /// </summary>
        public ServiceInvokeDTO<Teacher> TeacherLogin(string userName, string password)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<Teacher> result = null;
            try
            {
                Teacher dbTeacher = teacherDAL.GetByUserName(userName);
                if (dbTeacher != null)
                {
                    string saltPassword = SecurityUtil.MD5(password + Constant.TEACHER_SALT_KEY);
                    if (saltPassword.Equals(dbTeacher.Password))
                    {
                        result = new ServiceInvokeDTO<Teacher>(InvokeCode.SYS_INVOKE_SUCCESS, dbTeacher);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<Teacher>(InvokeCode.ACCOUNT_AGENCY_ADMIN_LOGIN_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<Teacher>(InvokeCode.ACCOUNT_AGENCY_ADMIN_LOGIN_ERROR);
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
        /// 修改老师密码
        /// </summary>
        public ServiceInvokeDTO UpdateTeacherPassword(int teacherID, string oldPwd, string newPwd)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Teacher dbTeacher = teacherDAL.GetByID(teacherID);
                if (dbTeacher != null)
                {
                    if (SecurityUtil.MD5(oldPwd + Constant.TEACHER_SALT_KEY).Equals(dbTeacher.Password))
                    {
                        dbTeacher.Password = SecurityUtil.MD5(newPwd + Constant.TEACHER_SALT_KEY);
                        teacherDAL.Update(dbTeacher);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_OLD_PWD_ERROR);
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
        /// 以分页的形式老师信息
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<Teacher>> QueryTeacher(QueryArgsDTO<Teacher> queryDTO)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<Teacher>> result = null;
            try
            {
                QueryResultDTO<Teacher> queryData = teacherDAL.Query(queryDTO);
                result = new ServiceInvokeDTO<QueryResultDTO<Teacher>>(InvokeCode.SYS_INVOKE_SUCCESS, queryData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<Teacher>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取老师信息
        /// </summary>
        public ServiceInvokeDTO<Teacher> GetTeacherByID(int teacherID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<Teacher> result = null;
            try
            {
                Teacher dbAdmin = teacherDAL.GetByID(teacherID);
                result = new ServiceInvokeDTO<Teacher>(InvokeCode.SYS_INVOKE_SUCCESS, dbAdmin);
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
        /// 添加老师
        /// </summary>
        public ServiceInvokeDTO AddTeacher(Teacher teacher)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // Check user name
                Teacher dbTeacher = teacherDAL.GetByUserName(teacher.UserName);
                if (dbTeacher == null)
                {
                    teacher.Password = SecurityUtil.MD5(teacher.Password + Constant.TEACHER_SALT_KEY);
                    teacherDAL.Insert(teacher);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_TEACHER_ACCOUNT_EXIST_ERROR);
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
        /// 更新老师
        /// </summary>
        public ServiceInvokeDTO UpdateTeacher(Teacher teacher)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // Check user name
                Teacher dbTeacher = teacherDAL.GetByUserName(teacher.UserName);
                if (dbTeacher != null && dbTeacher.ID != teacher.ID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_TEACHER_ACCOUNT_EXIST_ERROR);
                }
                else
                {
                    teacherDAL.Update(teacher);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
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
        /// 重置老师密码
        /// </summary>
        public ServiceInvokeDTO ResetTeacherPassword(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                string newPassword = SecurityUtil.MD5(SecurityUtil.MD5(Constant.TEACHER_RESET_DEFAULT_PASSWORD) + Constant.TEACHER_SALT_KEY);

                teacherDAL.UpdatePassword(id, newPassword);
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
        /// 删除老师
        /// </summary>
        public ServiceInvokeDTO DeleteTeacher(int teacherID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Teacher dbTeacher = teacherDAL.GetByID(teacherID);
                if (dbTeacher != null)
                {
                    // 系统管理员账号检测
                    if (dbTeacher.Level == TeacherLevel.SystemAdmin)
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_SYSTEM_ADMIN_NOT_DELETE_ERROR);
                    }
                    else
                    {
                        teacherDAL.DeleteByID(teacherID);
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
        /// 删除老师(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteTeacher(List<int> teacherIDs)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                bool isCanDeleted = true;
                foreach (var id in teacherIDs)
                {
                    // 检测机构管理员账号
                    Teacher teacher = teacherDAL.GetByID(id);
                    if (teacher != null && teacher.Level == TeacherLevel.SystemAdmin)
                    {
                        isCanDeleted = false;
                    }
                }

                if (isCanDeleted)
                {
                    teacherDAL.DeleteInBatch(teacherIDs);
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

        #endregion
    }
}
