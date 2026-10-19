using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Vendor_Portal.OCR
{
    public class PdfReader
    {
        public static string ReadPdf(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            PdfReader reader = new PdfReader(filePath);

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                sb.Append(PdfTextExtractor.GetTextFromPage(reader, i));
            }

            reader.Close();

            return sb.ToString();
        }
    }
}