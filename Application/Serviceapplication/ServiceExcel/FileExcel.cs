using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;

namespace Serviceapplication.ServiceExcel
{
    public class FileExcel
    {
           public static List<string> ReadExcelXlsx(IFormFile file)
            {
                //Data
                DataTable dt = new DataTable();

            var xls = new XLWorkbook((file.OpenReadStream()));
                var planilha = xls.Worksheets.First(w => w.Name == "Planilha1");
                var totalLinhas = planilha.Rows().Count();

                //Get Columns
                List<String> columns = new List<string>();
                var qtdColumns = planilha.Columns().Count();
                for (int i = 1; i < qtdColumns; i++)
                {
                    var col = planilha.Column(i).FirstCell().Value.ToString();
                    dt.Columns.Add(col);
                    columns.Add(col);
                }

                return columns;


            }
        }
    }

