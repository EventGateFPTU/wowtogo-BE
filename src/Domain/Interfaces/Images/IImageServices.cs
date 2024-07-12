namespace Domain.Interfaces.Images;
public interface IImageServices
{
    Task<string> Upload(Stream imageStream, string folderName, string type);
}