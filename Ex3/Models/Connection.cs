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
        private TcpClient client;
        private NetworkStream netStream;
        private StreamReader netReader;

        public bool IsCon = false;

        #region Singleton
        private static Connection m_Instance = null;
        public static Connection Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Connection();
                }
                return m_Instance;
            }
        }
        #endregion

        public void Clear()
        {
            m_Instance = null;
        }

        public void Connect(int port, string ip)
        {
            client = new TcpClient();
            while (!client.Connected)
            {
                try { client.Connect(IPAddress.Parse(ip), port); }
                catch (Exception) { }
            }
            Console.WriteLine(" connacted");
            netStream = client.GetStream();
            IsCon = true;
            netReader = new StreamReader(netStream);
        }
        public void Close()
        {
            client.Close();
            netStream.Close();
            IsCon = false;
        }

        public string GetPath(string command)
        {
            string msg = "get" + " " + command + "\r\n";
            byte[] masse = ASCIIEncoding.ASCII.GetBytes(msg);
            int len = masse.Length;
            netStream.Write(masse, 0,len);

            string commnadLine = netReader.ReadLine().Split('\'')[1];
            return commnadLine;
        }

       
    }
}

