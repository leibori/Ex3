using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace Ex3.Models
{
    public class Connection
    {
        private TcpClient client;
        private BinaryWriter writer;
        public bool isConnected = false;
        #region Singleton
        private static Connection _Instance = null;
        //singelton Command
        public static Connection Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Connection();
                }
                return _Instance;
            }
        }

        public bool Connected { get; internal set; }
        #endregion
        //connect to server
        public void ComConnect(string ip, int port)
        {
            client = new TcpClient();
            IPEndPoint ipEndPo = new IPEndPoint(IPAddress.Parse(ip), port);
            //keep logging
            while (!client.Connected)
            {
                try { client.Connect(ipEndPo); }
                catch (Exception exp)
                {
                    //exception
                    Console.WriteLine("pay attention to: {0}", exp.ToString());
                }
            }
            writer = new BinaryWriter(client.GetStream());
            Console.WriteLine("Connected");
            //update connect
            isConnected = true;
        }
        //send commands 
        public void ComSend(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            string[] commendsList = message.Split('\n');
            //go over commends
            foreach (string s in commendsList)
            {
                string indiCom = s + "\r\n";
                writer.Write(System.Text.Encoding.ASCII.GetBytes(indiCom));
                System.Threading.Thread.Sleep(2000);
            }
        }
        //clear instance
        public void ComClear()
        {
            _Instance = null;
        }
        //close 
        public void ComClose()
        {
            client.Close();
            writer.Close();
        }
    }
}