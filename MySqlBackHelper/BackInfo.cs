using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using Common;
using System.IO;

namespace MySqlBackHelper
{
    /// <summary>
    /// 备份数据库，备份时间等信息
    /// </summary>
    public class BackInfo
    {
        /// <summary>
        /// 备份的数据库
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 备份目标文件 没有文件后缀
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        /// 备份目标目录
        /// </summary>
        public string TargetPath { get; set; }
        /// <summary>
        /// 自动备份类型
        /// 1---每天备份
        /// </summary>
        public BackType AutoType { get; set; }
        /// <summary>
        /// 备份的时间表
        /// </summary>
        public List<DateTime> TimeList = new List<DateTime>();

        /// <summary>
        /// 序列化需要无参构造函数
        /// </summary>
        public BackInfo()
        {
        }

        public BackInfo(string DBName, List<DateTime> TimeList)
        {
            this.DBName = DBName;
            this.TargetName = DBName;
            this.TargetPath = "d:";
            this.TimeList = TimeList;
            this.AutoType = BackType.每天备份;
        }

        /// <summary>
        /// 解析时间
        /// </summary>
        /// <param name="timeStr"></param>
        /// <returns></returns>
        public DateTime getDateTime(string timeStr)
        {
            int[] result = timeStr.Split(':')
                .Select(q => Convert.ToInt32(q))
                .ToArray();
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, result[0], result[1], 0);
        }


        /// <summary>
        /// 判断是否可以备份
        /// </summary>
        /// <returns></returns>
        public bool IsCanBack()
        {
            if (AutoType == BackType.每天备份)
            {
                DateTime now = DateTime.Now;
                return TimeList.Any(q => q.Hour == now.Hour && q.Minute == now.Minute);
            }
            return false;
        }

        /// <summary>
        /// 执行当前备份
        /// </summary>
        /// <param name="hostInfo">指定主机信息</param>
        public void Exec_Back(HostInfo hostInfo)
        {
            if (IsCanBack())
            {
                //先获取配置，再备份
                BackDBTool _dbTool = new BackDBTool(hostInfo,
                    this.DBName, this.TargetName, this.TargetPath);
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


    /// <summary>
    /// 备份类型
    /// </summary>
    public enum BackType
    {
        每天备份 = 1,
        每周备份 = 2
    }


}
