using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlBackHelper
{
    /// <summary>
    /// 数据库用户名密码
    /// </summary>
    public class DBUser
    {
        /// <summary>
        /// 服务器主机
        /// 没指定默认 localhost
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// 默认 3306
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        #region 构造器
        public DBUser(string Host, int Port, string UserName, string Password)
        {
            this.Host = Host;
            this.Port = Port;
            this.UserName = UserName;
            this.Password = Password;
        }
        public DBUser(string Host, string UserName, string Password) : this(Host, 3306, UserName, Password) { }
        public DBUser(string UserName, string Password) : this("localhost", UserName, Password) { }
        public DBUser(string Password) : this("root", Password) { }
        public DBUser() { }
        #endregion


        /// <summary>
        /// 当前操作的用户，密码
        /// </summary>
        private static DBUser _user = null;
        public static void SetUser()
        {
            BackHostInfo hostInfo = new BackHostInfo();
            SetUser(hostInfo);
        }
        /// <summary>
        /// 设置基本信息
        /// </summary>
        /// <param name="hostInfo"></param>
        public static void SetUser(BackHostInfo hostInfo)
        {
            SetUser(hostInfo.Host, hostInfo.Port, hostInfo.User, hostInfo.Pwd);
        }
        /// <summary>
        /// 设置用户名密码
        /// </summary>
        public static void SetUser(string Host, int Port, string UserName, string Password)
        {
            if (_user == null)
            {
                _user = new DBUser();
            }
            _user.Host = Host;
            _user.Port = Port;
            _user.UserName = UserName;
            _user.Password = Password;
        }
        /// <summary>
        /// 设置用户名密码
        /// </summary>
        public static void SetUser(string UserName, string Password)
        {
            SetUser("localhost", 3306, UserName, Password);
        }
        /// <summary>
        /// 获取当前程序使用的用户
        /// </summary>
        /// <returns></returns>
        public static DBUser GetCurrentUser()
        {
            if (_user == null)
            {
                throw new Exception("获取当前用户失败，用户对象为空");
            }
            return _user;
        }
    }
}
