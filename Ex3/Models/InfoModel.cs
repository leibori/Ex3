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
        private XmlDocument doc = new XmlDocument();
        private string fileName = "";
        private List<Information> recorded = new List<Information>();

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

        public string SCENARIO_FILE = "";           // The Path of the Secnario
        public Information Information { get; private set; }
        public string FileName {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                SCENARIO_FILE = string.Format("~/App_Data/{0}.xml", value);
            }
        }
        public string ip { get; set; }
        public int port { get; set; }
        public int time { get; set; }
        public int timesPerSec { get; set; }
        public string Path { get; set; }
       

        public InfoModel()
        {
            Information = new Information();
        }

        public void RecordInfo(Information info)
        {
            recorded.Add(info);
        }

        public void ReadDataXML(Information info)
        {
            string path = HttpContext.Current.Server.MapPath((SCENARIO_FILE));
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

        public void RecordToFile()
        {
            string path = HttpContext.Current.Server.MapPath((SCENARIO_FILE));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            XmlWriter writer = XmlWriter.Create(path, settings);

            foreach (Information info in recorded)
            {
                info.ToXml(writer);
            }

            writer.Flush();
            writer.Close();
        }
    }
}