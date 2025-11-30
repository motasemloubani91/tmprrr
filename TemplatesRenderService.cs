

//09Nov2025
//09Nov2025
//using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net.Http;
using TemplatesRender.BL.Constants;
using TemplatesRender.BL.Enums;
using TemplatesRender.DAL;
using TemplatesRender.Dto;
using TemplatesRender.Models;
using IronPdf;
using System.Threading.Tasks;
using System.Linq;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using NLog;

namespace TemplatesRender.BL.Services
{
    public partial class TemplatesRenderService
    {
        //todo
        readonly IDataAccess _dataAccess;
        //private static NLog.Logger MainLogger = LogManager.GetLogger("KGACQRReports");
        public TemplatesRenderService()
        {
            _dataAccess = new FakeDataAccess();
            //  _dataAccess = new DataAccess();
        }

        public TemplateResultDto GetFormAsync(string id, string formName, OutputFormat outputFormat, Bitmap qrCodeImage)
        {

            String path = AppDomain.CurrentDomain.BaseDirectory;
            using (System.IO.StreamWriter outputFile = new
                System.IO.StreamWriter(path + "\\log.txt", true))
            {
                outputFile.WriteLine(DateTime.Now.ToString() + formName + " " + id + "=>" + "GetFormAsync Prepare HTML");
            }

            TemplateResultDto resultDto = new TemplateResultDto();
            switch (formName)
            {
                case "Declrationfrom":
                    {
                        resultDto = GetDeclrationForm(id, qrCodeImage);
                        break;
                    }
                case "SettlementTemp1":
                    {
                        resultDto = GetSettelmentFormTemp1(id, qrCodeImage);
                        break;
                    }

                case "SettlementTemp2":
                    {
                        resultDto = GetSettelmentFormTemp2(id, qrCodeImage);
                        break;
                    }
                case "PromissoryNoteForNonGvt":
                    {
                        resultDto = GetPromissoryNoteForNonGvtForm(id, qrCodeImage);
                        break;
                    }
                case "PromissoryNoteForGvt":
                    {
                        resultDto = GetPromissoryNoteForGvtForm(id, qrCodeImage);
                        break;
                    }
                case "ClearanceForm":
                    {
                        resultDto = GetClearanceForm(id, qrCodeImage);
                        break;
                    }
                case "AuthorizationForm":
                    {
                        resultDto = GetIndividualAuthorizationFormData(id, qrCodeImage);
                        break;
                    }
                case "ToWhomItConcernVehicleTransaction":
                    {
                        resultDto = GetVehicleTransactionCertificateData(id, qrCodeImage);
                        break;
                    }
                case "ToWhomItConcernVehicleNoTransaction":
                    {
                        resultDto = GetNoVehicleTransactionCertificateData(id, qrCodeImage);
                        break;
                    }
                default:
                    throw new Exception("not supported form");
            }

            using (System.IO.StreamWriter outputFile = new
                System.IO.StreamWriter(path + "\\log.txt", true))
            {
                outputFile.WriteLine(DateTime.Now.ToString() + "=>" + "GetFormAsync Prepare HTML End");
            }


            switch (outputFormat)
            {
                case OutputFormat.pdf:
                    {
                        using (System.IO.StreamWriter outputFile = new
                System.IO.StreamWriter(path + "\\log.txt", true))
                        {
                            outputFile.WriteLine(DateTime.Now.ToString() + "=>" + "Switch GeneratePDFAsync");
                        }
                        if (new List<string> {
                            "SettlementTemp1",
                            "SettlementTemp2",
                            "ToWhomItConcernVehicleTransaction" }.Any(a => a == formName))
                        {
                            resultDto.TemplatePdfFile = GeneratePDFAsync(resultDto.TemplateHtmls);
                        }
                        else
                        {
                            resultDto.TemplatePdfFile = GeneratePDFAsync(resultDto.TemplateHtml, resultDto.PagesNumber);
                        }
                        return resultDto;
                    }
                case OutputFormat.html:
                    {
                        return resultDto;
                    }
                default:
                    throw new Exception("not supported form");
            }

        }

        //private byte[] GeneratePDF(string htmlContent, int pagesNumber)
        //{
        //    string chromiumEnginePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Chromium-88.0.4324.0");
        //    var pdfConvert = new HtmlToPdf();
        //    pdfConvert.Options.PdfPageSize = PdfPageSize.A4;
        //    pdfConvert.Options.WebPageWidth = 794;
        //    pdfConvert.Options.MarginLeft = 2;
        //    pdfConvert.Options.RenderingEngine = RenderingEngine.Blink;
        //    pdfConvert.Options.BlinkEnginePath = chromiumEnginePath;
        //    PdfDocument pdfDocument = pdfConvert.ConvertHtmlString(htmlContent);
        //    if (pagesNumber == pdfDocument.Pages.Count)
        //        return pdfDocument.Save();
        //    for (var i = pagesNumber - 1; i < pagesNumber; i++)
        //    {
        //        pdfDocument.Pages.RemoveAt(i + 1);
        //    }
        //    return pdfDocument.Save();
        //}

        #region Commented Code
        //private  byte[] GeneratePDFAsync(string htmlContent, int pagesNumber)
        //{
        //    System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    string resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
        //    string bodyHtmlFile = Path.Combine(resourcesPath, "ddd.pdf");
        //    string bodyHtmlFile1 = Path.Combine(resourcesPath, "ddd1.pdf");
        //    string bodyHtmlFile2 = Path.Combine(resourcesPath, "ddd2.pdf");
        //    string bodyHtmlFile3 = Path.Combine(resourcesPath, "ddd3.pdf");
        //    string bodyHtmlFile4 = Path.Combine(resourcesPath, "ddd4.pdf");
        //    string bodyHtmlFile5 = Path.Combine(resourcesPath, "ddd5.pdf");
        //    string bodyHtmlFile6 = Path.Combine(resourcesPath, "ddd6.pdf");
        //    string bodyHtmlFile7 = Path.Combine(resourcesPath, "ddd7.pdf");
        //    string bodyHtmlFile8 = Path.Combine(resourcesPath, "ddd8.pdf");
        //    string bodyHtmlFile9 = Path.Combine(resourcesPath, "ddd9.pdf");
        //    string bodyHtmlFile10 = Path.Combine(resourcesPath, "ddd10.pdf");

        //    string file1 = @"C:\path\to\file1.pdf";
        //    string file2 = @"C:\path\to\file2.pdf";

        //    // Create a memory stream to hold the merged PDF
        //    MemoryStream ms = new MemoryStream();

        //    // Create a Document object
        //    Document document = new Document();

        //    // Create a PdfCopy object
        //    PdfCopy copy = new PdfCopy(document, ms);

        //    // Open the Document
        //    document.Open();

        //    // Add the first PDF file
        //    PdfReader reader1 = new PdfReader(bodyHtmlFile);
        //    copy.AddDocument(reader1);


        //    // Add the second PDF file
        //    PdfReader reader2 = new PdfReader(bodyHtmlFile1);
        //    copy.AddDocument(reader2);
        //    PdfReader reader3 = new PdfReader(bodyHtmlFile2);
        //    copy.AddDocument(reader3);
        //    PdfReader reader4 = new PdfReader(bodyHtmlFile3);
        //    copy.AddDocument(reader4);
        //    PdfReader reader5 = new PdfReader(bodyHtmlFile4);
        //    copy.AddDocument(reader5);
        //    PdfReader reader6 = new PdfReader(bodyHtmlFile5);
        //    copy.AddDocument(reader6);
        //    PdfReader reader7 = new PdfReader(bodyHtmlFile6);
        //    copy.AddDocument(reader7);
        //    PdfReader reader8 = new PdfReader(bodyHtmlFile7);
        //    copy.AddDocument(reader8);
        //    PdfReader reader9 = new PdfReader(bodyHtmlFile8);
        //    copy.AddDocument(reader9);
        //    PdfReader reader10 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader10);
        //    PdfReader reader11 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader11);
        //    PdfReader reader12 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader12);
        //    PdfReader reader13 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader13);

        //    PdfReader reader14 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader14);
        //    PdfReader reader15 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader15);
        //    PdfReader reader16 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader16);
        //    PdfReader reader17 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader17);
        //    PdfReader reader18 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader18);
        //    PdfReader reader19 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader19);
        //    PdfReader reader111 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader111);
        //    PdfReader reader20 = new PdfReader(bodyHtmlFile7);
        //    copy.AddDocument(reader20);
        //    PdfReader reader21 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader21);
        //    PdfReader reader22 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader22);
        //    PdfReader reader23 = new PdfReader(bodyHtmlFile9);
        //    copy.AddDocument(reader23);


        //    // Close the Document and PdfCopy objects
        //    document.Close();
        //    copy.Close();
        //    stopwatch.Stop();
        //    Debug.WriteLine("Elapsed time: {0}", stopwatch.Elapsed.TotalSeconds);
        //    stopwatch.Reset();
        //    // Convert the merged PDF to a byte array
        //    return ms.ToArray();

        //}
        #endregion  Commented Code

        private byte[] GeneratePDFAsync(List<string> htmlContent)
        {
            try
            {
                String path = AppDomain.CurrentDomain.BaseDirectory;

                string licenseKey = ConfigurationManager.AppSettings["IronPdf.LicenseKey"];
                IronPdf.License.LicenseKey = licenseKey;

                IronPdf.ChromePdfRenderer Renderer = new IronPdf.ChromePdfRenderer();

                Renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.A4;
                Renderer.RenderingOptions.Timeout = 30; //Seconds
                Renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Print;
                Renderer.RenderingOptions.MarginBottom = 0;
                Renderer.RenderingOptions.MarginRight = 0;
                Renderer.RenderingOptions.MarginLeft = 0;
                Renderer.RenderingOptions.MarginTop = 0;
                Renderer.RenderingOptions.MarginTop = 0; //it was 2
                Renderer.RenderingOptions.EnableJavaScript = false;
                Renderer.RenderingOptions.PaperOrientation = IronPdf.Rendering.PdfPaperOrientation.Portrait;
                List<IronPdf.PdfDocument> pdfPages = new List<PdfDocument>();
                IronPdf.PdfDocument pdf = null;
                IronPdf.PdfDocument finalReport = null;
                try
                {
                    for (int i = 0; i <= htmlContent.Count - 1; i++)
                    {
                        pdf = Renderer.RenderHtmlAsPdf(htmlContent[i]);
                        //pdf.SaveAs("D:\\Mohan\\" + i + ".pdf");
                        pdfPages.Add(pdf);
                    }
                    finalReport = PdfDocument.Merge(pdfPages);
                    //finalReport.SaveAs("D:\\Mohan\\pdfPages.pdf");
                    //foreach (var pdfPage in pdfPages)
                    //{
                    //    pdfPage.Dispose();
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }

                //CompressionOptions dd = new CompressionOptions();
                //pdf.Compress();
                using (System.IO.StreamWriter outputFile = new
                    System.IO.StreamWriter(path + "\\log.txt", true))
                {
                    outputFile.WriteLine(DateTime.Now.ToString() + "=>" + "GeneratePDFAsync End");
                }

                // Get the PDF document as a byte array
                return finalReport.BinaryData;
            }
            catch (Exception ex)
            {
                //String path = AppDomain.CurrentDomain.BaseDirectory;
                //using (System.IO.StreamWriter outputFile = new
                // System.IO.StreamWriter(path + "\\log.txt", true))
                //{
                //    outputFile.WriteLine(DateTime.Now.ToString() + "=>" + "GeneratePDFAsync Issue " + ex.InnerException.ToString());
                //}

                string sErrorMsg = "Error ocurred during PDF generate -  GeneratePDFAsync: \r\n" + ex.ToString().Replace("System.Exception: ", string.Empty);
                //MainLogger.Error(sErrorMsg);

                throw ex;
                //return null;
            }
        }

        private byte[] GeneratePDFAsync(string htmlContent, int pagesNumber)
        {
            try
            {
                String path = AppDomain.CurrentDomain.BaseDirectory;
                // String sfile = path + "\\" + Guid.NewGuid().ToString() + ".htm";

                /*using (System.IO.StreamWriter outputFile = new
                    System.IO.StreamWriter(path + "\\log.txt", true))
                 {
                     outputFile.WriteLine(DateTime.Now.ToString() + "=>" + "GeneratePDFAsync Start");
                 }

                   using (System.IO.StreamWriter outputFile = new
                       System.IO.StreamWriter(sfile, true))
                   {
                       outputFile.WriteLine(htmlContent);
                   }*/


                //Installation.ChromeBrowserLimit = 8;
                //Installation.SingleProcess = true;

                //Installation.LinuxAndDockerDependenciesAutoConfig = false;
                //Installation.ChromeGpuMode = IronPdf.Engines.Chrome.ChromeGpuModes.Disabled;
                //Installation.ChromeBrowserLimit = 15;
                //Installation.SingleProcess = true;
                //Installation.AutomaticallyDownloadNativeBinaries = false;

                string licenseKey = ConfigurationManager.AppSettings["IronPdf.LicenseKey"];
                IronPdf.License.LicenseKey = licenseKey;

                //IronPdf.Logging.Logger.EnableDebugging = true;
                //IronPdf.Logging.Logger.LogFilePath = "../logs/qrreporterr.txt";
                //IronPdf.Logging.Logger.LoggingMode = IronPdf.Logging.Logger.LoggingModes.All;

                IronPdf.ChromePdfRenderer Renderer = new IronPdf.ChromePdfRenderer();

                // Set the HTML to be rendered
                //string htmlString = "<html><body><h1>Hello, world!</h1></body></html>";

                // Render the HTML as a PDF document
                Renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.A4;
                Renderer.RenderingOptions.Timeout = 30; //Seconds
                Renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Print;
                Renderer.RenderingOptions.MarginBottom = 0;
                Renderer.RenderingOptions.MarginRight = 0;
                Renderer.RenderingOptions.MarginLeft = 0;
                Renderer.RenderingOptions.MarginTop = 0;
                Renderer.RenderingOptions.MarginTop = 2;
                Renderer.RenderingOptions.EnableJavaScript = false;
                Renderer.RenderingOptions.PaperOrientation = IronPdf.Rendering.PdfPaperOrientation.Portrait;
                IronPdf.PdfDocument pdf = null;
                //  File.WriteAllText("D:\\Mohan\\test_html.html", htmlContent);
                File.WriteAllText("D:\\test_html.html", htmlContent);
                try
                {
                    pdf = Renderer.RenderHtmlAsPdf(htmlContent);
                }
                catch (Exception ex)
                {
                    throw;
                }
                if (pdf.PageCount > pagesNumber)
                {
                    for (int i = pdf.PageCount - 1; i >= pagesNumber; i--)
                    {
                        pdf.RemovePage(i);
                    }
                }

                //CompressionOptions dd = new CompressionOptions();
                //pdf.Compress();
                using (System.IO.StreamWriter outputFile = new
                    System.IO.StreamWriter(path + "\\log.txt", true))
                {
                    outputFile.WriteLine(DateTime.Now.ToString() + "=>" + "GeneratePDFAsync End");
                }

                //    File.WriteAllText("D:\\Mohan\\test_html.html", htmlContent);
                //     File.WriteAllBytes("D:\\Mohan\\test_pdf.pdf", pdf.BinaryData);

                File.WriteAllText("D:\\test_html.html", htmlContent);
                File.WriteAllBytes("D:\\test_pdf.pdf", pdf.BinaryData);


                // Get the PDF document as a byte array
                return pdf.BinaryData;
            }
            catch (Exception ex)
            {
                //String path = AppDomain.CurrentDomain.BaseDirectory;
                //using (System.IO.StreamWriter outputFile = new
                // System.IO.StreamWriter(path + "\\log.txt", true))
                //{
                //    outputFile.WriteLine(DateTime.Now.ToString() + "=>" + "GeneratePDFAsync Issue " + ex.InnerException.ToString());
                //}

                string sErrorMsg = "Error ocurred during PDF generate -  GeneratePDFAsync: \r\n" + ex.ToString().Replace("System.Exception: ", string.Empty);
                //MainLogger.Error(sErrorMsg);

                throw ex;
                //return null;
            }
        }

        private async Task<IronPdf.PdfDocument> renderhtmtopdf(IronPdf.ChromePdfRenderer Renderer, String htmcontent)
        {
            IronPdf.PdfDocument tt = await Renderer.RenderHtmlAsPdfAsync(htmcontent);

            return tt;
        }

        //private byte[] GeneratePDFAsync(string htmlContent, int pagesNumber)
        //{
        // chrometopdf
        //    var converter = new ChromeHtmlToPdfLib.Converter();
        //    var pdfBytes = converter.ConvertHtmlToPdf(htmlContent);

        //    // Save the PDF to a file
        //    File.WriteAllBytes("output.pdf", pdfBytes);

        //    return pdfBytes;
        //}



        //private byte[] GeneratePDFAsync(string htmlContent, int pagesNumber)
        //{
        // ITEXTSHARP
        //    Document document = new Document(PageSize.A4);

        //    // Create a MemoryStream to store the PDF output
        //    MemoryStream outputStream = new MemoryStream();

        //    // Create a PdfWriter instance to write the PDF document to the output stream
        //    PdfWriter writer = PdfWriter.GetInstance(document, outputStream);

        //    // Open the document
        //    document.Open();

        //    // Parse the HTML content and generate PDF output
        //    using (var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
        //    {
        //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlStream, Encoding.UTF8);
        //    }

        //    // Close the document
        //    document.Close();

        //    // Return the PDF output as an array of bytes
        //    return outputStream.ToArray();
        //}



        //private  byte[] GeneratePDFAsync(string htmlContent, int pagesNumber)
        //{
        //    var browserFetcher = new BrowserFetcher();
        //    var downloadPath = browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision).Result;
        //    var options = new LaunchOptions { Headless = true };
        //    var browser = Puppeteer.LaunchAsync(options).Result;
        //    var page = browser.NewPageAsync().Result;
        //    page.EmulateMediaTypeAsync(MediaType.Print).Wait();
        //    page.SetContentAsync(htmlContent).Wait();
        //    var pdfStream = page.PdfStreamAsync(new PdfOptions
        //    {
        //        Format = PaperFormat.A4,
        //        PrintBackground = true
        //    }).Result;
        //    var memoryStream = new MemoryStream();
        //    pdfStream.CopyTo(memoryStream);
        //    var pdfData = memoryStream.ToArray();
        //    browser.Dispose();
        //    return pdfData;
        //}

        private string ConvertBitmapToBase64(Bitmap bitmapImage)
        {
            if (bitmapImage == null)
            {
                return "";
            }

            using (var ms = new MemoryStream())
            {
                var newImage = new Bitmap(bitmapImage);
                newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var SigBase64 = Convert.ToBase64String(ms.GetBuffer());
                return SigBase64;
            }
        }

        private string GetBarCode(string text)
        {
            if (_dataAccess is FakeDataAccess)
            {
                string x = string.Empty;
                Bitmap y;
                {
                    using (var ms = new MemoryStream(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\barCode.png"))))
                    {
                        y = new Bitmap(ms);
                    }
                }
                x = "data:image/png;base64," + ConvertBitmapToBase64(y);

                return x;
            }

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(string.Format(ConfigurationManager.AppSettings["QRCodeGeneratorURL"], text));
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["QRCodeGeneratorURL"] + text);
                //HTTP GET
                var responseTask = client.GetAsync("");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStreamAsync();

                    readTask.Wait();

                    var stream = readTask.Result;


                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        var bytes = memoryStream.ToArray();

                        string base64String = Convert.ToBase64String(bytes);
                        return base64String;
                    }
                }
                return string.Empty;
            }
        }

        private Bitmap ReadLocalImage(string key)
        {
            using (var ms = new MemoryStream(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Templates\\{key}"))))
            {
                return new Bitmap(ms);
            }
        }

        private string ReplaceHtmlData(Dictionary<string, string> dictionaryData, string formName)
        {
            string resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
            string bodyHtmlFile = Path.Combine(resourcesPath, $"{formName}.html");

            using (StreamReader reader = new StreamReader(bodyHtmlFile))
            {
                string htmlContent = reader.ReadToEnd();

                foreach (var pair in dictionaryData)
                {
                    htmlContent = htmlContent.Replace(pair.Key, pair.Value);
                }

                return htmlContent;
            }
        }
    }
}