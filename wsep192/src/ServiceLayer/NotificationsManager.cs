using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
namespace src.ServiceLayer
{
    internal class NotificationsManager
    {
        private static HubConnection hubConnection;
        private static IHubProxy hubProxy;


        public static async void initAsync()
        {
            hubConnection = new HubConnection("http://localhost:53416/signalr");
            hubProxy = hubConnection.CreateHubProxy("ChatHub");
            await hubConnection.Start();
        }

        public static void notify(string userName)
        {

            hubProxy.Invoke("Send",);
        }
    }
}