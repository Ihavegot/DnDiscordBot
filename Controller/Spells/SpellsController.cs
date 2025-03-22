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
                        _ = SendMessage(message, SpellOutput(spell) ?? string.Empty);
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
        private string SpellOutput(SpellsDataModel spell)
        {   // Leave this ugly format for now, will be changed later
            return $@"{spell.Name}

Source: {spell.Source}
Level: {spell.Level} {(spell.Ritual?.Equals("Y") == true ? "(Ritual)" : "")}
School: {spell.School}
Casting Time: {spell.CastingTime}
Range: {spell.Range} {spell.Area}
Components: {(spell.Verbal?.Equals("Y") == true ? "V" : "")} {(spell.Somatic?.Equals("Y") == true ? "S" : "")} {(spell.Material?.Equals("Y") == true ? "M" : "")}
Duration: {(spell.Concentration?.Equals("Y") == true ? "(Concentration)" : "")} {spell.Duration}

{spell.Details}";
        }
    }
}