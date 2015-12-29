using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlBackHelper
{
    /// <summary>
    /// 备份使用的基本信息
    /// </summary>
    public class BackBase
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public DBUser _user = null;
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 备份目标名称
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        /// 备份目标的路径
        /// </summary>
        private string _TargetPath;
        public string TargetPath
        {
            get
            {
                if (_TargetPath.EndsWith("/") == false && _TargetPath.EndsWith("\\") == false)
                    return _TargetPath + "/";
                return _TargetPath;
            }
            set
            {
                _TargetPath = value;
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        private LogHelper.LogHelper _log = null;
        public BackBase()
        {
            _log = new LogHelper.LogHelper("MySqlBack_Log");
            _user = DBUser.GetCurrentUser();
        }


        /// <summary>
        /// 这是MySql 的bin目录
        /// 如果没有设置环境变量，则需要手动设置mysq的bin目录位置
        /// </summary>
        /// <param name="MySqlBinPath"></param>
        public void SetBinPath(string MySqlBinPath)
        {
            CmdHelper.MySqlBinPath = MySqlBinPath;
        }


        /// <summary>
        /// 添加备份数据库日志
        /// </summary>
        public void Log_Back(string cmd)
        {
            _log.WriteLine("数据库'" + DBName + "'备份成功，执行命令语句：" + cmd);
        }
        /// <summary>
        /// 添加还原数据库日志
        /// </summary>
        public void Log_Restore(string cmd)
        {
            _log.WriteLine("数据库'" + DBName + "'还原成功，执行命令语句：" + cmd);
        }
        public void Log(string str)
        {
            _log.WriteLine(str);
        }
    }
}
