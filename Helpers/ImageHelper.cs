namespace Weav_App.Helpers;

public static class ImageHelper
{
    public static async Task<string?> SaveProductImageAsync(IFormFile file, string webRootPath)
    {
        if (file == null || file.Length == 0)
            return null;

        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

        var relativePath = Path.Combine("images", "products", uniqueFileName);     
        var fullPath = Path.Combine(webRootPath, relativePath);                    

        var directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return "/" + relativePath.Replace("\\", "/"); 
    }
}