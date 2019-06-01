using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Xml;

namespace Ex3.Models
{
    public class InfoModel
    {
        private static InfoModel s_instace = null;

        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                }
                return s_instace;
            }
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.xml";           // The Path of the Secnario
        public Information Information { get; private set; }
        public string fileName { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public int time { get; set; }
        public int timesPerSec { get; set; }
        public string Path { get; set; }
       

        public InfoModel()
        {
            Information = new Information();
        }

        public void ReadDataXML(Information info)
        {
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            if (File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);        // reading all the lines of the file
                info.Lat = double.Parse(lines[0]);
                info.Lon = double.Parse(lines[1]);
                info.Rudder = double.Parse(lines[2]);
                info.Throttle = double.Parse(lines[3]);
            }
        }
        public string ToXml(Information information)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("AllInformation");

            information.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }
        public string createDateBaseFile(Information information)
        {
            //Initiate XML stuff
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(path, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("AllInformation");

            Information.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return path;
        }
    }
}