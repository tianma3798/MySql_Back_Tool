using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlBackHelper
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class BackLog
    {

        /// <summary>
        /// 日志记录
        /// </summary>
        static LogHelper.LogHelper _log = new LogHelper.LogHelper("Back_Service_Log");

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="str"></param>
        public static void Log(string str)
        {
            _log.WriteLine(str);
        }
    }
}
