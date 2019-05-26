using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ex3.Models

namespace Ex3.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        public bool isIp(string param)
        {
            string[] data = param.split('.');
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

        [HttpGet]
        public ActionResult display(string param1, int param2)
        {
            if (isIp(param1))
            {
                InfoModel.Instance.ip = ip;
                InfoModel.Instance.port = port.ToString();
            }
            else
            {
                InfoModel.fileName = param1
                InfoModel.timesPerSec = param2
            }

            return View();
        }

        [HttpGet]
        public ActionResult display(string ip, int port, int timesPerSec)
        {
            InfoModel.Instance.ip = ip;
            InfoModel.Instance.port = port.ToString();
            InfoModel.Instance.time = timesPerSec;

            return View();
        }
    }
}