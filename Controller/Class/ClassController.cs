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
                    var characterClass = records.Where(
                        r => !string.IsNullOrEmpty(r.Class) && !string.IsNullOrEmpty(r.Subclass) &&
                        r.Class.Contains(message.Content.Split(" ")[0], StringComparison.OrdinalIgnoreCase)
                    );
                    var characterSubClass = characterClass.FirstOrDefault(
                        r => !string.IsNullOrEmpty(r.Subclass) &&
                        r.Subclass.Contains(message.Content.Split(" ")[1], StringComparison.OrdinalIgnoreCase)
                    );
                    if (characterSubClass != null)
                    {
                        _ = SendMessage(message, SpellOutput(characterSubClass) ?? string.Empty);
                    }
                    else
                    {
                        _ = SendMessage(message, $"Class '{message.Content}' not found.");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private string SpellOutput(ClassDataModel characterClass)
        {
            return string.Join("\n", characterClass.GetType().GetProperties()
                .Select(prop => $"{prop.Name}: {prop.GetValue(characterClass)}"));
        }
    }
}