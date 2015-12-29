using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MySqlBackHelper
{
    /// <summary>
    /// 执行cmd命令方法
    /// </summary>
    public class CmdHelper
    {

        /// <summary>
        /// mysql 程序的bin目录
        /// </summary>
        public static string MySqlBinPath { get; set; }

        /// <summary>
        /// 执行一句批处理命令
        /// ---从StandardOutput 流中读取结果
        /// </summary>
        /// <param name="str">命令内容</param>
        /// <returns></returns>
        public static string RunCmd(string str)
        {
            string result = string.Empty;
            try
            {
                using (Process p=new Process())
                {
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;

                    if (string.IsNullOrEmpty(MySqlBinPath) == false)
                        p.StartInfo.WorkingDirectory = MySqlBinPath;
                    //p.StartInfo.WorkingDirectory = @"C:\Windows\system32>";
                    p.Start();

                    string cmd = str + "&exit";
                    p.StandardInput.WriteLine(cmd);
                    p.StandardInput.AutoFlush = true;

                    result = p.StandardOutput.ReadToEnd();

                    p.WaitForExit();
                    p.Close();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("执行cmd命令失败："+ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 执行一句批处理命令
        /// ---StandardError 流中读取结果
        /// </summary>
        /// <param name="str">命令内容</param>
        /// <returns></returns>
        public static string RunCmd_Err(string str)
        {
            string result = string.Empty;
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;

                    if (string.IsNullOrEmpty(MySqlBinPath) == false)
                        p.StartInfo.WorkingDirectory = MySqlBinPath;
                    p.Start();

                    string cmd = str + "&exit";
                    p.StandardInput.WriteLine(cmd);
                    p.StandardInput.AutoFlush = true;

                    result = p.StandardError.ReadToEnd();

                    p.WaitForExit();
                    p.Close();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("执行cmd命令失败：" + ex.Message);
            }

            return result;
        }
    }
}
