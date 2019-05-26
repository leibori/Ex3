using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public string file { get; set; }
        public string ip { get; set; }
        public string port { get; set; }
        public int time { get; set; }

        public InfoModel()
        {
            Information = new Information();
        }
    }
}