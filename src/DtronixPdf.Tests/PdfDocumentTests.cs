using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DtronixPdf.Tests
{
    public class PdfDocumentTests
    {

        [SetUp]
        public void Setup()
        {
            //PDFiumCoreManager.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            //PDFiumCoreManager.Destroy();
        }


        [Test]
        public async Task LoadsDocument()
        {
            await using var document = await PdfDocument.Load("TestPdf.pdf", null);
            Assert.AreEqual(1, document.Pages);
        }
        
        [Test]
        public async Task LoadsPdfDocumentFromByteArray()
        {
            var data = await LoadFileContentAsync("TestPdf.pdf");
            
            await using var document = await PdfDocument.Load(data, null);
            Assert.AreEqual(1, document.Pages);
        }
        
        [Test]
        public async Task SavesDocument()
        {
            await using var document = await PdfDocument.Load("TestPdf.pdf", null);
            await using var sw = new MemoryStream();
            await document.Save(sw);

            Assert.Greater(sw.Length, 10000);
        }

        private async Task<byte[]> LoadFileContentAsync(string path)
        {
            await using var fs = File.Open(path, FileMode.Open);
            await using MemoryStream ms = new ();
            await fs.CopyToAsync(ms);
            return ms.ToArray();
        }

    }
}