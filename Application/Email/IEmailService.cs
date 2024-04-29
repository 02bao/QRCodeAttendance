namespace QRCodeAttendance.Application.Email
{
    public interface IEmailService
    {
        bool SendEmail(string ReceiveEmail, string ReceiveName, string Subjects, string Body);
        bool SendRegisterEmail(string ReceiveEmails, string ReceiveName, string Tokens);
    }
}
