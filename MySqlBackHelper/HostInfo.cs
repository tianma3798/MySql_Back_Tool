using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace MySqlBackHelper
{
    /// <summary>
    /// 备份，主机信息,用户信息
    /// </summary>
    public class HostInfo
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
        /// 当前备份名称
        /// </summary>
        public string InfoName { get; set; }
        /// <summary>
        /// 当前备份字符表示
        /// 唯一标示
        /// </summary>
        public string Character { get; set; }
        /// <summary>
        /// 当前备份服务器信息简介描述
        /// </summary>
        public string InfoSummary { get; set; }
        /// <summary>
        /// 当前主机信息是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public HostInfo()
        {
        }


        /// <summary>
        /// 执行当前主机所有数据库的备份
        /// </summary>
        public void Exec_Back()
        {
            BackInfoOperate _operate = new BackInfoOperate(Character);
            //获取数据库信息列表
            List<BackInfo> infoList = _operate.GetList();
            foreach (var item in infoList)
            {
                //执行备份操作
                item.Exec_Back(this);
            }
        }
    }
}
