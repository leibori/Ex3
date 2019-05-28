using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Ex3.Models
{
    public class Information
    {
        public int Lat { get; set; }
        public int Lon { get; set; }
        public int Throttle { get; set; }
        public int Rudder { get; set; }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Information");
            writer.WriteElementString("Lat", this.Lat.ToString());
            writer.WriteElementString("Lon", this.Lon.ToString());
            writer.WriteElementString("Throttle", this.Throttle.ToString());
            writer.WriteElementString("Rudder", this.Rudder.ToString());
            writer.WriteEndElement();
        }
    }
}