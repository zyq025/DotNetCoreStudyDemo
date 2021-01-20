using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDFDemo.Models;
using Wkhtmltopdf.NetCore;

namespace PDFDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFTestController : ControllerBase
    {
        // 生成pdf的实例对象
        private readonly IGeneratePdf _generatePdf;

        // 注入
        public PDFTestController(IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
        }

        /// <summary>
        /// 先看再下载
        /// </summary>
        [HttpGet("ExportPDFByHtml")]
        public IActionResult ExportPDFByHtml()
        {
            string strHtmlTemplate = @"<!DOCTYPE html>
                        <html>
                        <head>
                        </head>
                        <body>
                            <header>
                                <h1>Zoe</h1>
                            </header>
                            <div>
                                <h2>欢迎关注Code综艺圈</h2>
                            </div>
                        </body>";
            // 生成pdf
            var pdf = _generatePdf.GetPDF(strHtmlTemplate);
            var pdfFileStream = new MemoryStream();
            pdfFileStream.Write(pdf, 0,pdf.Length);
            pdfFileStream.Position = 0;
            // 以文件流形式返回，类型设置为application/pdf，在浏览器中可以查看
            return new FileStreamResult(pdfFileStream, "application/pdf");
        }

        /// <summary>
        /// 直接保存
        /// </summary>
        [HttpGet("SavePDFByHtml")]
        public IActionResult SavePDFByHtml()
        {
            // 导出html模板
            string strHtmlTemplate = @"<!DOCTYPE html>
                        <html>
                        <head>
                        </head>
                        <body>
                            <header>
                                <h1>Zoe</h1>
                            </header>
                            <div>
                                <h2>欢迎关注Code综艺圈</h2>
                            </div>
                        </body>";
            var pdf = _generatePdf.GetPDF(strHtmlTemplate);
            // 将文件流直接保存，指定保存的文件名，可以根据需求生成
            System.IO.File.WriteAllBytes("testpdf.pdf", pdf);
            return Ok("成功");
        }

        [HttpGet("TestMarginAndPageSize")]
        public IActionResult TestMarginAndPageSize()
        {
            string strHtmlTemplate = @"<!DOCTYPE html>
                        <html>
                        <head>
                        </head>
                        <body>
                            <header>
                                <h1>Zoe</h1>
                            </header>
                            <div>
                                <h2>欢迎关注Code综艺圈</h2>
                            </div>
                        </body>";
            var options = new ConvertOptions
            {
                PageMargins = new Wkhtmltopdf.NetCore.Options.Margins
                {
                    Bottom = 10,//下边距
                    Left = 0,//左边距
                    Right = 0,//右边距
                    Top = 15//上边距
                },
                PageSize = Wkhtmltopdf.NetCore.Options.Size.A5
            };
            _generatePdf.SetConvertOptions(options);
            var pdf = _generatePdf.GetPDF(strHtmlTemplate);
            var pdfFileStream = new MemoryStream();
            pdfFileStream.Write(pdf, 0, pdf.Length);
            pdfFileStream.Position = 0;
            return new FileStreamResult(pdfFileStream, "application/pdf");
        }

        [HttpGet("ExportByRazorView")]
        public async Task<IActionResult> ExportByRazorView()
        {
            var pdf = await _generatePdf.GetByteArray("Views/Zoe.cshtml");
            var pdfFileStream = new MemoryStream();
            pdfFileStream.Write(pdf, 0, pdf.Length);
            pdfFileStream.Position = 0;
            return new FileStreamResult(pdfFileStream, "application/pdf");
        }

        [HttpGet("ExportByRazorViewData")]
        public async Task<IActionResult> ExportByRazorViewData()
        {
            Zoe zoe = new Zoe { 
                Name="Zoe，好酷",
                WeiXin="Code综艺圈"
            };
            var pdf = await _generatePdf.GetByteArray("Views/ZoeData.cshtml",zoe);
            var pdfFileStream = new MemoryStream();
            pdfFileStream.Write(pdf, 0, pdf.Length);
            pdfFileStream.Position = 0;
            return new FileStreamResult(pdfFileStream, "application/pdf");
        }
    }
}
