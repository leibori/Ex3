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
        //connect to server

        public void Connect(int port, string ip)
        {
            client = new TcpClient();
            //keep trying logging
            while (!client.Connected)
            {
                try { client.Connect(IPAddress.Parse(ip), port); }
                //exception
                catch (Exception) { }
            }
            Console.WriteLine(" connacted");
            netStream = client.GetStream();
            //update connect status
            IsCon = true;
            netReader = new StreamReader(netStream);
        }
        //close client
        public void Close()
        {
            client.Close();
            netStream.Close();
            //update connect status
            IsCon = false;
        }
        //get message of the path from simulator
        public string GetPath(string command)
        {
            // full message with the command to simulator
            string msg = "get" + " " + command + "\r\n";
            byte[] masse = ASCIIEncoding.ASCII.GetBytes(msg);
            int len = masse.Length;
            netStream.Write(masse, 0,len);
            // get and return command line by split the '/' sign
            string commnadLine = netReader.ReadLine().Split('\'')[1];
            return commnadLine;
        }
    }
}

