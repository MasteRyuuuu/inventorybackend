using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        [HttpGet("test-send")]
        public async Task<IActionResult> TestSend()
        {
            var mail = new MailMessage();
            mail.From = new MailAddress("inventory.shamrock@gmail.com");
            mail.To.Add("inventory.shamrock@gmail.com"); // 可先发送给自己
            mail.Subject = "📧 测试邮件";
            mail.Body = "这是一封来自 Gmail SMTP 的测试邮件。";

            using var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("inventory.shamrock@gmail.com", "ahetbcdfvwsingim");
            smtp.EnableSsl = true;

            try
            {
                await smtp.SendMailAsync(mail);
                return Ok("✅ 邮件发送成功！");
            }
            catch (SmtpException smtpEx)
            {
                return BadRequest("❌ SMTP 异常: " + smtpEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("❌ 发送失败: " + ex.Message);
            }
        }
    }
}

