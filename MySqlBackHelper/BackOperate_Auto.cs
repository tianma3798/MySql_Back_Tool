using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Threading;

namespace MySqlBackHelper
{
    /// <summary>
    /// 自动备份操作
    /// </summary>
    public class BackOperate_Auto
    {
        /// <summary>
        /// 自动备份线程，唯一
        /// </summary>
        static Thread _backThread;
        /// <summary>
        /// 当前轮询的时间间隔
        /// </summary>
        int RunSpan = 1000;

        public BackOperate_Auto()
        {
            //初始化线程
            if (_backThread == null)
            {
                _backThread = new Thread(new ThreadStart(Run));
                _backThread.Priority = ThreadPriority.Normal;
            }
            RunSpan= Common.ConfigValue.GetInt("RunSpan");
        }
        /// <summary>
        /// 线程运行方法
        /// </summary>
        private void Run()
        {
            while (true)
            {
                try
                {
                    //获取所有主机信息，执行备份
                    HostInfoOperate _operate = new HostInfoOperate();
                    List<HostInfo> list = _operate.GetList();
                    foreach (var item in list)
                    {
                        item.Exec_Back();
                    }
                }
                catch (Exception ex)
                {
                    ServiceLog.Log("线程方法执行失败：" + ex.Message);
                }
                //等待10分钟
                Thread.Sleep(RunSpan);
            }
        }
        /// <summary>
        /// 启动工作
        /// </summary>
        public void Start()
        {
            _backThread.Start();
        }
        /// <summary>
        /// 结束工作
        /// </summary>
        public void Stop()
        {
            _backThread.Abort();
        }
    }
}
