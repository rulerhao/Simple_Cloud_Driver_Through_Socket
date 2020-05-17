using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Security;
using System.Xml;

namespace SocketClentRemake
{
    public partial class Form1 : Form
    {
        static string Now_Dir = Directory.GetCurrentDirectory();
        static string Receive_Path = Now_Dir + @"\DATA";
        XmlDocument xml = new XmlDocument();
        bool Input_Account_Bool = false;
        bool Input_Password_Bool = false;

        string Account;

        Socket Socket_Ori = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        public Form1()
        {
            InitializeComponent();
            //初始的狀態顯示
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine(Receive_Path);
            Control_Panel.Visible = false;
            Text_Panel.Visible = false;

            Show_Log_In_Statue.Text = "請輸入帳號";
            //TCP port  
            int Port = 6000;
            string Host = "192.168.56.1";//伺服器端ip地址
            IPAddress IP = IPAddress.Parse(Host);
            IPEndPoint IPE = new IPEndPoint(IP, Port);
            try
            {
                Socket_Ori.Connect(IPE);
            }
            catch
            {
                MessageBox.Show("Server is not online.Please open when server is online");
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        //判斷是否帳號密碼已輸入，如果都已輸入，則回傳ture
        private bool Entry_Qualification()
        {
            bool Qualification = false;
            byte[] Sends_First_String_Byte = Encoding.UTF8.GetBytes(textBox1.Text);

            //如果還沒輸入過帳號 在此輸入帳號
            if (Input_Account_Bool == false)
            {
                Input_Account_Bool = true;
                Socket_Ori.Send(Sends_First_String_Byte);
                Show_Log_In_Statue.Text = "請輸入密碼";
                Account = textBox1.Text;
            }
            //如果已經輸入過帳號 則在此判斷密碼
            else if (Input_Account_Bool == true)
            {
                Input_Password_Bool = true;
                Socket_Ori.Send(Sends_First_String_Byte);
            }
            //如果帳號密碼都已輸入
            if (Input_Account_Bool == true &&
                Input_Password_Bool == true)
            {
                Qualification = true;
            }
            return (Qualification);
        }
        //判斷接收後的訊息正確與否
        private bool Correct_Or_Error(Socket Soc)
        {

            //如果伺服器回傳正確 則關閉Log in Panel
            if (Receive_And_Bytes_To_Str(Soc) == "Correct")
            {
                return (true);
            }
            //如果伺服器回傳錯誤則Reset
            else
            {
                return (false);
            }
        }
        private string Receive_And_Bytes_To_Str(Socket Soc)
        {
            byte[] Receive_Message_Byte = new byte[1024 * 50000];

            //在此等待接收後才繼續執行
            int Receive_Message_Byte_Length = Soc.Receive(Receive_Message_Byte);

            string Message = Encoding.UTF8.GetString(Receive_Message_Byte, 0, Receive_Message_Byte_Length);
            return (Message);
        }

        //回覆已接收完畢
        static void Return_Finish_Bytes(Socket Soc)
        {
            //回覆訊息接收完畢
            byte[] Return_Finish_Byte = Encoding.UTF8.GetBytes("Finish");
            Soc.Send(Return_Finish_Byte);
        }

        //傳送字串到另一個socket
        private void Send_String_Messages(Socket Soc, String str)
        {
            byte[] Sends_Message_Byte = Encoding.UTF8.GetBytes(str);
            Soc.Send(Sends_Message_Byte);
        }


        //傳送有幾個檔案給另一個Socket

        //接收完成訊息 確認完成
        static void Get_Finish_Byte(Socket Soc)
        {
            byte[] Get_Finish_Byte = new byte[1024 * 50000];
            Soc.Receive(Get_Finish_Byte);
            Console.WriteLine("Get Finish Message");
        }

        static int Get_Files_Num(Socket Soc)
        {
            int Files_Num;
            byte[] Get_Data_Byte = new byte[1024 * 50000];
            int Get_Data_Byte_Length = Soc.Receive(Get_Data_Byte);
            string Receive_Files_Num_Str = Encoding.UTF8.GetString(Get_Data_Byte, 0, Get_Data_Byte_Length);
            Files_Num = Int32.Parse(Receive_Files_Num_Str);
            return (Files_Num);
        }
        //接收多個檔案
        static void Get_Multi_Files(Socket Soc, string Receive_Path, int Files_Num)
        {
            for (int i = 0; i < Files_Num; i++)
            {
                byte[] Get_Data = new byte[1024 * 50000];
                int Received_Bytes_Len = Soc.Receive(Get_Data);
                int File_Name_Len = BitConverter.ToInt32(Get_Data, 0);
                string File_Name = Encoding.ASCII.GetString(Get_Data, 4, File_Name_Len);
                Console.WriteLine("Client:{0} connected & File {1} started received.", Soc.RemoteEndPoint, File_Name);
                BinaryWriter bWrite = new BinaryWriter(File.Open(Receive_Path + @"\" + File_Name, FileMode.Create));
                bWrite.Write(Get_Data, 4 + File_Name_Len, Received_Bytes_Len - 4 - File_Name_Len);
                Console.WriteLine("File: {0} received & saved at path: {1}", File_Name, Receive_Path);
                bWrite.Close();
                //回覆該檔案接收完畢
                Return_Finish_Bytes(Soc);
            }
        }
        static void Send_Files_Num(Socket Soc, int Files_Num)
        {
            string Files_Num_Str = Files_Num.ToString();
            byte[] Sends_Files_Num_Byte = Encoding.UTF8.GetBytes(Files_Num_Str);
            Soc.Send(Sends_Files_Num_Byte);
        }
        static void Send_Multi_Files(Socket Soc, string[] Files_Name_With_Dir, string Data_Dir)
        {
            int Files_Num = Files_Name_With_Dir.Length;

            for (int i = 0; i < Files_Num; i++)
            {
                string File_Name = Cut_String_To_Get_Files_Name(Data_Dir, Files_Name_With_Dir[i]);

                Console.WriteLine(File_Name);
                byte[] fileNameByte = Encoding.ASCII.GetBytes(File_Name);

                byte[] fileData = File.ReadAllBytes(Data_Dir + @"\" + File_Name);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                Soc.Send(clientData);
                Console.WriteLine("File:{0} has been sent.", File_Name);

                Get_Finish_Byte(Soc);
            }

        }

        static string Cut_String_To_Get_Files_Name(string Dir, string Dir_With_Name)
        {
            string Files_Name = Dir_With_Name.Substring(Dir.Length + 1);
            return (Files_Name);
        }

        //按鍵判斷區\\
        //按下X關閉時
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Send_String_Messages(Socket_Ori, "Finish");
        }
        
        private void Send_Button_Click(object sender, EventArgs e)
        {
            //如果帳號密碼都已輸入
            if (Entry_Qualification() == true)
            {
                //判斷回傳訊息正確與否 若正確則進入主畫面
                if (Correct_Or_Error(Socket_Ori) == true)
                {
                    MessageBox.Show("Correct And Log in");
                    Log_In_Panel.Hide();
                    Text_Panel.Visible = true;
                    Control_Panel.Visible = true;
                    Account_Text.Text = Account;
                }
                //錯誤則重新輸入
                else
                {
                    MessageBox.Show("Error And Try Again");
                    Input_Account_Bool = false;
                    Input_Password_Bool = false;
                    Show_Log_In_Statue.Text = "請輸入帳號";

                }
            }
        }   

        private void Choose_Dir_Button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1;
            folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Receive_Path = folderBrowserDialog1.SelectedPath;
                Console.WriteLine(Receive_Path);
            }
        }

        private void Receive_Files_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Send_String_Messages(Socket_Ori, "Send File");
                //接收有幾個檔案
                int Files_Num = Get_Files_Num(Socket_Ori);

                //回覆訊息接收完畢
                Return_Finish_Bytes(Socket_Ori);

                //開始接收檔案
                Get_Multi_Files(Socket_Ori, Receive_Path, Files_Num);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //回傳檔案的按鈕
        private void Return_Files_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Send_String_Messages(Socket_Ori, "Receive File");

                Get_Finish_Byte(Socket_Ori);
                string[] Files_Name_With_Dir = Directory.GetFiles(Receive_Path);
                int Files_Num = Files_Name_With_Dir.Length;
                //傳送檔案數量
                Send_Files_Num(Socket_Ori, Files_Num);


                //接收傳送完成的訊息
                Get_Finish_Byte(Socket_Ori);

                //傳送檔案
                Send_Multi_Files(Socket_Ori, Files_Name_With_Dir, Receive_Path);
            }
            //如果有例外狀況發生則跳出並回覆例外訊息
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //按鍵判斷區//
    }
}
