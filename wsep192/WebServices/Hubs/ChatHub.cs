using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebServices
{
    public class ChatHub : Hub
    {

        Dictionary<string,string> connections = new Dictionary<string, string>();
        
        public void Login(string username, string connID)
        {
            connections.Add(username,connID);
                
        }

        public void Logout(string username)
        {
            connections.Remove(username);
        }



        public void Send(string name, string message)
        {
            

            Clients.All.addNewMessageToPage(name, message);
        }
    }
}