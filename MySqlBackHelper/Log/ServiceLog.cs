using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlBackHelper
{
    /// <summary>
    /// 服务启动运行，日志记录
    /// </summary>
    public class ServiceLog
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        static LogHelper.LogHelper _log = null;
        /// <summary>
        /// 静态内容初始化
        /// </summary>
        static ServiceLog()
        {
            //将 日志记录放在当前DLL的目录下
            string fullname = LocalPathHelper.GetCurrentDLLSub("log") + "\\Back_Service_Log.txt";
            _log = new LogHelper.LogHelper(fullname, true);
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="str"></param>
        public static void Log(string str)
        {
            _log.WriteLine(str);
        }
        /// <summary>
        /// 获取记录操作对象
        /// </summary>
        public static LogHelper.LogHelper GetLog()
        {
            return _log;
        }
    }
}
