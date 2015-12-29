using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace MySqlBackHelper
{
    /// <summary>
    /// 备份，主机信息
    /// </summary>
    public class BackHostInfo
    {
        /// <summary>
        /// mysql服务器主机
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// mysql服务端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 运行的时间间隔,单位秒
        /// </summary>
        public int RunSpan { get; set; }
        public BackHostInfo()
        {
            try
            {
                Host = GetValue("MySql_Host");
                Port = Convert.ToInt32(GetValue("MySql_Port"));
                User = GetValue("MySql_User");
                Pwd = GetValue("MySql_Pwd");
                RunSpan = Convert.ToInt32(GetValue("RunSpan"));
                RunSpan = RunSpan < 1000 ? 1000 : RunSpan;
            }
            catch (Exception ex)
            {
                throw new Exception("获取主机信息失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 获取配置文件中的值
        /// </summary>
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
