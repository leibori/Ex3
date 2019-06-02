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

        public string SCENARIO_FILE = "~/App_Data/{0}.xml";           // The Path of the Secnario
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
                if (!File.Exists(SCENARIO_FILE))
                {
                    XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlElement root = doc.DocumentElement;
                    doc.InsertBefore(xmlDeclaration, root);
                    XmlElement element = doc.CreateElement(string.Empty, "AllInformation", string.Empty);
                    doc.AppendChild(element);
                    doc.Save(SCENARIO_FILE);
                }
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

        public string createDateBaseFile(Information information)
        {
            //Initiate XML stuff
            //string path = HttpContext.Current.Server.MapPath((SCENARIO_FILE));
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("AllInformation");

            Information.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }
    }
}