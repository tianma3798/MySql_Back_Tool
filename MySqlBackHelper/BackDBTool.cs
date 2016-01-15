using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MySqlBackHelper
{
    public class BackDBTool : BackBase
    {
        #region 构造器
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="DBName">数据库名称</param>
        /// <param name="TargetName">备份文件名称</param>
        /// <param name="TargetPath">备份目录</param>
        public BackDBTool(HostInfo HostInfo, string DBName, string TargetName, string TargetPath) : base(HostInfo)
        {
            this.DBName = DBName;
            this.TargetName = TargetName;
            this.TargetPath = TargetPath;
        }
        /// <summary>
        /// 不指定备份文件名，则与数据库名相同
        /// </summary>
        /// <param name="DBName">数据库名称</param>
        /// <param name="TargetPath">备份文件名称</param>
        public BackDBTool(HostInfo HostInfo, string DBName, string TargetPath) : this(HostInfo, DBName, DBName, TargetPath)
        { }
        /// <summary>
        /// 不指定路径默认人到d：盘
        /// </summary>
        public BackDBTool(HostInfo HostInfo, string DBName) : this(HostInfo, DBName, DBName, "d:") { }
        #endregion

        /// <summary>
        /// 获取备份语句
        /// </summary>
        /// <param name="rename">是否重命名</param>
        /// <returns></returns>
        public string GetSql_Back(bool rename = true)
        {
            string sql = "mysqldump -h {0} -u {1} -p{2} {3} > {4}.sql";
            string targetname = this.TargetName;
            if (rename)
            {
                targetname += "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            }
            targetname = this.TargetPath + targetname;
            sql = string.Format(sql,
                HostInfo.Host,
                HostInfo.User,
                HostInfo.Pwd,
                DBName, targetname);
            return sql;
        }

        /// <summary>
        /// 执行备份
        /// 执行成功返回内容
        /// Warning: Using a password on the command line interface can be insecure.
        /// </summary>
        /// <returns></returns>
        public bool Exec_Back()
        {
            try
            {
                string str = this.GetSql_Back();
                string result = CmdHelper.RunCmd_Err(str);
                result = RegexHelper.RemoveEmptyLine(result);
                //备份数据库成功
                if (result == "Warning: Using a password on the command line interface can be insecure.")
                {
                    //日志
                    Log_Back(str);
                    return true;
                }
                else
                {
                    Log("执行备份命令失败，命令行返回：" + result);
                }
            }
            catch (Exception ex)
            {
                Log("执行数据库‘" + DBName + "’备份出错：" + ex.Message + ",当前执行语句：" + this.GetSql_Back());
                throw ex;
            }
            return false;
        }

        #region 还原
        /// <summary>
        /// 获取 还原语句
        /// </summary>
        /// <returns></returns>
        public string GetSql_Resotre(string newFileName = null)
        {
            string targetname = this.TargetName;
            if (string.IsNullOrEmpty(newFileName) == false)
            {
                targetname = newFileName;
            }
            targetname = this.TargetPath + targetname;
            return GetSql_Restore_File(targetname);
        }
        /// <summary>
        /// 获取 还原语句
        /// </summary>
        /// <param name="fullName">文件路径</param>
        /// <returns></returns>
        public string GetSql_Restore_File(string fullName)
        {
            string sql = "mysql -h {0} -u {1} -p{2} {3} < {4}";
            sql = string.Format(sql,
                HostInfo.Host,
                HostInfo.User,
                HostInfo.Pwd,
                DBName, fullName);
            return sql;
        }
        /// <summary>
        /// 执行还原命令
        /// 执行命令成功
        /// Warning: Using a password on the command line interface can be insecure.
        /// </summary>
        /// <returns></returns>
        public bool Exec_Restore(string NewFileName = null)
        {
            return Exec(this.GetSql_Resotre(NewFileName));
        }
        /// <summary>
        /// 执行还原命令
        /// </summary>
        /// <param name="FullFile">文件路径</param>
        /// <returns></returns>
        public bool Exec_Restore_File(string FullFile)
        {
          return  Exec(GetSql_Restore_File(FullFile));
        }
        /// <summary>
        /// 执行还原命令
        /// </summary>
        /// <param name="cmd">命令语句</param>
        /// <returns></returns>
        public bool Exec(string cmd)
        {
            try
            {
                string result = CmdHelper.RunCmd_Err(cmd);
                result = RegexHelper.RemoveEmptyLine(result);
                if (result == "Warning: Using a password on the command line interface can be insecure.")
                {
                    //日志
                    Log_Restore(cmd);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log("执行数据库‘" + DBName + "’还原出错：" + ex.Message);
                throw ex;
            }
            return false;
        }
        /// <summary>
        /// 自动获取备份文件加中最新的文件
        /// </summary>
        /// <returns></returns>
        public bool Exec_Restore_Auto()
        {
            string lastFile = GetLastTimeFile();
            return Exec_Restore(lastFile);
        }
        /// <summary>
        /// 获取备份文件夹中最新的文件
        /// </summary>
        /// <returns></returns>
        public string GetLastTimeFile()
        {
            //1.获取文件夹中最新的源文件
            string[] files = Directory.GetFiles(this.TargetPath, this.TargetName + "*.sql");
            if (files == null || files.Length <= 0)
            {
                throw new Exception("还原数据库是获取源文件失败");
            }
            DateTime lastTime = DateTime.MinValue;
            string lastName = "";
            foreach (var item in files)
            {
                FileInfo info = new FileInfo(item);
                if (info.LastWriteTime > lastTime)
                {
                    lastTime = info.LastWriteTime;
                    lastName = info.Name;
                }
            }
            if (lastName == "")
            {
                lastName = new FileInfo(files.First()).Name;
            }
            return lastName;
        }
        #endregion

    }
}
