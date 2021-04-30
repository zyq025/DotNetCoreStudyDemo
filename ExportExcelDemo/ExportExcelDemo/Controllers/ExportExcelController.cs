using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExportExcelDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportExcelController : ControllerBase
    {

        [HttpGet("FillDataExcel")]
        public IActionResult FillDataExcel()
        {
            using (var package = new ExcelPackage())
            {
                // 需要添加一个Sheet，名称为"填充数据Demo"
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("填充数据Demo");
                // 不需要创建行和列就可以定位单元格赋值
                worksheet.Cells[1, 1].Value = "Data1"; // 给第一行第一列单元格赋值为Data1
                worksheet.Cells[1, 2].Value = 666;// 给第一行第二列单元格赋值为666
                worksheet.Cells[1, 3].Value = "Data3";// 给第一行第三列单元格赋值为Data3
                worksheet.Cells[1, 4].Value = "Data4";// 给第一行第四列单元格赋值为Data4
                worksheet.Cells[1, 5].Value = 0.25;// 给第一行第五列单元格赋值为0.25

                worksheet.Cells["A2"].Value = "AData1"; // 给第二行第一列单元格赋值为AData1
                worksheet.Cells["B2"].Value = 888;// 给第二行第二列单元格赋值为888
                worksheet.Cells["C2"].Value = "AData3";// 给第二行第三列单元格赋值为AData3
                worksheet.Cells["D2"].Value = "AData4";// 给第二行第四列单元格赋值为AData4
                worksheet.Cells["E2"].Value = 0.25;// 给第二行第五列单元格赋值为0.25
                // 直接通过内存导出，不用先保存文件了
                var excelFileStream = new MemoryStream(package.GetAsByteArray());

                return new FileStreamResult(excelFileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }


        [HttpGet("FormulaDataExcel")]
        public IActionResult FormulaDataExcel()
        {
            using (var package = new ExcelPackage())
            {
                // 需要添加一个Sheet，名称为"填充数据Demo"
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("公式Demo");
                // 不需要创建行和列就可以定位单元格赋值
                worksheet.Cells[1, 1].Value = 111; 
                worksheet.Cells[1, 2].Value = 666;
                worksheet.Cells[1, 3].Value = 2.6;
                worksheet.Cells[1, 4].Value = 33;
                worksheet.Cells[1, 5].Value = 0.25;

                worksheet.Cells["A2"].Value = 35; 
                worksheet.Cells["B2"].Value = 888;
                worksheet.Cells["C2"].Value = 6;
                worksheet.Cells["D2"].Value = 1;
                worksheet.Cells["E2"].Value = 0.25;

                //在第对应行中第六列单元格中 显示前五个单元格的和， 使用ExcelAddress定位计算的范围，第一行前五个单元求和
                worksheet.Cells[1, 6].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(1, 1,1, 5).Address);
                // 第二行前五个单元求和 ，并赋值给指定单元格(第二行的第六个单元格)
                worksheet.Cells[2, 6].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 1, 2, 5).Address);

                // 定某个单元格范围的值使用公式， 这里指定第三行的值等于第一行乘以第二行
                worksheet.Cells["A3:E3"].Formula = "A1*A2";

                // 直接通过内存导出，不用先保存文件了
                var excelFileStream = new MemoryStream(package.GetAsByteArray());

                return new FileStreamResult(excelFileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        [HttpGet("StyleDataExcel")]
        public IActionResult StyleDataExcel()
        {
            using (var package = new ExcelPackage())
            {
                // 需要添加一个Sheet，名称为"填充数据Demo"
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("样式Demo");
                // 设置所有单元格的内容水平和垂直对齐
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center; 
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                // 设置所有单元格边框格式
                worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // 不需要创建行和列就可以定位单元格赋值
                worksheet.Cells[1, 1].Value = "表头1";
                worksheet.Cells[1, 2].Value = "表头2";
                worksheet.Cells[1, 3].Value = "表头3";
                worksheet.Cells[1, 4].Value = "表头4";
                worksheet.Cells[1, 5].Value = "表头5";
                using (var range = worksheet.Cells[1, 1, 1, 5]) //获取一个区域，这里指的是第一行的1~5列区域
                {
                    // 设置字体
                    range.Style.Font.Bold = true;
                    // 背景色填充
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(1, 51, 204, 204);
                }

                //worksheet.Cells[1, 1].Style.Font.Bold = true;
                //worksheet.Cells[1, 2].Style.Font.Bold = true;
                //worksheet.Cells[1, 3].Style.Font.Bold = true;
                //worksheet.Cells[1, 4].Style.Font.Bold = true;
                //worksheet.Cells[1, 5].Style.Font.Bold = true;

                worksheet.Cells[2, 1].Value = 111;
                worksheet.Cells[2, 2].Value = 666;
                worksheet.Cells[2, 3].Value = 2.6;
                worksheet.Cells[2, 4].Value = 33;
                worksheet.Cells[2, 5].Value = 0.25;

                worksheet.Cells["A3"].Value = 35;
                worksheet.Cells["B3"].Value = 888;
                worksheet.Cells["C3"].Value = 6;
                worksheet.Cells["D3"].Value = 1;
                worksheet.Cells["E3"].Value = 0.25;

                
                // 直接通过内存导出，不用先保存文件了
                var excelFileStream = new MemoryStream(package.GetAsByteArray());

                return new FileStreamResult(excelFileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        [HttpGet("MergeDataExcel")]
        public IActionResult MergeDataExcel()
        {
            using (var package = new ExcelPackage())
            {
                // 需要添加一个Sheet，名称为"填充数据Demo"
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("合并Demo");
                // 设置所有单元格的内容水平和垂直对齐
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                // 设置所有单元格边框格式
                worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // 不需要创建行和列就可以定位单元格赋值
                worksheet.Cells[1, 1].Value = "表头1";
                worksheet.Cells[1, 3].Value = "表头3";
                worksheet.Cells[1, 4].Value = "表头4";
                worksheet.Cells[1, 5].Value = "表头5";
                MergeCell(worksheet, 1, 1, 1, 2);// 将第一行中的前两个单元进行合并
                using (var range = worksheet.Cells[1, 1, 1, 5]) //获取一个区域，这里指的是第一行的1~5列区域
                {
                    // 设置字体
                    range.Style.Font.Bold = true;
                    // 背景色填充
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(1, 51, 204, 204);
                }

                worksheet.Cells[2, 1].Value = 111;
                worksheet.Cells[2, 2].Value = 666;
                worksheet.Cells[2, 3].Value = 2.6;
                worksheet.Cells[2, 4].Value = 33;
                worksheet.Cells[2, 5].Value = 0.25;

                worksheet.Cells["A3"].Value = 111;
                worksheet.Cells["B3"].Value = 888;
                worksheet.Cells["C3"].Value = 6;
                worksheet.Cells["D3"].Value = 1;
                worksheet.Cells["E3"].Value = 0.25;

                worksheet.Cells["A4"].Value = 112;
                worksheet.Cells["B4"].Value = 8881;
                worksheet.Cells["C4"].Value = 612;
                worksheet.Cells["D4"].Value = 11;
                worksheet.Cells["E4"].Value = 0.251;

                MergeCell(worksheet, 1, 2);//合并第一列值相等的单元格，从第二行开始遍历
                // 直接通过内存导出，不用先保存文件了
                var excelFileStream = new MemoryStream(package.GetAsByteArray());

                return new FileStreamResult(excelFileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        [HttpGet("FitDataExcel")]
        public IActionResult FitDataExcel()
        {
            using (var package = new ExcelPackage())
            {
                // 需要添加一个Sheet，名称为"填充数据Demo"
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("合并Demo");
                
                // 不需要创建行和列就可以定位单元格赋值
                worksheet.Cells[2, 3].Value = "表头3测试好长一段内容";
                worksheet.Cells[3, 4].Value = "表头4测试好长一段内容";
                worksheet.Cells[4, 5].Value = "表头5测试好长一段内容";
                worksheet.Cells[5, 5].Value = "表头5测试好长一段内容";

                // 单元格根据内容自动填充
                worksheet.Cells[3, 4].AutoFitColumns();

                //缩小内容填充
                worksheet.Cells[4, 5].Style.ShrinkToFit = true;

                //换行显示
                worksheet.Cells[5, 5].Style.WrapText = true;

                // 直接通过内存导出，不用先保存文件了
                var excelFileStream = new MemoryStream(package.GetAsByteArray());

                return new FileStreamResult(excelFileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        [HttpGet("SaveFileDataExcel")]
        public IActionResult SaveFileDataExcel()
        {
            using (var package = new ExcelPackage(new FileInfo("ExportFile.xlsx")))
            {
                // 需要添加一个Sheet，名称为"填充数据Demo"
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("合并Demo");

                // 不需要创建行和列就可以定位单元格赋值
                worksheet.Cells[1, 1].Value = "Data1"; // 给第一行第一列单元格赋值为Data1
                worksheet.Cells[1, 2].Value = 666;// 给第一行第二列单元格赋值为666
                worksheet.Cells[1, 3].Value = "Data3";// 给第一行第三列单元格赋值为Data3
                worksheet.Cells[1, 4].Value = "Data4";// 给第一行第四列单元格赋值为Data4
                worksheet.Cells[1, 5].Value = 0.25;// 给第一行第五列单元格赋值为0.25

                worksheet.Cells["A2"].Value = "AData1"; // 给第二行第一列单元格赋值为AData1
                worksheet.Cells["B2"].Value = 888;// 给第二行第二列单元格赋值为888
                worksheet.Cells["C2"].Value = "AData3";// 给第二行第三列单元格赋值为AData3
                worksheet.Cells["D2"].Value = "AData4";// 给第二行第四列单元格赋值为AData4
                worksheet.Cells["E2"].Value = 0.25;// 给第二行第五列单元格赋值为0.25

                // 保存文件
                package.Save();
            }
            return Ok();
        }


        /// <summary>
        /// 合并单元格,指定开始位置和结束位置即可
        /// </summary>
        private void MergeCell(ExcelWorksheet sheet, int fromRow, int toRow, int fromCol, int toCol)
        {
            // 合并很简单，将其Merge设置为true即可
            sheet.Cells[fromRow, fromCol, toRow, toCol].Merge = true; 
            // 合并之后设置为水平居中和垂直居中
            sheet.Cells[fromRow, fromCol, toRow, toCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            sheet.Cells[fromRow, fromCol, toRow, toCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        /// <summary>
        /// 合并指定列值相等的行
        /// </summary>
        private void MergeCell(ExcelWorksheet sheet, int col, int startRow)
        {
            int fromRow = startRow;
            int fromCol = col;
            int toRow = startRow;
            int toCol = col;
            // 获取指定单元格的值
            string value = sheet.Cells[startRow + 1, col].Value?.ToString();
            // 循环遍历行
            for (int i = startRow + 1; i < sheet.Cells.Rows; i++)
            {
                // 获取相邻行对应单元格的值
                var valueNext = sheet.Cells[i + 1, col].Value?.ToString();
                // 比较是否相等
                if (value != valueNext)
                {
                    value = valueNext; // 将当前值更新为相邻值，方便后续继续遍历
                    toRow = i; 
                    // 设置合并属性
                    sheet.Cells[fromRow, fromCol, toRow, toCol].Merge = true; 
                    // 合并之后设置为垂直居中
                    sheet.Cells[fromRow, fromCol, toRow, toCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    // 更新开始位置继续遍历
                    fromRow = i + 1;
                }
                // 如果取得值为空就中断
                if (valueNext == null)
                {
                    break;
                }
            }
        }
    }
}
