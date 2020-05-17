using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Xml;

namespace SocketPraticeServer1
{
    class Program
    {
        static void Main(string[] args)
        {
                int Port = 6000;
                IPAddress IP = IPAddress.Any;
                IPEndPoint IPE = new IPEndPoint(IP, Port);
            while (true)
            {
                Socket Socket_Ori = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                Socket_Ori.Bind(IPE);
                Socket_Ori.Listen(100);
                Socket Socket_Server = Socket_Ori.Accept(); //在此等待Connect後才繼續執行;

                string Input_Account = "";
                string Input_Password = "";

                bool Input_Account_Bool = false;
                bool Input_Password_Bool = false;

                //位置設定
                string NowDir = Directory.GetCurrentDirectory();
                string DataDir = NowDir + @"\DATA";
                string[] Files_Name_With_Dir = Directory.GetFiles(DataDir);

                string Account_Folder;

                while (true)
                {
                    //接收訊息
                    string Receive_First_Message = "";

                    Receive_First_Message = Get_Byte_And_To_Str(Socket_Server);

                    //如果帳號還未輸入 則得到的Message設為帳號
                    if (Input_Account_Bool == false)
                    {
                        Input_Account = Receive_First_Message;
                        Input_Account_Bool = true;
                    }
                    //如果帳號已輸入 則得到的Message設為密碼
                    else if (Input_Account_Bool == true &&
                            Input_Password_Bool == false)
                    {
                        Input_Password = Receive_First_Message;
                        Input_Password_Bool = true;
                    }

                    //如果帳號密碼都已輸入
                    if (Input_Account_Bool == true &&
                       Input_Password_Bool == true)
                    {
                        try
                        {
                            XmlDocument xml = new XmlDocument();

                            xml.Load(DataDir + @"\" + Input_Account + ".xml");

                            XmlNode Staff = xml.SelectSingleNode("Staff");
                            string Account = Staff.FirstChild.Name.ToString();
                            XmlNode Information = xml.SelectSingleNode("Staff/" + Account);
                            string Correct_Account = Information.Attributes[0].Value;
                            string Correct_Password = Information.Attributes[1].Value;
                            string Position = Information.Attributes[2].Value;

                            //如果帳號密碼都正確 則傳送正確訊息
                            if (Input_Account == Correct_Account &&
                                Input_Password == Correct_Password)
                            {
                                //判斷有無此資料夾 若無則建立
                                Account_Folder = @Input_Account;
                                if (Directory.Exists(Account_Folder))
                                {
                                    Console.WriteLine("The directory {0} already exists.", Account_Folder);
                                }
                                else
                                {
                                    Directory.CreateDirectory(Account_Folder);
                                }

                                Console.WriteLine("All Correct");
                                string Sends_Message_Str = "Correct";
                                Send_Str_Message(Socket_Server, Sends_Message_Str);
                                
                                break;
                            }
                            //如果帳號或密碼不正確 則重新輸入
                            else
                            {
                                Console.WriteLine("Account or Password error");
                                string Sends_Message_Str = "Error";
                                Send_Str_Message(Socket_Server, Sends_Message_Str);

                                Input_Account_Bool = false;
                                Input_Password_Bool = false;
                            }
                        }
                        //如果無此帳號的檔案 則重新輸入
                        catch
                        {
                            Console.WriteLine("Account or Password error");
                            string Sends_Message_Str = "Error";
                            Send_Str_Message(Socket_Server, Sends_Message_Str);

                            Input_Account_Bool = false;
                            Input_Password_Bool = false;
                        }
                    }
                }
                //登入完成


                while (true)
                {
                    string Control_String = Receive_And_To_String(Socket_Server);
                    if (Control_String == "Finish")
                    {
                        break;
                    }
                    else if (Control_String == "Send File")
                    {
                        //傳送檔案
                        Send_Multi_Files(Socket_Server, Account_Folder);
                    }
                    else if (Control_String == "Receive File")
                    {
                        //回覆訊息接收完畢
                        Return_Finish_Bytes(Socket_Server);

                        //開始接收檔案
                        Get_Multi_Files(Socket_Server, Account_Folder);
                    }
                    else
                    {

                    }
                }
                Socket_Ori.Close();
            }
        }
        private static void Send_Str_Message(Socket Soc,string str)
        {
            byte[] Sends_Message_Byte = Encoding.UTF8.GetBytes(str);
            Soc.Send(Sends_Message_Byte);
        }
        //等待另一個Socket，接收訊息並轉為string回傳
        private static string Get_Byte_And_To_Str(Socket Soc)
        {
            byte[] Receive_Message_Byte = new byte[1024 * 50000];

            //在此等待接收後才繼續執行
            int Receive_Message_Byte_Length = Soc.Receive(Receive_Message_Byte);

            string Receive_Message = Encoding.UTF8.GetString(Receive_Message_Byte, 0, Receive_Message_Byte_Length);

            if(Receive_Message == "Finish")
            {
                System.Environment.Exit(System.Environment.ExitCode);
            }
            return (Receive_Message);
        }


        //將"路徑/檔名" 切為檔名並回傳檔名
        static string Get_Files_Name(string Dir, string Dir_With_Name)
        {
            string Files_Name = Dir_With_Name.Substring(Dir.Length + 1);
            return (Files_Name);
        }

        //接收訊息並轉為 string 回傳
        static string Receive_And_To_String(Socket Soc)
        {
            byte[] Receive_Message_Byte = new byte[1024 * 50000];

            //在此等待接收後才繼續執行
            int Receive_Message_Byte_Length = Soc.Receive(Receive_Message_Byte);

            string Message = Encoding.UTF8.GetString(Receive_Message_Byte, 0, Receive_Message_Byte_Length);

            return (Message);
        }
        //接收有多少個檔案

        //等待並接收完成訊息，以確認完成
        static void Get_Finish_Byte(Socket Soc)
        {
            byte[] Get_Finish_Byte = new byte[1024 * 50000];
            Soc.Receive(Get_Finish_Byte);
            Console.WriteLine("Get Finish Message");
        }

        //傳送完成訊息已雙方確認完成
        static void Return_Finish_Bytes(Socket Soc)
        {
            //回覆訊息接收完畢
            byte[] Return_Finish_Byte = Encoding.UTF8.GetBytes("Finish");
            Soc.Send(Return_Finish_Byte);
        }

        static void Get_Multi_Files(Socket Soc, string Receive_Path)
        {
            //接收有幾個檔案
            int Files_Num = Get_Files_Num(Soc);

            //回覆訊息接收完畢
            Return_Finish_Bytes(Soc);

            for (int i = 0; i < Files_Num; i++)
            {
                byte[] Get_Data = new byte[1024 * 50000];
                int Received_Bytes_Len = Soc.Receive(Get_Data);
                int File_Name_Len = BitConverter.ToInt32(Get_Data, 0);
                string File_Name = Encoding.ASCII.GetString(Get_Data, 4, File_Name_Len);
                BinaryWriter bWrite = new BinaryWriter(File.Open(Receive_Path + @"\" + File_Name, FileMode.Create));
                bWrite.Write(Get_Data, 4 + File_Name_Len, Received_Bytes_Len - 4 - File_Name_Len);
                bWrite.Close();
                //回覆該檔案接收完畢
                Return_Finish_Bytes(Soc);
            }
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

        static void Send_Multi_Files(Socket Soc, string Data_Dir)
        {
            //傳送檔案數量
            string[] Files_Name_With_Dir = Directory.GetFiles(Data_Dir);
            int Files_Num = Files_Name_With_Dir.Length;
            Send_Files_Num(Soc, Files_Num);


            //接收傳送完成的訊息
            Get_Finish_Byte(Soc);

            for (int i = 0; i < Files_Num; i++)
            {
                string File_Name = Get_Files_Name(Data_Dir, Files_Name_With_Dir[i]);

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

        static void Send_Files_Num(Socket Soc, int Files_Num)
        {
            string Files_Num_Str = Files_Num.ToString();
            byte[] Sends_Files_Num_Byte = Encoding.UTF8.GetBytes(Files_Num_Str);
            Soc.Send(Sends_Files_Num_Byte);
        }
    }
}
