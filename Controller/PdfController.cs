using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.AcroForms;
using Discord.WebSocket;
using Discord;

namespace ConsoleApp.Pdf
{
    public class PdfController
    {
        private string pdfPath = "C:\\Users\\Lukasz\\Desktop\\DnDiscordBot\\character.pdf";
        public PdfController(){}

        public async Task SendPdf(SocketMessage message)
        {
            if (message.Channel is ITextChannel textChannel)
            {
                await ClearMessagesAsync(textChannel);
            }
            await message.Channel.SendFileAsync(pdfPath, "Character Sheet");
        }

        public void UpdateFormField(string fieldName, string fieldValue)
        {
            PdfDocument document = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Modify);
            if (document.AcroForm != null)
            {
                PdfTextField? field = document.AcroForm.Fields[fieldName] as PdfTextField;
                if (field != null)
                {
                    field.Value = new PdfString(fieldValue);
                    document.Save(pdfPath);
                    Console.WriteLine($"Updated field '{fieldName}' with value '{fieldValue}'.");
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' not found.");
                }
            }
            else
            {
                Console.WriteLine("No AcroForm found in the PDF document.");
            }
        }

        private async Task ClearMessagesAsync(ITextChannel channel)
        {
            if (channel == null) return;

            var messages = await channel.GetMessagesAsync(100).FlattenAsync();
            await channel.DeleteMessagesAsync(messages);
        }
    }
}