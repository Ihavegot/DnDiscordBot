using ConsoleApp.Controller;
using Discord;
using Discord.WebSocket;
using DnDiscordBot;
using DnDiscordBot.Controller;
using DotNetEnv;

class Program
{
    static async Task Main()
    {
        BotSetup bot = new BotSetup();
        await bot.Start();
    }
}