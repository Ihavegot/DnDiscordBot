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

                    if (characterClass == null)
                    {
                        _ = SendMessage(message, $"Class '{message.Content}' not found.");
                        return;
                    }
                    else
                    {
                        var classMainInfo = characterClass.First(
                            r => !string.IsNullOrEmpty(r.Subclass) &&
                            r.Subclass.Contains("(choose one)", StringComparison.OrdinalIgnoreCase)
                        );
                        var characterSubClass = characterClass.FirstOrDefault(
                            r => !string.IsNullOrEmpty(r.Subclass) &&
                            r.Subclass.Contains(message.Content.Split(" ", 2)[1], StringComparison.OrdinalIgnoreCase)
                        );
                        if (characterSubClass == null)
                        {
                            _ = SendMessage(message, $"Subclass '{message.Content}' not found.");
                        }
                        else
                        {
                            _ = SendMessage(message, SpellOutput(CombineData(classMainInfo, characterSubClass)) ?? string.Empty);
                        }
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // TODO: Fix this bonobo code
        private ClassDataModel CombineData(ClassDataModel classMainInfo, ClassDataModel characterSubClass)
        {
            foreach(var classValue in characterSubClass.GetType().GetProperties()){
                if(classValue == null){
                    characterSubClass.GetType().GetProperty(classValue.Name).SetValue(this, classMainInfo.GetType().GetProperty(classValue.Name).GetValue(classMainInfo));
                }
            }
            return characterSubClass;
        }

        private string SpellOutput(ClassDataModel characterClass)
        {
            return string.Join("\n", characterClass.GetType().GetProperties()
                .Select(prop => $"{prop.Name}: {prop.GetValue(characterClass)}"));
        }
    }
}