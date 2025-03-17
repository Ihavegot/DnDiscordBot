using Discord.WebSocket;

namespace ConsoleApp.Controller
{
    public interface ICommand
    {
        Task Execute(SocketMessage message);
    }
}