using System;
using System.Collections.Generic;
using ConsoleApp.Controller.Commands;
using Discord;
using Discord.WebSocket;

namespace ConsoleApp.Controller
{
    public class CommandController
    {
        private DiscordSocketClient _client;
        ICommand? _command;
        public CommandController(DiscordSocketClient _client)
        {
            this._client = _client;
        }

        public void ExecuteCommand(SocketMessage message)
        {
            if ((message.Author.Id == _client?.CurrentUser.Id) || message.Channel.Name != "dice")
                return;

            switch (CommandStartsWith(message))
            {
                case "d4":
                    _command = new D4Command();
                    _command.Execute(message);
                    break;
                case "d6":
                    _command = new D6Command();
                    _command.Execute(message);
                    break;
                case "d8":
                    _command = new D8Command();
                    _command.Execute(message);
                    break;
                case "d10":
                    _command = new D10Command();
                    _command.Execute(message);
                    break;
                case "d12":
                    _command = new D12Command();
                    _command.Execute(message);
                    break;
                case "d20":
                    _command = new D20Command();
                    _command.Execute(message);
                    break;
                case "d100":
                    _command = new D100Command();
                    _command.Execute(message);
                    break;
                default:
                    _command = new DiceClearCommand();
                    _command.Execute(message);
                    break;
            }
        }

        private string CommandStartsWith(SocketMessage message)
        {
            string[] commands = new string[] { "d4", "d6", "d8", "d100", "d12", "d20", "d10" };
            foreach (var command in commands)
            {
                if (message.Content.Contains(command))
                    return command;
            }
            return string.Empty;
        }
    }
}