using ConsoleApp.Controller.Commands;
using Discord.WebSocket;

namespace DnDiscordBot.Controller
{
    public class DiceController : IController
    {
        private Dictionary<string, ICommand> _commands;
        public DiceController(){
            _commands = new Dictionary<string, ICommand>
            {
                { "d4", new D4Command() },
                { "d6", new D6Command() },
                { "d8", new D8Command() },
                { "d10", new D10Command() },
                { "d12", new D12Command() },
                { "d20", new D20Command() },
                { "d100", new D100Command() },
                { "clear", new DiceClearCommand() }
            };
        }

        public void Execute(SocketMessage message)
        {
            try{
                if(_commands.ContainsKey(message.Content.ToLower())){
                    _commands[message.Content.ToLower()].Execute(message);
                }else{
                    _commands["clear"].Execute(message);
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}