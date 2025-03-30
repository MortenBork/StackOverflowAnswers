using iTextSharp.text.log;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using System.Net;

namespace StackoverflowAnswers
{
    public class PdfStreamsHandler
    {
        private readonly ILogger<PdfStreamsHandler> _logger;

        public PdfStreamsHandler(ILogger<PdfStreamsHandler> logger)
        {
            _logger = logger;
        }

        private readonly string url1 = "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf";
        private readonly string url2 = "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf";

        readonly List<string> psURLs = [
        "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf",
        "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf",
        "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf"
        ];

        public MemoryStream MergePdfs()
        {
            MemoryStream finalStream = new MemoryStream();
            PdfCopyFields ConcattedPdf = new PdfCopyFields(finalStream);

            foreach (string s in psURLs)
            {
                try
                {
                    var pdfMemoryStream = new MemoryStream();
                    ConvertToStream(s, pdfMemoryStream);
                    pdfMemoryStream.Position = 0;
                    ConcattedPdf.AddDocument(new PdfReader(pdfMemoryStream));
                    pdfMemoryStream.Dispose();
                }
                catch (Exception exc)
                {
                    _logger.LogInformation(exc.Message);
                    _logger.LogInformation(exc.InnerException?.Message);
                    _logger.LogInformation(exc.StackTrace);
                }
            }

            ConcattedPdf.Close();

            return finalStream;
        }

        private void ConvertToStream(string fileUrl, Stream stream)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fileUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            try
            {
                Stream response_stream = response.GetResponseStream();

                response_stream.CopyTo(stream, 4096);
            }
            finally
            {
                response.Close();
            }
        }
    }
}


