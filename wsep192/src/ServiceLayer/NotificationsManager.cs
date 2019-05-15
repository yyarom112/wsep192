using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
namespace src.ServiceLayer
{
    internal class NotificationsManager
    {
        private static HubConnection hubConnection;
        private static IHubProxy hubProxy;


        public static void init()
        {
            hubConnection = new HubConnection("http://localhost:53416/signalr");
            hubProxy = hubConnection.CreateHubProxy("ChatHub");
            hubConnection.Start().Wait();
        }

        public static void notify(string userName,string message)
        {

            hubProxy.Invoke("Send",userName,message);
        }
    }
}