using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CoreServer
{
    public class ExcelHandler
    {
        public string CreateExcelDocument(string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var fileInfo = new FileInfo(@fileName);

            var excelFile = new ExcelPackage(fileInfo);

            excelFile.Workbook.Worksheets.Add("Sheet1");

            GlobalObject global = GlobalObject.Instance;

            Guid id = Guid.NewGuid();

            global.fileIndex.Add(id.ToString(), excelFile);

            return id.ToString();
        }

        public int SaveExcelDocument(string filename)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo fileInfo = new FileInfo(@filename);

            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                //ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                //get worksheet by name
                //ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];



                //string valueA1 = worksheet.Cells["A1"].Value.ToString();
                //Console.WriteLine(valueA1);

                //excelPackage.Save();
                excelPackage.Save();
            }
               
                return 0;
                //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                //    GlobalObject global = GlobalObject.Instance;

                //    ExcelPackage excelFile;


                //    Object tempObject;

                //    global.fileIndex.TryGetValue(id, out tempObject);

                //    if (tempObject is ExcelPackage)
                //    {
                //        excelFile = (ExcelPackage)tempObject;
                //    }
                //    else
                //    {
                //        return Result.NOK;
                //    }

                //    excelFile.Save();

    
        }


        public int OpenExcelDocument(string filename)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo fileInfo = new FileInfo(@filename);

            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                //ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                //get worksheet by name
                //ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];



                //string valueA1 = worksheet.Cells["A1"].Value.ToString();
                //Console.WriteLine(valueA1);

                //excelPackage.Save();
                excelPackage.Save();
                Process process = Process.Start(filename);
                return 0;
            }

        }
        public int writeExcelDocument(string filename, string cell, string value)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo fileInfo = new FileInfo(@filename);

            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                //ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                //get worksheet by name
                //ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];



                //string valueA1 = worksheet.Cells["A1"].Value.ToString();
                //Console.WriteLine(valueA1);

                //excelPackage.Save();
                worksheet.Cells[cell].Value = value;
                excelPackage.Save();
                Process process = Process.Start(filename);
                return 0;
            }

        }
        public string ReadDataExcelDocument(string filename, string cell)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo fileInfo = new FileInfo(@filename);

            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                //ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                //get worksheet by name
                //ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet1"];



                //string valueA1 = worksheet.Cells["A1"].Value.ToString();
                //Console.WriteLine(valueA1);

                //excelPackage.Save();
                string value =worksheet.Cells[cell].Text;
                return value;
            }

        }


    }
}
