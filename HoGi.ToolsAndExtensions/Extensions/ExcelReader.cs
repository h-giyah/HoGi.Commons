using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using HoGi.Shared.Exceptions;
using HoGi.ToolsAndExtensions.Interfaces;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace HoGi.ToolsAndExtensions.Extensions
{
    public class ExcelReader : IExcelReader
    {
        public IList<T> GetExcelData<T>(Stream stream, bool hasHeader = true)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using var excelPack = new ExcelPackage();
                excelPack.Load(stream);

                //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                var ws = excelPack.Workbook.Worksheets[0];

                //Get all details as DataTable -because Datatable make life easy :)
                var excelAsTable = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    //Get colummn details
                    if (!string.IsNullOrEmpty(firstRowCell.Text))
                    {
                        var firstColumn = $"Column {firstRowCell.Start.Column}";
                        excelAsTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                    }
                }
                var startRow = hasHeader ? 2 : 1;
                //Get row details
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, excelAsTable.Columns.Count];
                    var row = excelAsTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text.Replace(",","");
                    }
                }

                //Get everything as generics and let end user decides on casting to required type

                try
                {
                    var generatedType = JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(excelAsTable));
                    return generatedType;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw new GeneralException("در اکسل ورودی مشکلی وجود دارد");
                }
                //return (T)Convert.ChangeType(generatedType, typeof(T));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new GeneralException("در اکسل ورودی مشکلی وجود دارد");
            }
        }
    }
}
