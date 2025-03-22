using System;
using System.Globalization;
using CsvHelper;
using Discord;
using Discord.WebSocket;

namespace DnDiscordBot.Controller.Spells
{
    public class SpellsController : IController
    {
        public SpellsController() { }

        public void Execute(SocketMessage message)
        {
            try
            {
                if (message.Content.Contains("spells"))
                {
                    using (var reader = new StreamReader("C:\\Users\\Lukasz\\Desktop\\Git\\DnDiscordBot\\5eData\\5eSpells.csv"))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<SpellsDataModel>();
                        var spellName = message.Content.Replace("spells ", "");
                        var spell = records.FirstOrDefault(r => !string.IsNullOrEmpty(r.Name) && r.Name.Contains(spellName, StringComparison.OrdinalIgnoreCase));
                        
                        if (spell != null)
                        {
                            string output = $"{spell.Name} - {spell.Level} - {spell.School}";
                            _ = SendMessage(message, output ?? string.Empty);
                        }
                        else
                        {
                            _ = SendMessage(message, $"Spell '{spellName}' not found.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task SendMessage(SocketMessage message, string output)
        {
            if (message.Channel is ITextChannel textChannel)
            {
                await ClearMessagesAsync(textChannel);
            }
            await message.Channel.SendMessageAsync(output);
        }
        private async Task ClearMessagesAsync(ITextChannel channel)
        {
            if (channel == null) return;

            var messages = await channel.GetMessagesAsync(100).FlattenAsync();
            await channel.DeleteMessagesAsync(messages);
        }
    }
}