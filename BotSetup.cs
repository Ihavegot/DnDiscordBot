using Discord;
using Discord.WebSocket;
using DnDiscordBot.Controller;
using DotNetEnv;

namespace DnDiscordBot
{
    public class BotSetup
    {
        private string? _token;
        private DiscordSocketClient? _client;
        private CommandController? _commandController;

        public async Task Start()
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

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task MessageReceivedAsync(SocketMessage message)
        {
            _commandController?.ExecuteCommand(message);
            return Task.CompletedTask;
        }

    }
}