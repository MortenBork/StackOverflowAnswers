using iTextSharp.text.log;
using Microsoft.Extensions.Logging;
using Moq;
using StackoverflowAnswers;

namespace StackOverflowAnswers.Test;

public class StackOverflowAnswersTests
{
    [Fact]
    public void TestAnswer()
    {
        Mock<ILogger<PdfStreamsHandler>> mock = new Mock<ILogger<PdfStreamsHandler>>();
        PdfStreamsHandler pdfStreamHandler = new PdfStreamsHandler(mock.Object);
        MemoryStream actualResult = pdfStreamHandler.MergePdfs();
        Assert.NotNull(actualResult);
    }
}

