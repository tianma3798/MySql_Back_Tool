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
    /// 备份信息操作
    /// </summary>
    public class BackInfoOperate
    {
        /// <summary>
        /// 存储
        /// </summary>
        private List<BackInfo> _list = new List<BackInfo>();

        /// <summary>
        /// 构造器
        /// </summary>
        public BackInfoOperate()
        {
            //加载信息
            LoadList();
        }
        /// <summary>
        /// 获取信息列表
        /// </summary>
        /// <returns></returns>
        public List<BackInfo> GetList()
        {
            return _list;
        }

        #region 添加备份信息
        /// <summary>
        /// 添加备份信息
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <returns></returns>
        public bool Add(BackInfo info)
        {
            //判断是否已经存在
            if (Exists(info))
                return false;
            _list.Add(info);
            //保存信息
            SaveList();
            return true;
        }
        /// <summary>
        /// 移除备份信息
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <returns></returns>
        public bool Remove(BackInfo info)
        {
            return Remove(info.DBName);
        }
        /// <summary>
        /// 移除备份信息
        /// </summary>
        /// <param name="DBName">数据库名称</param>
        /// <returns></returns>
        public bool Remove(string DBName)
        {
            //判断是否存在
            if (Exists(DBName) == false)
                return false;
            BackInfo target = _list.Where(q => q.DBName == DBName).First();
            _list.Remove(target);

            //保存信息
            SaveList();

            return true;
        }
        /// <summary>
        /// 判断备份的数据库信息是否存在
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <returns></returns>
        public bool Exists(BackInfo info)
        {
            return Exists(info.DBName);
        }
        /// <summary>
        /// 判断备份的数据库信息是否存在
        /// </summary>
        /// <param name="DBName">数据库名称</param>
        /// <returns></returns>
        public bool Exists(string DBName)
        {
            return _list.Any(q => q.DBName == DBName);
        }
        #endregion

        #region 存储处理
        /// <summary>
        /// 保存信息
        /// </summary>
        public void SaveList()
        {
            string filename = GetFileName();
            try
            {
                XmlSerializerHelper _serialize = new XmlSerializerHelper(filename);
                _serialize.XmlSerialize<List<BackInfo>>(_list);
            }
            catch (Exception ex)
            {
                string msg = "序列化保存信息失败：" + ex.Message + "---文件位置：" + filename;
                BackLog.Log(msg);
                throw new Exception(msg);
            }
        }
        /// <summary>
        /// 加载信息
        /// </summary>
        public void LoadList()
        {
            string filename = GetFileName();
            try
            {
                XmlSerializerHelper _serialize = new XmlSerializerHelper(filename);
                _list = _serialize.XmlDeserialize<List<BackInfo>>();
            }
            catch (Exception ex)
            {
                //throw new Exception("反序列化获取信息失败：" + ex.Message);
                BackLog.Log("反序列化获取信息失败：" + ex.Message + "----文件位置：" + filename);
            }
        }
        /// <summary>
        /// 获取文件存储位置
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            string filename = LocalPathHelper.GetCurrentDLLSub("data") + "\\backinfo.xml";
            if (File.Exists(filename) == false)
            {
                File.Create(filename).Close();
            }
            return filename;
        }
        #endregion
    }
}
