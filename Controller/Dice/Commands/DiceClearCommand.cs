using System;
using System.Windows.Input;
using Discord;
using Discord.WebSocket;

namespace ConsoleApp.Controller.Commands
{
    public class DiceClearCommand : DnDiscordBot.Controller.ICommand
    {
        public async Task Execute(SocketMessage message)
        {
            if (message.Channel is ITextChannel textChannel)
            {
                await ClearMessagesAsync(textChannel);
            }
        }
        private async Task ClearMessagesAsync(ITextChannel channel)
        {
            if (channel == null) return;

            var messages = await channel.GetMessagesAsync(100).FlattenAsync();
            await channel.DeleteMessagesAsync(messages);
        }
    }
}