using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Launcher;
using System.Management;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace Launcher
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete(@"3412.poit");
            Application.Exit();
        }

        private void folder_browser_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string destFileName = folderBrowserDialog1.SelectedPath + "/samp.exe";
                if (File.Exists(destFileName))
                {
                    User.path = folderBrowserDialog1.SelectedPath;
                    UserSave Usave = new UserSave();
                    Usave.path = folderBrowserDialog1.SelectedPath;
                    Usave.nickname = User.nickname;
                    string serialized = JsonConvert.SerializeObject(Usave);
                    using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "/set.json"))
                    {
                        sw.Write(serialized);
                        sw.Close();
                    }
                }
                else MessageBox.Show("Указан неверный путь, samp.exe не найден!");
                textBox1.Text = folderBrowserDialog1.SelectedPath;


            }
        }
        static public class User
        {
            public static string nickname = "Nickname";
            public static string path = " ";
            public static string ip = "///";
        }
        public class UserSave
        {
            public string nickname = "Nickname";
            public string path = " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
                foreach (ManagementObject queryObj in searcher2.Get())
                {
                    label1.Text = string.Format("Ваша видеокарта: {0}", queryObj["Caption"]);
                }
                ManagementObjectSearcher searcher3 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject queryObj in searcher3.Get())
                {
                    label2.Text = string.Format("Ваш процессор: {0}", queryObj["Caption"]);
                }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
