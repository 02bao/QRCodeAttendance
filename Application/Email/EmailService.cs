using MimeKit;
using MailKit.Net.Smtp;
using QRCodeAttendance.Infrastructure.Data;

namespace QRCodeAttendance.Application.Email
{
    public class EmailService : IEmailService
    {
        public string Email { get; set; } = "thaibaoluong02@gmail.com";
        public string Name { get; set; } = "Thai Bao";
        public string SmtpService { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public string SmtpName { get; set; } = "thaibaoluong02@gmail.com";
        public string SmtpPassword { get; set; } = "uypq kjwj nqxi dpzq";

        public bool SendEmail(string ReceiveEmail, string ReceiveName, string Subjects, string Body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(Name, Email));
                message.To.Add(new MailboxAddress(ReceiveName, ReceiveEmail));
                message.Subject = Subjects;
                var builder = new BodyBuilder();
                builder.TextBody = Body;
                message.Body = builder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpService, Port, false);
                    client.Authenticate(SmtpName, SmtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SendRegisterEmail(string ReceiveEmails, string ReceiveName, string Token)
        {
            try
            {
                var Emails = new EmailService();
                string Url = "http://localhost:3000/api/Users/Verify/" + Token;
                string Subject = "Activate Your Account";
                string Body = "Chào mừng bạn đã đăng kí tài khoản ITStudy.Vui lòng xác nhận tài khoản bằng cách " +
                    "click vào đường dẫn sau : " + Url;

                return Emails.SendEmail(ReceiveEmails, ReceiveName, Subject, Body);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }   
}
