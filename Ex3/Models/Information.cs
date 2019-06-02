using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Ex3.Models
{
    public class Information
    {
       // private static int infoNum = 1;
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Throttle { get; set; }
        public double Rudder { get; set; }

        public void ToXml(XmlWriter writer)
        {
            string title = "Information";
            writer.WriteStartElement(title);
            writer.WriteElementString("Lat", this.Lat.ToString());
            writer.WriteElementString("Lon", this.Lon.ToString());
            writer.WriteElementString("Throttle", this.Throttle.ToString());
            writer.WriteElementString("Rudder", this.Rudder.ToString());
            writer.WriteEndElement();
        //    infoNum++;
        }

        public void ToCachedXml(XmlDocument doc)
        {
            string title = "Information";
            XmlElement mainElement = doc.CreateElement(string.Empty, title, string.Empty);
            doc.AppendChild(mainElement);

            XmlElement latElement = doc.CreateElement(string.Empty, "Lat", string.Empty);
            XmlText latInfo = doc.CreateTextNode(this.Lat.ToString());
            latElement.AppendChild(latInfo);
            mainElement.AppendChild(latElement);

            XmlElement lonElement = doc.CreateElement(string.Empty, "Lon", string.Empty);
            XmlText lonInfo = doc.CreateTextNode(this.Lon.ToString());
            lonElement.AppendChild(lonInfo);
            mainElement.AppendChild(lonElement);

            XmlElement throttleElement = doc.CreateElement(string.Empty, "Throttle", string.Empty);
            XmlText throttleInfo = doc.CreateTextNode(this.Throttle.ToString());
            throttleElement.AppendChild(throttleInfo);
            mainElement.AppendChild(throttleElement);

            XmlElement rudderElement = doc.CreateElement(string.Empty, "Rudder", string.Empty);
            XmlText rudderInfo = doc.CreateTextNode(this.Rudder.ToString());
            rudderElement.AppendChild(rudderInfo);
            mainElement.AppendChild(rudderElement);

           // ++infoNum;
        }
    }
}