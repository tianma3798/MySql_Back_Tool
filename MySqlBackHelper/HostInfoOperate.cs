using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlBackHelper
{
    /// <summary>
    /// 主机信息操作
    /// </summary>
    public class HostInfoOperate
    {
        /// <summary>
        /// 存储备份主机信息
        /// </summary>
        private List<HostInfo> _list = new List<HostInfo>();

        /// <summary>
        /// 初始化
        /// </summary>
        public HostInfoOperate()
        {
            //加载信息
            LoadList();
        }
        /// <summary>
        /// 获取主机信息列表
        /// </summary>
        /// <returns></returns>
        public List<HostInfo> GetList()
        {
            return _list;
        }

        #region 操作主机信息
        /// <summary>
        /// 获取信息对象
        /// </summary>
        /// <param name="Character">字符标识</param>
        /// <returns></returns>
        public HostInfo Get(string Character)
        {
            return _list.Where(q => q.Character == Character).FirstOrDefault();
        }
        /// <summary>
        /// 添加主机信息
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <returns></returns>
        public bool Add(HostInfo info)
        {
            //判断是否已经存在
            if (Exists(info))
                return false;
            _list.Add(info);
            //保存
            SaveList();
            return true;
        }
        /// <summary>
        /// 移除主机信息
        /// </summary>
        /// <param name="info">信息名称字符标识</param>
        /// <returns></returns>
        public bool Remove(HostInfo info)
        {
            return Remove(info.Character);
        }
        /// <summary>
        /// 移除主机信息
        /// </summary>
        /// <param name="Character">信息名称字符标识</param>
        /// <returns></returns>
        public bool Remove(string Character)
        {
            //判断是否存在
            if (Exists(Character) == false)
                return false;
            HostInfo info = _list.Where(q => q.Character == Character).FirstOrDefault();
            _list.Remove(info);
            //保存信息
            SaveList();
            return true;
        }
        /// <summary>
        /// 判断信息是否已经存在
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <returns></returns>
        public bool Exists(HostInfo info)
        {
            return Exists(info.Character);
        }
        /// <summary>
        /// 判断信息是否已经存在
        /// </summary>
        /// <param name="Character">信息名称字符标识</param>
        /// <returns></returns>
        public bool Exists(string Character)
        {
            return _list.Any(q => q.Character == Character);
        }
        #endregion

        #region 存储处理
        /// <summary>
        /// 保存信息到磁盘
        /// </summary>
        public void SaveList()
        {
            string filename = GetFileName();
            try
            {
                XmlSerializerHelper _serialize = new XmlSerializerHelper(filename);
                _serialize.XmlSerialize<List<HostInfo>>(_list);
            }
            catch (Exception ex)
            {
                string msg = "序列化保存主机信息失败：" + ex.Message + "----文件位置：" + filename;
                ServiceLog.Log(msg);
                throw new Exception(msg);
            }
        }
        /// <summary>
        /// 加载磁盘信息
        /// </summary>
        public void LoadList()
        {
            string filename = GetFileName();
            try
            {
                XmlSerializerHelper _serialize = new XmlSerializerHelper(filename);
                _list = _serialize.XmlDeserialize<List<HostInfo>>();
            }
            catch (Exception ex)
            {
                ServiceLog.Log("反序列化获取主机列表信息失败：" + ex.Message + "----文件位置:" + filename);
            }
        }
        /// <summary>
        /// 获取 文件存储位置
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            string filename = LocalPathHelper.GetCurrentDLLSub("data") + "\\hostinfo.xml";

            if (File.Exists(filename) == false)
            {
                File.Create(filename).Close();
            }

            return filename;
        }
        #endregion
    }
}
