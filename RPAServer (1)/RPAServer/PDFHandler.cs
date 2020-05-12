using IronPdf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
namespace CoreServer
{
    class PDFHandler
    {
        public string OpenPDF(string path)
        {
            PdfDocument document = null;

            document = PdfDocument.FromFile(path);
            
            GlobalObject global = GlobalObject.Instance;

            Guid id = Guid.NewGuid();
            
            Process process= Process.Start(path);

            global.fileIndex.Add(id.ToString(), document);
         
            
           
            Thread.Sleep(2000);
            process.Kill();

            return id.ToString();
        }

        public int ClosePDF(string id)
        {
            GlobalObject global = GlobalObject.Instance;

            PdfDocument document = null;

            Object tempObject;

            global.fileIndex.TryGetValue(id, out tempObject);

            if (tempObject is PdfDocument)
            {
                document = (PdfDocument)tempObject;
            }
            else
            {
                return Result.NOK;
            }

            document = null;

            global.fileIndex.Remove(id);

            return Result.OK;
        }

        public string ReadTextFromPage(string id, int page)
        {
            GlobalObject global = GlobalObject.Instance;

            PdfDocument document = null;

            Object tempObject;

            global.fileIndex.TryGetValue(id, out tempObject);

            if (tempObject is PdfDocument)
            {
                document = (PdfDocument)tempObject;
            }
            else
            {
                return "";
            }

            return document.ExtractTextFromPage(0);
        }

    }
}
