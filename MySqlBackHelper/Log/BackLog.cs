using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlBackHelper
{
    /// <summary>
    /// 备份操作，日志记录
    /// </summary>
    public class BackLog
    {
        static LogHelper.LogHelper _log = null;
        /// <summary>
        /// 静态内容初始化
        /// </summary>
        static BackLog()
        {
            //将 日志记录放在当前DLL的目录下
            string fullname = LocalPathHelper.GetCurrentDLLSub("log") + "\\MySqlBack_Log.txt";
            _log = new LogHelper.LogHelper(fullname,true);
        }
        /// <summary>
        /// 写入内容
        /// </summary>
        /// <param name="content">字符串内容</param>
        public static void Log(string content)
        {
            _log.WriteLine(content);
        }
        /// <summary>
        /// 获取当前记录操作对象
        /// </summary>
        /// <returns></returns>
        public static LogHelper.LogHelper GetLog()
        {
            return _log;
        }
    }
}
