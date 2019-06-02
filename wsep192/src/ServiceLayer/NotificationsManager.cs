using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
namespace src.ServiceLayer
{
    internal class NotificationsManager
    {
        private static HubConnection hubConnection;
        private static IHubProxy hubProxy;
        private volatile bool started = false;

        public void init()
        {
            hubConnection = new HubConnection("http://localhost:53416/signalr");
            hubProxy = hubConnection.CreateHubProxy("ChatHub");
            hubConnection.Start().Wait();
            started = true;
        }

        public void notify(string userName, string message)
        {
            while (!started) ;
            hubProxy.Invoke("Send", userName, message);
        }

    }
}