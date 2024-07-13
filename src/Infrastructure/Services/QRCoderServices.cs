using System.Drawing;
using Domain.Interfaces.QRCoder;
using QRCoder;

namespace Infrastructure.Services;
public class QRCoderServices : IQRCoderServices
{
    public string EncodeToBase64(string plainText)
    {
        QRCodeGenerator qRCodeGenerator = new();
        QRCodeData qr = qRCodeGenerator.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.Q);
        Base64QRCode qrCode = new Base64QRCode(qr);
        string qrCodeImageAsBase64 = qrCode.GetGraphic(20, Color.Black, Color.White, true, Base64QRCode.ImageType.Png);
        string htmlPictureTag = $"{qrCodeImageAsBase64}";
        return htmlPictureTag;
    }
}