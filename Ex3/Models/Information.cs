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
        public int Hight { get; set; }
        public int Dircation { get; set; }
        public int Speed { get; set; }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Information");
            writer.WriteElementString("Lat", this.Lat.ToString());
            writer.WriteElementString("Lon", this.Lon.ToString());
            writer.WriteElementString("Hight", this.Hight.ToString());
            writer.WriteElementString("Dircation", this.Dircation.ToString());
            writer.WriteElementString("Speed", this.Speed.ToString());

        
            writer.WriteEndElement();
        }

    }
}