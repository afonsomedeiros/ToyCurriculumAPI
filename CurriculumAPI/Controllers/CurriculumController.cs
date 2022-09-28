using Microsoft.AspNetCore.Mvc;
using CurriculumAPI.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using System.Reflection;

namespace CurriculumAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CurriculumController : ControllerBase
{
    private readonly ILogger<CurriculumController> _logger;

    public CurriculumController(ILogger<CurriculumController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public CurriculumResponse Post([FromBody] Curriculum curriculum)
    {
        var document = new PdfDocument();
        var page = document.Pages.Add();
        var gfx = page.Graphics;
        int word_position_x = 0;
        int word_position_y = 0;
        var font = new PdfStandardFont(PdfFontFamily.TimesRoman, 20, PdfFontStyle.Regular);
        var font_bold = new PdfStandardFont(PdfFontFamily.TimesRoman, 20, PdfFontStyle.Bold);
        
        foreach(PropertyInfo prop in curriculum.GetType().GetProperties())
        {
            gfx.DrawString(prop.Name+": ", font_bold, PdfBrushes.Black, new PointF(word_position_x,word_position_y));
            gfx.DrawString(prop.GetValue(curriculum).ToString(), font, PdfBrushes.Black, new PointF(word_position_x+100,word_position_y));   
            word_position_y += 30;
        }

        MemoryStream stream = new MemoryStream();
        document.Save(stream);
        stream.Position = 0;

        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, (int)stream.Position, (int)stream.Length);

        return new CurriculumResponse() { Curriculum_base64 = Convert.ToBase64String(buffer) };
    }
}
