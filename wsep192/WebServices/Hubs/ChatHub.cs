using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebServices.Controllers;

namespace WebServices
{
    public class ChatHub : Hub
    {

        public void Login(string username, string connID)
        {
                WebsocketsController.Connections.Add(username, connID);
        }

        public void Logout(string username)
        {
            WebsocketsController.Connections.Remove(username);
        }


        public void Send(string userName, string message)
        {
            Clients.Client(WebsocketsController.Connections[userName]).addNewMessageToPage(message);
        }

        public void Request(string userName, string message,string reqId)
        {
            Clients.Client(WebsocketsController.Connections[userName]).addNewRequestToPage(message,reqId);
        }


    }
}