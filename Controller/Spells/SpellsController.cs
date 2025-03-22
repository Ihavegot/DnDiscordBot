using System;
using System.Globalization;
using CsvHelper;
using Discord;
using Discord.WebSocket;

namespace DnDiscordBot.Controller.Spells
{
    public class SpellsController : MessageController, IController
    {
        public SpellsController() { }

        public void Execute(SocketMessage message)
        {
            try
            {
                using (var reader = new StreamReader("C:\\Users\\Lukasz\\Desktop\\Git\\DnDiscordBot\\5eData\\5eSpells.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<SpellsDataModel>();
                    var spell = records.FirstOrDefault(r => !string.IsNullOrEmpty(r.Name) && r.Name.Contains(message.Content, StringComparison.OrdinalIgnoreCase));

                    if (spell != null)
                    {
                        string output = $"{spell.Name} - {spell.Level} - {spell.School}";
                        _ = SendMessage(message, output ?? string.Empty);
                    }
                    else
                    {
                        _ = SendMessage(message, $"Spell '{message.Content}' not found.");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}