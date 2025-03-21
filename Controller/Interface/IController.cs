using Discord.WebSocket;

namespace DnDiscordBot.Controller
{
    public interface IController
    {
        void Execute(SocketMessage message);
    }
}