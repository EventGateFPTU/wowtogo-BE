using System.Drawing;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Domain.Interfaces.Email;
using Domain.Interfaces.QRCoder;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using QRCoder;

namespace Infrastructure.Services;
public class MailService(IOptionsMonitor<MailSettings> mailSettings, IQRCoderServices qrCoderServices) : IMailService
{
    private readonly MailSettings _mailSettings = mailSettings.CurrentValue;
    public bool SendCreateRequestHtmlMail(string userName, string requestType, string operation, string documentName, string reason, Guid documentId, string email)
    {
        throw new NotImplementedException();
    }

    public bool SendResetPasswordHtmlMail(string userEmail, string temporaryPassword, string token)
    {
        throw new NotImplementedException();
    }

    public bool SendShareEntryHtmlMail(bool isDirectory, string name, string sharerName, string operation, string ownerName, string email, string path)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SendTicketGeneratedMailAsync(string toEmail, string code)
    {
        var httpClient = new HttpClient();
        var request = new
        {
            template_uuid = _mailSettings.TemplateId,
            from = new
            {
                email = _mailSettings.SenderEmail,
                name = _mailSettings.Username
            },
            to = new[] { new {
                    email = toEmail
                }},
            template_variables = new
            {
                ticket_code = code,
            },
            attachments = new[]{
                new{
                    content=qrCoderServices.EncodeToBase64(code),
                    type="image/png",
                    filename=$"qr-{code}.png",
                    disposition="inline",
                    content_id="qr_code"
                }
            }
        };
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_mailSettings.Token}");
        // request.Headers.Add("Content-Type", $"application/json");
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(_mailSettings.Host, request);
        if (response.StatusCode != HttpStatusCode.OK) return false;
        return true;
    }
}