using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using InventorySystem.Data;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class InventoryEmailJob : BackgroundService
{
    private readonly IServiceProvider _services;

    public InventoryEmailJob(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var targetTime = DateTime.Today.AddHours(19); // Daily at 7:00 PM

            if (now > targetTime)
                targetTime = targetTime.AddDays(1);

            var delay = targetTime - now;
            Console.WriteLine($"🕖 Waiting until {targetTime:yyyy-MM-dd HH:mm:ss} to send the inventory report...");
            await Task.Delay(delay, stoppingToken);

            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

            var inventoryList = db.Inventory.ToList();
            var materialDict = db.Materials.ToDictionary(m => m.Id, m => m);

            var sb = new StringBuilder();
            var warningList = new StringBuilder();
            var normalList = new StringBuilder();
            bool hasWarning = false;

            foreach (var item in inventoryList)
            {
                if (!materialDict.TryGetValue(item.MaterialId, out var material)) continue;

                if (item.Quantity < material.Threshold)
                {
                    warningList.AppendLine($"🔻 {material.Name}: {item.Quantity} {material.Unit} (below threshold {material.Threshold})");
                    hasWarning = true;
                }
                else
                {
                    normalList.AppendLine($"🔹 {material.Name}: {item.Quantity} {material.Unit}");
                }
            }

            sb.AppendLine($"📦 Inventory Report - {DateTime.Now:yyyy-MM-dd}");
            sb.AppendLine();

            if (normalList.Length > 0)
            {
                sb.AppendLine("✅ In Stock:");
                sb.AppendLine(normalList.ToString().TrimEnd());
                sb.AppendLine();
            }

            if (hasWarning)
            {
                sb.AppendLine("⚠️ Low Stock Warning:");
                sb.AppendLine(warningList.ToString().TrimEnd());
            }

            string subject = hasWarning ? "⚠️ Low Stock Alert" : "✅ Daily Inventory Summary";
            string body = sb.ToString();

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress("inventory.shamrock@gmail.com");

                // Multiple recipients
                var recipients = new[]
                {
                    "inventory.shamrock@gmail.com",
                    "zwang237@syr.edu",
                    "dawntang@shamrockmh.com",
                    "jasmineyeh@shamrockmh.com"
                };

                foreach (var email in recipients)
                {
                    mail.To.Add(email);
                }

                mail.Subject = subject;
                mail.Body = body;

                using var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("inventory.shamrock@gmail.com", "ahetbcdfvwsingim");
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(mail, stoppingToken);
                Console.WriteLine($"[Mail] Email sent successfully at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Mail] Failed to send email: {ex.Message}");
            }
        }
    }
}
