using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Captcha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        [HttpGet]
        [Route("getCaptcha")]
        public async Task<IActionResult> GetCaptchaAsync()
        {
            string captchaText = GenerateRandomText();
            byte[] captchaImageBytes = GenerateCaptchaImage(captchaText);
            HttpContext.Session.SetString("captcha", captchaText);
            return File(captchaImageBytes, "image/jpeg");
        }
        private string GenerateRandomText(int length = 6)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private byte[] GenerateCaptchaImage(string text)
        {
            int width = 180;
            int height = 30;
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.AliceBlue);
                    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    using (Font font = new Font("Times New Roman", 16, FontStyle.Bold))
                    {
                        using (Brush brush = new SolidBrush(Color.Black))
                        {
                            SizeF textSize = graphics.MeasureString(text, font);
                            float x = (width - textSize.Width) / 2;
                            float y = (height - textSize.Height) / 2;

                            graphics.DrawString(text, font, brush, x, y);
                        }
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Jpeg);
                        return ms.ToArray();
                    }
                }
            }
        }

        [HttpPost]
        [Route("validateCaptcha")]
        public IActionResult ValidateCaptcha([FromBody] string userInputCaptcha)
        {
            string storedCaptcha = HttpContext.Session.GetString("captcha");

            if (storedCaptcha != null && storedCaptcha.Equals(userInputCaptcha, StringComparison.OrdinalIgnoreCase))
            {
                return Ok(new { Success = true, Message = "CAPTCHA validation succeeded." });
            }
            else
            {
                return BadRequest(new { Success = false, Message = "CAPTCHA validation failed." });
            }
        }
    }
}
