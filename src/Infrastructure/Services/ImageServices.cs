using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Interfaces.Images;

namespace Infrastructure.Services;
public class ImageServices(Cloudinary cloudinaryService) : IImageServices
{
    public async Task<string> Upload(Stream imageStream, string folderName, string fileName)
    {
        ImageUploadParams imageUploadParams = new()
        {
            File = new FileDescription(fileName, imageStream),
            Folder = folderName,
            Overwrite = true,
        };
        var uploadResult = await cloudinaryService.UploadAsync(imageUploadParams);
        if (uploadResult is null) return string.Empty;
        return uploadResult.Url.ToString();
    }
}