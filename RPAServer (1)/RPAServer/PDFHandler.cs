//using IronPdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using SautinSoft;

using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenQA.Selenium.Remote;

namespace CoreServer
{
    class PDFHandler
    {
        public string OpenPDF(string path)
        {
            string strText = string.Empty;
            try
            {
                PdfReader reader = new PdfReader("c:/log/Laser Punch & Bend_Long Quote.PDF");
                string res = "";
                for (int page = 1; page < reader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                     res = PdfTextExtractor.GetTextFromPage(reader, page, its);

                    res = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(res)));
                    Console.WriteLine(res);
                    strText += res;
                }

                reader.Close();
                string[] parameters = res.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                int start = 0;
                for (int i = 0; i < parameters.Length; i++)
                {
                    string mstring = parameters[i];

                    if (mstring.Contains("AMOUNT"))
                    {
                        start = i;
                        break;
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return " ";
            /* SautinSoft.PdfFocus f = new PdfFocus();
             f.OpenPdf(@"c:\log\Laser Punch & Bend_Long Quote.PDF");

             if (f.PageCount > 0)
                 f.ToExcel(@"c:\log\Invoice.xls");


             PdfReader reader = new PdfReader(@"c:\log\Laser Punch & Bend_Long Quote.PDF");
             int intPageNum = reader.NumberOfPages;
             intPageNum = 1;
             string[] words;
             string line;
             string res = "";
             for (int i = 1; i <= intPageNum; i++)
             {
                 res+= PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                 Console.WriteLine(res);
                 words = res.Split('\n');
                 for (int j = 0, len = words.Length; j < len; j++)
                 {
                     line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j]));
                 }
             }
             string[] parameters = res.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
             int start = 0;
             for (int i = 0; i < parameters.Length; i++)
             {
                 string mstring = parameters[i];

                 if (mstring.Contains("AMOUNT"))
                 {
                     start = i;
                     break;
                 }
             }
             string[] columns = parameters[start].Split(' ');

             int cols = 5;
             string[] curRes = Enumerable.Repeat(" ", cols).ToArray();
             ArrayList rows = new ArrayList();
             //  ArrayList<string> ls = new ArrayList<string>();
             for (int i = start + 1; i < parameters.Length; i++)
             {

                 if (parameters[i].ToLower().Contains("page"))
                 {
                     string mstring = parameters[i];

                     while (!mstring.Contains("AMOUNT"))
                     {
                         i++;
                         mstring = parameters[i];

                     }
                     continue;
                 }
                 curRes = Enumerable.Repeat(" ", cols).ToArray();
                 Console.WriteLine("i=" + i + "  " + parameters[i]);
                 string[] cur = new string[cols];
                 string[] temp =parameters[i].Split(' ');
                 int kstart = 0;
                 while (cur.Length < cols)
                 {
                   for(int k = kstart; k < temp.Length; k++)
                   {
                      cur[k] = temp[k];
                   }
                     kstart = temp.Length;
                     i++;
                     temp = parameters[i].Split(' ');
                 }


                 curRes[0] += temp[0];
                 for (int k = 1; k < temp.Length; k++)
                 {
                     curRes[1] += temp[k];


                 }
                 /*
                 i++;
                 cur = parameters[i].Split(' ');
                 curRes[0] += cur[trueZero];
                 for (int j = trueZero + 1; j < cur.Length; j++)
                 {
                     curRes[1] += cur[j];
                 }
                 i++;

                 rows.Add(curRes);
             }
             return res;
             */
        }

  
        /*
        public string OpenPDF(string path)
        {
            PdfDocument document = null;

            document = PdfDocument.FromFile(path);

            GlobalObject global = GlobalObject.Instance;

            Guid id = Guid.NewGuid();

            global.fileIndex.Add(id.ToString(), document);

            //System.Diagnostics.Process.Start(path);


            //Console.WriteLine("ID Added : " + id.ToString());

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
        //public string mergeDocuments(string path1, string path2)
        //{
        //    PdfDocument document1 = null;
        //    PdfDocument document2 = null;

        //    document1 = PdfDocument.FromFile(path1);
        //    document2 = PdfDocument.FromFile(path2);

        //    GlobalObject global = GlobalObject.Instance;

        //    Guid id = Guid.NewGuid();

        //    List<PdfDocument> pdfs = new List<PdfDocument>();

        //    pdfs.Add(document1);
        //    pdfs.Add(document2);

        //    PdfDocument mergedDoc = PdfDocument.Merge(pdfs);

        //    global.fileIndex.Add(id.ToString(), mergedDoc);

        //    return id.ToString();
        //}

        public string mergeDocuments(string id1, string id2)
        {

            GlobalObject global = GlobalObject.Instance;


            PdfDocument document1 = null;
            PdfDocument document2 = null;


            Object tempDoc1;
            Object tempDoc2;


            //Guid id = new Guid();



            global.fileIndex.TryGetValue(id1, out tempDoc1);
            global.fileIndex.TryGetValue(id2, out tempDoc2);


            if ((tempDoc1 is PdfDocument) && (tempDoc2 is PdfDocument))
            {
                document1 = (PdfDocument)tempDoc1;
                document2 = (PdfDocument)tempDoc2;
            }




            PdfDocument mergedDoc = PdfDocument.Merge(document1, document2);

            //global.fileIndex.Add(id.ToString(), mergedDoc);

            //return id.ToString();

            Console.WriteLine("Page Count on Document 1: " + document1.PageCount);
            Console.WriteLine("Page Count on Document 2: " + document2.PageCount);

            Console.WriteLine("Page Count on Document Resulting from the Merge: " + mergedDoc.PageCount);

            return "SUCCESS";
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

                string result = "";
                try
                {
                    result = document.ExtractTextFromPage(page - 1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Data);
                }
                byte[] a = document.Stream.ToArray();
                Console.WriteLine(result);
                return result;


        }
        */
    }
}
