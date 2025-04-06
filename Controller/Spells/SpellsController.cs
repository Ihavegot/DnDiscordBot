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
                // using (var reader = new StreamReader("5eData/5eSpells.csv")) // Linux
                using (var reader = new StreamReader("..\\..\\..\\5eData\\5eSpells.csv")) // Windows
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
        {
            return $@"{spell.Name}{"\n"}{"\n"}Source: {spell.Source}{"\n"}Level: {spell.Level} {(spell.Ritual?.Equals("Y") == true ? "(Ritual)" : "")}{"\n"}School: {spell.School}{"\n"}Casting Time: {spell.CastingTime}{"\n"}Range: {spell.Range} {spell.Area}{"\n"}Components: {(spell.Verbal?.Equals("Y") == true ? "V" : "")} {(spell.Somatic?.Equals("Y") == true ? "S" : "")} {(spell.Material?.Equals("Y") == true ? "M" : "")}{"\n"}Duration: {(spell.Concentration?.Equals("Y") == true ? "(Concentration)" : "")} {spell.Duration}{"\n"}{"\n"}{spell.Details}";
        }
    }
}