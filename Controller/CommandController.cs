using System;
using System.Windows.Input;
using ConsoleApp.Controller;
using Discord.WebSocket;
using DnDiscordBot.Controller.Class;
using DnDiscordBot.Controller.Spells;

namespace DnDiscordBot.Controller
{
    public class CommandController
    {
        private DiscordSocketClient _client;
        private Dictionary<string, IController> _commands;
        public CommandController(DiscordSocketClient _client)
        {
            this._client = _client;
            _commands = new Dictionary<string, IController>
            {
                { "dice", new DiceController() },
                { "spells", new SpellsController() },
                { "class", new ClassController() }
            };
        }

        public void ExecuteCommand(SocketMessage message)
        {
            if (message.Author.Id != _client?.CurrentUser.Id){ 
                try{
                    _commands[message.Channel.Name].Execute(message);
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                }
            }
            
        }
    }
}
