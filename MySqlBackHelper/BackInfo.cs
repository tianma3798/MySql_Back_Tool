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
    /// 备份信息
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
