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
        private Dictionary<int, WebSocket> webSockets = new Dictionary<int, WebSocket>(); 


        
        public string Notify()
        {
            return "Hello!";
        }
    }
}