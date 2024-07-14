namespace Domain.Interfaces.QRCoder;
public interface IQRCoderServices
{
    string EncodeToBase64(string plainText);
}