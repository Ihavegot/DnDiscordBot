using System;
using System.Windows.Input;
using Discord;
using Discord.WebSocket;

namespace ConsoleApp.Controller.Commands
{
    public class D6Command : DnDiscordBot.Controller.ICommand
    {
        public async Task Execute(SocketMessage message)
        {
            if (message.Channel is ITextChannel textChannel)
            {
                await ClearMessagesAsync(textChannel);
            }
            await message.Channel.SendMessageAsync("D6: " + new Random().Next(1, 7));
        }
        private async Task ClearMessagesAsync(ITextChannel channel)
        {
            if (channel == null) return;

            var messages = await channel.GetMessagesAsync(100).FlattenAsync();
            await channel.DeleteMessagesAsync(messages);
        }
    }
}