using System.Globalization;
using CsvHelper;
using Discord.WebSocket;

namespace DnDiscordBot.Controller.Class
{
    public class ClassController : MessageController, IController
    {
        public ClassController()
        {
        }

        public void Execute(SocketMessage message)
        {
            try
            {
                // using (var reader = new StreamReader("5eData/5eClasses.csv")) // Linux
                using (var reader = new StreamReader("..\\..\\..\\5eData\\5eClasses.csv")) // Windows
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ClassDataModel>();
                    var characterClass = records.FirstOrDefault(r => !string.IsNullOrEmpty(r.Class) && r.Class.Contains(message.Content, StringComparison.OrdinalIgnoreCase));
                    System.Console.WriteLine(characterClass.Class + " " + characterClass.Subclass + " " + characterClass.HD + " " + characterClass.Caster + " " + characterClass.Prepared + " " + characterClass.LearnSpells + " " + characterClass.Healing + " " + characterClass.Ability + " " + characterClass.Saves + " " + characterClass.Armour + " " + characterClass.Weapons + " " + characterClass.Skills + " " + characterClass.Tools + " " + characterClass.Language + " " + characterClass.ExtraAttack + " " + characterClass.Superiority + " " + characterClass.Evasion + " " + characterClass.FightingStyle + " " + characterClass.Expertise + " " + characterClass.MoveOptions + " " + characterClass.Source + " " + characterClass.LevelToSubclass);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}