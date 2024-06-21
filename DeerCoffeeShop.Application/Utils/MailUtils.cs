using System.Net;
using System.Net.Mail;
using DeerCoffeeShop.Domain.Entities;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DeerCoffeeShop.Application.Utils
{
    public static class MailUtils
    {
        public static async Task SendMail(MailContent mailContent)
        {
            var dic = Directory.GetCurrentDirectory();

            IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", true, true)
             .Build();
            var result = Directory.GetCurrentDirectory();
            var mailSettings = config.GetSection("MailSettings").Get<MailSettings>();

            var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings?.DisplayName, mailSettings?.Mail);
            email.From.Add(new MailboxAddress(mailSettings?.DisplayName, mailSettings?.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings?.Host, mailSettings.Port | 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                // System.IO.Directory.CreateDirectory("mailssave");
                // var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                // await email.WriteToAsync(emailsavefile);

                System.Console.WriteLine("errors: ", ex);

                // logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                // logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

        }

        public static async Task SendEmailAsync(string userName, string userEmail, string address, string phoneNumber, string dateOfBirth, string subject)
        {
            var body = MailBody.getConfirmEmail(userName, userEmail, address, phoneNumber, dateOfBirth, "Deer Coffee");
            await SendMail(new MailContent()
            {
                To = userEmail,
                Subject = subject,
                Body = body
            });
        }
        public static async Task SendEmailAsync(string userName, string userEmail, string subject, DateTime date, string address)
        {
            var body = MailBody.getApprovedEmail(userName, userEmail, "Deer Coffee", date, address);
            await SendMail(new MailContent()
            {
                To = userEmail,
                Subject = subject,
                Body = body
            });
        }
        public static async Task SendPasswordAsync(string userEmail, string userName, string userID,string companyName)
        {
            var body = MailBody.getPasswordEmail(userName, userID, DateTime.Now,companyName);
            await SendMail(new MailContent()
            {
                To = userEmail,
                Subject = "Password",
                Body = body
            });
        }

    }
}