using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ex3.Models;
using System.Xml;
using System.Text;

namespace Ex3.Controllers
{
    public class MapController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public bool isIp(string param)
        {
            string[] data = param.Split('.');
            if (data.Length != 4) { return false; }

             int parsedInt = 0;
            for (int index = 0; index < 4; ++index)
            {
                if (int.TryParse(data[index], out parsedInt))
                {
                    if (parsedInt < 0 || 255 < parsedInt) { return false; }
                }
                else { return false; }
            }

            return true;
        }

        static string lonPath = "position/longitude-deg";
        static string latPath = "position/latitude-deg";
        static string rudderPath = "/controls/flight/rudder";
        static string throttlePath = "/controls/engines/current-engine/throttle";

        [HttpGet]
        public ActionResult display(string param1, int param2, int? param3)
        {
            if (isIp(param1))
            {
                Connection.Instance.Connect(param2, param1);
                Session["lat"] = Connection.Instance.GetPath(latPath);
                Session["lon"] = Connection.Instance.GetPath(lonPath);

                if (param3 != null) { Session["timesPerSec"] = param3; }
                else { Session["timesPerSec"] = 0; }
            }
            else
            {
                /*Connection.Instance.Connect(param2, param1);
                ViewBag.lat = Connection.Instance.GetPath(lonPath);
                ViewBag.lon = Connection.Instance.GetPath(latPath);*/
                InfoModel.Instance.fileName = param1;
                Session["timesPerSec"] = param2;
            }

            return View();
        }

        [HttpPost]
        public void getInfo()
        {
            Session["lat"] = Connection.Instance.GetPath(latPath);
            Session["lon"] = Connection.Instance.GetPath(lonPath);

            return;
        }

        [HttpPost]
        public void getInfoAndWrite()
        {
            var info = new Information();
            info.Lat = int.Parse(Connection.Instance.GetPath(latPath));
            info.Lon = int.Parse(Connection.Instance.GetPath(lonPath));
            info.Rudder = int.Parse(Connection.Instance.GetPath(rudderPath));
            info.Throttle = int.Parse(Connection.Instance.GetPath(throttlePath));

            ToXml(info);

            return;
        }

        private void ToXml(Information information)
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
            return;
        }
    }
}