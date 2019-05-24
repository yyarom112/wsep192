using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebServices.Controllers
{
    public class WebsocketsController : ApiController
    {
        private static Dictionary<string, string> connections = new Dictionary<string, string>();

        public static Dictionary<string, string> Connections { get => connections; }



    }
}