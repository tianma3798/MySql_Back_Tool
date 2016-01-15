using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using MySqlBackHelper;
using System.IO;
using System.Threading;

namespace MySql_Back.test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string result = DateTime.Now.ToString("yyyy-MM-dd") + " 24:00:00";
            //Console.WriteLine(Convert.ToDateTime(result));


            //string result = DateTime.Now.ToString("yyyy年MM月dd日") + " 24:00:00";
            //DateTime date = DateTime.ParseExact(result, 
            //    "yyyy-MM-dd HH:mm:ss", new System.Globalization.CultureInfo("zh-CN"));
            //Console.WriteLine(date);
            //Console.WriteLine(DateTime.Now.ToString("yyyyMMdd_HHmmss"));


            HelperTwo();

            //TestTwo();
            //TestOne();
            Console.Read();
        }


        public static void HelperTwo()
        {
            ////添加主机信息
            //HostInfo hostInfo = new HostInfo();
            //hostInfo.Host = "192.168.1.126";
            //hostInfo.Port = 3306;
            //hostInfo.User = "root";
            //hostInfo.Pwd = "123";
            //hostInfo.InfoName = "本地蔚蓝留学网数据库";
            //hostInfo.Character = "local_wlliuxue";
            //hostInfo.InfoSummary = "介绍";
            //HostInfoOperate hostOperate = new HostInfoOperate();
            //hostOperate.Add(hostInfo);

            //BackInfoOperate _info;
            ////添加备份信息
            //_info = new BackInfoOperate();
            //_info.Add(new BackInfo("test", new List<DateTime>() {
            //    DateTime.Now.AddMinutes(1),
            //    DateTime.Now.AddMinutes(3)
            //}));

            BackOperate_Auto _operate = new BackOperate_Auto();
            _operate.Start();
        }

        public static void TestTwo()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WorkingDirectory = @"C:\Windows\system32>";
            p.Start();

            string str = @"mysqldump - u root - p123 test > d:\test_20151227110010.sql";
            p.StandardInput.WriteLine(str + "&exit");
            p.StandardInput.AutoFlush = true;
            string result = p.StandardOutput.ReadToEnd();

            p.WaitForExit();
            p.Close();
            Console.WriteLine(result);
        }
        //备份还原mysql
        public static void TestOne()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            //指定MySql程序的bin 目录
            //p.StartInfo.WorkingDirectory = @"E:\mysql-5.6.26-winx64\bin";
            p.Start();

            /*************
            * 备份命令
            **************/
            //简单格式
            //string strSql = "mysqldump --quick --host=localhost -u root -p123 test > d:\\test_20151227110010.sql";
            /*命令指定格式*/
            //String command = "mysqldump --quick --host=localhost --default-character-set=gb2312 --lock-tables --verbose  --force --port=端口号 --user=用户名 --password=密码 数据库名 -r 备份到的地址";
            //string strSql = "mysqldump --quick --host=localhost  --default-character-set=gb2312 --lock-tables --verbose  --force --port=3306 --user=root --password=123 test -r d:\\one.sql";

            /*************
            * 还原命令
            **************/
            //简单格式
            //string strSql = "mysql  --host=localhost -u root -p123 test < d:\\test_20151227110010.sql";
            /**还原命令格式**/
            //string s = "mysql --host=localhost --default-character-set=gbk --port=端口号 --user=用户名 --password=密码 数据库名<还原文件所在路径";
            string strSql = "mysql  --host=localhost --port=3306 --user=root --password=123 test < d:\\test_20151227110010.sql";

            p.StandardInput.WriteLine(strSql + " &exit");
            p.StandardInput.AutoFlush = true;

            /****执行命令，没有返回可验证的结果*****/
            //显示方式1
            //StreamReader reader = p.StandardOutput;
            //string line = reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    Console.WriteLine(line);
            //    line = reader.ReadLine();
            //}

            //显示方式2
            string result = p.StandardOutput.ReadToEnd();
            Console.WriteLine(result);

            //返回警告结果 --Warning: Using a password on the command line interface can be insecure.
            string result2 = p.StandardError.ReadToEnd();
            Console.WriteLine(result2);

            //显示方式3
            ShowValue(p);

            p.WaitForExit();
            p.Close();
        }

        private static async void ShowValue(Process p)
        {
            string result = await p.StandardOutput.ReadToEndAsync();
            Console.WriteLine(result);
        }

        private static void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void RunCmd2(string cmdExe, string cmdStr)
        {
            using (Process p = new Process())
            {

            }
        }


    }
}
