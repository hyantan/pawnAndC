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
using System.Windows.Threading;
using Newtonsoft.Json;
namespace Launcher
{
    public partial class Form1 : Form
    {
        private DispatcherTimer timer = null;
        Bitmap enterState = (Bitmap)Image.FromFile("KNOPKAA2.png");
        Bitmap normalState = (Bitmap)Image.FromFile("KNOPKA4.png");
        /*private String Online()
        {
            string userAgentString = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Win64; x64; Trident/4.0; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; .NET CLR 2.0.50727; SLCC2; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; .NET4.0C; .NET4.0E)";
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Headers.Add("user-agent", userAgentString);
            String Response = wc.DownloadString("//");
            //<div class="online-count">91 <span>online</span></div
            // <div class="online-count">91 <span>online</span></div>
            // <span class="img_online">84/1000</span>
            String Rate = System.Text.RegularExpressions.Regex.Match(Response, @"serverid="[0-100000]""").Groups[1].Value;
            return "АксонБанк: " + Rate + " р. \r\n";
        } */
        public Form1()
        {
            InitializeComponent();


            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10000);
            timer.Start();

            UserSave Usave = new UserSave();

            try
            {

                using (StreamReader sw = new StreamReader(Directory.GetCurrentDirectory() + "/set.json"))
                {

                    string json = sw.ReadToEnd();
                    Usave = JsonConvert.DeserializeObject<UserSave>(json);
                    User.nickname = Usave.nickname;
                    User.path = Usave.path;
                    sw.Close();
                }
                this.Input_Login.Text = User.nickname;
            }
            catch
            {

            }

        }
        System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            System.Net.WebClient wc = new System.Net.WebClient();

            //Number Of Bytes Downloaded Are Stored In ‘data’
            byte[] data = wc.DownloadData("https://yandex.by");

            //DateTime Variable To Store Download End Time.
            DateTime dt2 = DateTime.Now;

            //To Calculate Speed in Kb Divide Value Of data by 1024 And Then by End Time Subtract Start Time To Know Download Per Second.
            double a = Math.Round(((data.Length) / 10.24) / (dt2 - dt1).TotalSeconds, 2);
            label3.Text = a + " MB/sek";
        }
        Point LastPoint;
        private void border_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);
        }

        private void border_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void save_login_Click(object sender, EventArgs e)
        {
            UserSave Usave = new UserSave();
            Usave.nickname = User.nickname = Input_Login.Text;

            Usave.path = User.path;
            String serialized = JsonConvert.SerializeObject(Usave);

            using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "/set.json"))
            {
                sw.Write(serialized);
                sw.Close();
            }
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


            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string url = "https://";
            string html = string.Empty;
            string pattern = "Сейчас: (.*) / 1000";


            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream());
            html = myStreamReader.ReadToEnd();


            Match match = Regex.Match(html, pattern);
            string oao = match.Groups[1].ToString();
            label1.Text = oao;
            //
            System.Net.WebClient wc = new System.Net.WebClient();

            DateTime dt1 = DateTime.Now;

            byte[] data = wc.DownloadData("https://yandex.by");

            DateTime dt2 = DateTime.Now;

            double a = Math.Round(((data.Length) / 34.1) / (dt2 - dt1).TotalSeconds, 2);
            label3.Text = a + " MB/sek";

        }
        static public class User
        {
            public static string nickname = "Nickname";
            public static string path = " ";
            public static string ip = "147.135.198.9";
        }
        public class UserSave
        {
            public string nickname = "Nickname";
            public string path = " ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form ifrm1 = new MainWindow();
            //ifrm1.Close(); // отображаем Form2
            //this.Close();
            System.IO.File.Delete(@"3412.poit");
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void border_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ifrm = new MainWindow();
            ifrm.Show(); // отображаем Form2
        }

        private void Input_Login_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbNickname_TextChanged(object sender, EventArgs e)
        {

        }

        private void bSettings_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ifrm = new Form2();
            ifrm.Show(); // отображаем Form2
            this.Hide(); // скрываем Form1 (this - текущая форма)
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       

        private void inGame_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(User.path))//Если директория указана
            {
                String sobeitCheat = User.path + "/d3d9.dll";
                String DestFile = User.path + "/models/txd/other.txd";
                string src = Directory.GetCurrentDirectory() + @"/myPack";
                string dest = User.path + "/models/txd";


                if (File.Exists(sobeitCheat))
                {
                    MessageBox.Show("Папка с игрой содержит запрещенный файл d3d9.dll!");
                }
                else
                {

                    if (File.Exists(DestFile))//если нет файлов .txd
                        CopyDir.Copy(src, dest);// - загружаем

                    Process.Start(
                            User.path + "//samp.exe",
                            User.ip + " - n" + Input_Login.Text);//запускаем
                }
                //
            }
            else MessageBox.Show("Выберете путь с GTA SA");
        }

        private void inGame_MouseMove(object sender, MouseEventArgs e)
        {
            inGame.Image = enterState;
        }

        private void inGame_MouseLeave(object sender, EventArgs e)
        {
            inGame.Image = normalState;
        }

        private void bModpack_Click(object sender, EventArgs e)
        {
            string link = @"http:////"; //ссылка на файл
            WebClient webClient = new WebClient();
            var client = new WebClient();
            DateTime dt2 = DateTime.Now;
            webClient.DownloadProgressChanged += (o, args) => progressBar2.Value = args.ProgressPercentage;
            webClient.DownloadFileCompleted += (o, args) => progressBar2.Value = 100;
            string save_path = "D:\\testik\\1";
            webClient.DownloadFileAsync(new Uri(link), save_path); //куда сохранить, в данный момент в папку где запущена программа
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_BackColorChanged(object sender, EventArgs e)
        {
        }
    }
}
