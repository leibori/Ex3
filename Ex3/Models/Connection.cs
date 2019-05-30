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
        TcpClient client;
        BinaryWriter writer;
        BinaryReader reader;
        private TcpListener listener;
        public bool isConnected = false;

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
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();

            // We try to connect again and again util the connection is made
            while (!client.Connected)
            {
                try { client.Connect(ep); }
                catch (Exception) { }
            }

            Console.WriteLine("connected");
            isConnected = true;
            writer = new BinaryWriter(client.GetStream());
            reader = new BinaryReader(client.GetStream());
        }

        public string GetPath(string command)
        {
            if (string.IsNullOrEmpty(command)) return "0";
            string buffer = command + "\r\n";
            writer.Write(Encoding.ASCII.GetBytes(buffer));

            char c;
            string line = "";
            while ((c = reader.ReadChar()) != '\n') line += c;
            return Parse(line);
        }

        public string Parse(string rawString)
        {
            string parsedString = "";
            string[] values = rawString.Split('\'');
            parsedString = values[1];
            return parsedString;
        }
    }
}

