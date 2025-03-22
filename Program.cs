using ConsoleApp.Controller;
using Discord;
using Discord.WebSocket;
using DnDiscordBot.Controller;
using DotNetEnv;

class Program
{
    private static string? _token;
    private static DiscordSocketClient? _client;
    private static CommandController? _commandController;

    static async Task Main()
    {
        Env.TraversePath().Load();
        _token = Env.GetString("DISCORD_BOT_TOKEN", "NO_TOKEN");

        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        };

        _client = new DiscordSocketClient(config);
        _client.Log += Log;
        _commandController = new CommandController(_client);
        _client.MessageReceived += MessageReceivedAsync;

        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();

        Console.WriteLine("Bot is running...");
        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private static Task MessageReceivedAsync(SocketMessage message)
    {
        _commandController?.ExecuteCommand(message);
        return Task.CompletedTask;
    }
}