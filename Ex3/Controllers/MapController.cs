using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ex3.Models;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;

namespace Ex3.Controllers
{
    public class MapController : Controller
    {

        public ActionResult Index()
        {
            string x = Directory.GetCurrentDirectory();
            return View();
        }

        static string lonPath = "position/longitude-deg";
        static string latPath = "position/latitude-deg";
        static string rudderPath = "/controls/flight/rudder";
        static string throttlePath = "/controls/engines/current-engine/throttle";
        
        [HttpGet]
        public ActionResult display1(string ip, int port, int timesPerSec)
        {
            IPAddress address;
            if (IPAddress.TryParse(ip, out address))
            {
                Connection.Instance.Connect(port, ip);
                if (Connection.Instance.IsCon) {
                    Session["lat"] = Connection.Instance.GetPath(latPath);
                    Session["lon"] = Connection.Instance.GetPath(lonPath);
                }

                if (timesPerSec > 0) { Session["timesPerSec"] = timesPerSec; }
                else { Session["timesPerSec"] = 0; }
            }

            Session["timesPerSec"] = 1;
            return View();
        }

        [HttpGet]
        public ActionResult display2(string param1, int param2)
        {
            IPAddress address;
            if (IPAddress.TryParse(param1, out address))
            {
                Connection.Instance.Connect(param2, param1);
                if (Connection.Instance.IsCon)
                {
                    Session["lat"] = Connection.Instance.GetPath(latPath);
                    Session["lon"] = Connection.Instance.GetPath(lonPath);
                }
                Session["timesPerSec"] = 0;
                Session["stop"] = 1;
            }
            else
            {
                if (Connection.Instance.IsCon)
                {
                    Connection.Instance.Close();
                }
                Information info = new Information();
                InfoModel.Instance.FileName = param1;
                InfoModel.Instance.ReadDataXML();
                InfoModel.Instance.Index = -1;
                info = InfoModel.Instance.GetInformation();
                if (info == null)
                {
                    Session["stop"] = 1;
                    return View();
                }
                Session["lat"] = info.Lat;
                Session["lon"] = info.Lon;
                Session["timesPerSec"] = param2;
                Session["stop"] = 0;

            }
            return View();
        }

        [HttpGet]
        public ActionResult save(string ip, int port, int timesPerSec, int time, string fileName)
        {
            IPAddress address;
            if (IPAddress.TryParse(ip, out address))
            {
                Connection.Instance.Connect(port, ip);
                if (Connection.Instance.IsCon)
                {
                    Session["lat"] = Connection.Instance.GetPath(latPath);
                    Session["lon"] = Connection.Instance.GetPath(lonPath);
                }

                if ( timesPerSec > 0) { Session["timesPerSec"] = timesPerSec; }
                else { Session["timesPerSec"] = 0; }

                Session["time"] = time;
                InfoModel.Instance.FileName = fileName;
            }
            return View();
        }

        [HttpPost]
        public string GetInfo()
        {
            var info = new Information();
            if (Connection.Instance.IsCon)
            {
                info.Lat = double.Parse(Connection.Instance.GetPath(latPath));
                info.Lon = double.Parse(Connection.Instance.GetPath(lonPath));
                info.Rudder = double.Parse(Connection.Instance.GetPath(rudderPath));
                info.Throttle = double.Parse(Connection.Instance.GetPath(throttlePath));
                if (InfoModel.Instance.FileName != "")
                {
                    InfoModel.Instance.RecordInfo(info);
                }
                return InfoModel.Instance.ToXml(info);
            }
            else if (InfoModel.Instance.FileName != "")
            {
                info = InfoModel.Instance.GetInformation();
                if (info == null)
                {
                    Session["stop"] = 1;
                    return null;
                }
                return InfoModel.Instance.ToXml(info);
            }
            return null;
        }

        [HttpPost]
        public void RecordToFile()
        {
            InfoModel.Instance.RecordToFile();
        }
    }
}