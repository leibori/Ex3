using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ex3.Models
{
    public class Connection
    {
        private TcpClient clientTcp;
        private NetworkStream stream;
        private StreamReader reader;

        public bool IsCon { set; get; } = false;

        #region SingleTon
        private static Connection m_Instance = null;
        public static Connection Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new Connection();
                return m_Instance;
            }
        }
        #endregion

        public void Connect(int port, string ip)
        {
            clientTcp = new TcpClient();
            while (!clientTcp.Connected)
            {
                try { clientTcp.Connect(IPAddress.Parse(ip), port); }
                catch (Exception) { }
            }
            IsCon = true;
            reader = new StreamReader(clientTcp.GetStream());
            Console.WriteLine("connacted");
        }

        public string GetPath(string path)
        {
            string msg = "get" + " " + path + "\r\n";
            byte[] massegeToSend = ASCIIEncoding.ASCII.GetBytes(msg);
            stream.Write(massegeToSend, 0, massegeToSend.Length);

            string SplitCommMess = reader.ReadLine().Split('\'')[1];
            return SplitCommMess;
        }

        public void Disconnect()
        {
            clientTcp.Close();
            stream.Close();
            IsCon = false;
        }
    }
}