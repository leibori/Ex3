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
        public Information Information { get; private set; }
        public string fileName { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public int time { get; set; }
        public int timesPerSec { get; set; }
       

        public InfoModel()
        {
            Information = new Information();
        }

       
        public const string SCENARIO_FILE = "~/App_Data/{0}.xml";           // The Path of the Secnario

        public void ReadDataXML(string name)
        {
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, name));
            if (!File.Exists(path))
            { }
            else
            {
                string[] lines = System.IO.File.ReadAllLines(path);        // reading all the lines of the file
                Information.Lat = double.Parse(lines[0]);
                Information.Lon = double.Parse(lines[1]);
                Information.Rudder = double.Parse(lines[2]);
                Information.Throttle = double.Parse(lines[3]);
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
      /*  public string dateBaseFile(Information information, HttpServerUtilityBase Server)
        {
            //Initiate XML stuff
            string path = Server.MapPath("~/App_Data/" + fileName + ".xml");
            if (System.IO.File.Exists(path)){
                
            }
            System.IO.File.WriteAllText(path, information.ToXml()
            

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }*/

    }
}