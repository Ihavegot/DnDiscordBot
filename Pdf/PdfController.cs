using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.AcroForms;

namespace ConsoleApp.Pdf
{
    public class PdfController
    {
        private string pdfPath = "C:\\Users\\Lukasz\\Desktop\\DnDiscordBot\\character.pdf";
        public PdfController()
        {
            PdfDocument document = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Modify);
            Console.WriteLine($"Loaded PDF file with {document.PageCount} pages.");
        }

        public void ReadPdf(string filePath)
        {
            // Method to read a PDF
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
    }
}