using System.Drawing;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Domain.Interfaces.Email;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using QRCoder;

namespace Infrastructure.Services;
public class MailService(IOptionsMonitor<MailSettings> mailSettings) : IMailService
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
            template_uuid = "b00bdc21-361b-4261-beaa-d87964b4e847",
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
                qr_code = Base64Encode(code)
            },
            attachments = new[]{
                new{
                    content=Base64Encode(code),
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
    public static string Base64Encode(string plainText)
    {
        QRCodeGenerator qRCodeGenerator = new();
        QRCodeData qr = qRCodeGenerator.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.Q);
        Base64QRCode qrCode = new Base64QRCode(qr);
        string qrCodeImageAsBase64 = qrCode.GetGraphic(20, Color.Black, Color.White, true, Base64QRCode.ImageType.Png);
        var htmlPictureTag = $"{qrCodeImageAsBase64}";
        return htmlPictureTag;
    }
}