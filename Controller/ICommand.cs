using Discord;
using Discord.WebSocket;

namespace ConsoleApp.Controller
{
    public interface IDiceCommand
    {
        Task Execute(SocketMessage message);
    }
}