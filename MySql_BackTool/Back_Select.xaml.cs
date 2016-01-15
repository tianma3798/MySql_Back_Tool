using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MySqlBackHelper;
//using System.Windows.Forms;

namespace MySql_BackTool
{
    /// <summary>
    /// Back_Select.xaml 的交互逻辑
    /// </summary>
    public partial class Back_Select : Window
    {
        public Back_Select()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string DBName = txtDBName.Text;
            string fileName = txtFile.Text;
            if (DBName.Length <= 0)
            {
                txtDBName.Focus();
                return;
            }
            if (fileName.Length <= 0)
            {
                MessageBox.Show("还没有选择源文件");
                return;
            }
            try
            {
                //确定还原本地数据库
                HostInfo hostInfo = new HostInfo();
                hostInfo.Host = "192.168.1.126";
                hostInfo.Port = 3306;
                hostInfo.User = "root";
                hostInfo.Pwd = "123";
                hostInfo.InfoName = "本地蔚蓝留学网数据库";
                hostInfo.Character = "local_wlliuxue";
                hostInfo.InfoSummary = "介绍";

                BackDBTool _dbTool = new BackDBTool(hostInfo, DBName);
                if (_dbTool.Exec_Restore_File(fileName))
                {
                    MessageBox.Show("备份成功");
                }
                else
                {
                    MessageBox.Show("备份失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份失败:" + ex.Message);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog _file = new System.Windows.Forms.OpenFileDialog();
            _file.Title = "选择备份文件";
            _file.Filter = "sql文件|*.sql";
            _file.FileName = string.Empty;
            _file.RestoreDirectory = true;

            System.Windows.Forms.DialogResult result = _file.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            string fileName = _file.FileName;
            txtFile.Text = fileName;
        }
    }
}
