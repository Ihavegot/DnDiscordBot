using Discord.WebSocket;

namespace DnDiscordBot.Controller
{
    public interface ICommand
    {
        Task Execute(SocketMessage message);
    }
}