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
        BackHostInfo _hotsInfo = null;
        public BackOperate_Auto()
        {
            //初始化备份参数
            Init();
            //初始化线程
            if (_backThread == null)
            {
                _backThread = new Thread(new ThreadStart(Run));
                _backThread.Priority = ThreadPriority.Normal;
            }
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
                    BackInfoOperate _operate = new BackInfoOperate();
                    List<BackInfo> infoList = _operate.GetList();
                    foreach (var _info in infoList)
                    {
                        if (_info.IsCanBack())
                        {
                            //先获取配置，再备份
                            BackDBTool _dbTool = new BackDBTool(_info.DBName, _info.TargetName, _info.TargetPath);
                            //开始备份
                            if (_dbTool.Exec_Back() == false)
                            {
                                throw new Exception("备份文件失败,对应数据库名称："
                                    + _dbTool.DBName + ",备份语句："
                                    + _dbTool.GetSql_Back());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    BackLog.Log("线程方法执行失败：" + ex.Message);
                }
                //等待10分钟
                Thread.Sleep(_hotsInfo.RunSpan);
            }
        }
        //初始化参数
        private void Init()
        {
            //先获取配置，再备份
            _hotsInfo = new BackHostInfo();
            DBUser.SetUser(_hotsInfo);
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
