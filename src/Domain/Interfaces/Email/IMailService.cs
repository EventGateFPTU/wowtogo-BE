namespace Domain.Interfaces.Email;
public interface IMailService
{
    bool SendResetPasswordHtmlMail(string userEmail, string temporaryPassword, string token);
    bool SendShareEntryHtmlMail(bool isDirectory, string name, string sharerName, string operation, string ownerName, string email, string path);
    bool SendCreateRequestHtmlMail(string userName, string requestType, string operation, string documentName, string reason, Guid documentId, string email);
    Task<bool> SendTicketGeneratedMailAsync(string userName, string code);
}