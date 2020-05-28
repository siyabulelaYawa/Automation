using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;
using Tesseract;
using IronOcr.Languages;
using System.Collections;

namespace CoreServer
{
    class OCRHandler
    {
        public string getTextFromImage(string path)
        {
            AutoOcr Ocr = new AutoOcr();
            //  path = @"c:\log\Steelcut Services_Long Quote.PDF";
            path = @"c:\log\Steelcut Services_Long Quote.PDF";
            OcrResult Result = Ocr.Read(path);
            string res = Result.Text;
            //Console.WriteLine(Result.Text);
            string[] parameters = res.Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
            int start = 0;
            for(int i = 0; i < parameters.Length; i++)
            {
                string mstring = parameters[i];

                if (mstring.Contains("AMOUNT"))
                {
                    start = i;
                    break;
                }
            }
            string[] columns = parameters[start].Split(' ');
            
            int cols =6;
            string[] curRes = Enumerable.Repeat(" ", cols).ToArray();
            ArrayList rows = new ArrayList();
            //  ArrayList<string> ls = new ArrayList<string>();
            for (int i = start + 1; i < parameters.Length; i++)
            {
                try {
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
                    string[] cur = parameters[i].Split(' ');
                    if (cur.Length < (cols - 1))
                    {
                        continue;
                    }
                    //      if()
                    int trueZero = cur.Length - cols;

                    for (int j = cur.Length - 1; j >= trueZero; j--)
                    {
                        curRes[j - trueZero] += cur[j];
                    }
                    i++;

                    cur = parameters[i].Split(' ');
                    curRes[0] += " ";
                    curRes[1] += " ";
                    curRes[0] += cur[trueZero];
                    for (int j = trueZero + 1; j < cur.Length; j++)
                    {
                        curRes[1] += cur[j];
                    }
                    i++;
                    cur = parameters[i].Split(' ');
                    curRes[1] += " ";
                    for (int k = 0; k < cur.Length; k++)
                    {
                        curRes[1] += cur[k];
                    }
                    
                    rows.Add(curRes);
                }catch(Exception e){
                    Console.WriteLine("Error "+curRes);
                  }
                }
            ExcelHandler excelHandler = new ExcelHandler();
           string pid= excelHandler.OpenExcelDocument(@"c:\log\test.xlsx");
            for(int i = 0; i < rows.Count; i++)
            {
                excelHandler.writeExcelDocument(pid, "A" + i + 2, ((string[])rows[i])[0], "sheet1");
                excelHandler.writeExcelDocument(pid, "B" + i + 2, ((string[])rows[i])[1], "sheet1");
                excelHandler.writeExcelDocument(pid, "C" + i + 2, ((string[])rows[i])[2], "sheet1");
                excelHandler.writeExcelDocument(pid, "D" + i + 2, ((string[])rows[i])[3], "sheet1");
                excelHandler.writeExcelDocument(pid, "E" + i + 2, ((string[])rows[i])[4], "sheet1");
                excelHandler.writeExcelDocument(pid, "F" + i + 2, ((string[])rows[i])[5], "sheet1");
            }
            excelHandler.SaveExcelDocument(pid);
            return Result.Text;
        }


        public string readGermanText(string path)
        {
            AdvancedOcr ocr = new AdvancedOcr()
            {
                Language = IronOcr.Languages.German.OcrLanguagePack,
                ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                EnhanceResolution = true,
                EnhanceContrast = true,
                CleanBackgroundNoise = true,
                ColorDepth = 4,
                RotateAndStraighten = false,
                DetectWhiteTextOnDarkBackgrounds = false,
                ReadBarCodes = false,
                Strategy = AdvancedOcr.OcrStrategy.Fast,
                InputImageType = AdvancedOcr.InputTypes.Document
            };

            OcrResult results = ocr.Read(@path);
            Console.WriteLine(results.Text);

            return results.Text;
        }


    }
}
