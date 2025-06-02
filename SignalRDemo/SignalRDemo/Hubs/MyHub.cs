using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs
{
    public class MyHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}

// Bu sinif Hub sinifindan miras almak zorunda ki hub olarak kullanilabilsin. burada async bir metot olusturuyoruz. Bu metot client tarafina mesaj gonderiyor. ReceiveMessage parametresi ise client tarafinda tanimlanmis bir metot. Bu metot client tarafinda tanimlanmissa client tarafina mesaj gonderiyor. Burada user ve message parametreleri client tarafina gonderilecek olan veriler. Bu veriler client tarafinda kullanilabilir.