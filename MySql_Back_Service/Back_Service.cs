using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using MySqlBackHelper;

namespace MySql_Back_Service
{
    public partial class Back_Service : ServiceBase
    {
        /// <summary>
        /// 服务启动记录
        /// </summary>
        LogHelper.LogHelper _log = ServiceLog.GetLog();
        BackOperate_Auto _operate = null;
        public Back_Service()
        {
            InitializeComponent();
            try
            {
                _operate = new BackOperate_Auto();
            }
            catch (Exception ex)
            {
                _log.WriteLine("初始化，操作对象失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 服务启动
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            _log.WriteLine(">>>>>>>MySql备份服务启动>>>>");
            _operate.Start();
        }
        /// <summary>
        /// 服务结束
        /// </summary>
        protected override void OnStop()
        {
            _log.WriteLine("<<<<<<<<MySql备份服务终止<<<<");
            _operate.Stop();
        }
    }
}
