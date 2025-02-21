namespace USAFlag.Auth.Core.Shared.Helpers;
public static class SaveDocument
{
    private const string rootPath = "wwwroot";
    private const string rootFolder = "Images";
    public static async Task<string> SaveDoc(string folderPath, IFormFile file, CancellationToken token = new())
    {
        var ext = GetFileExtension(file.FileName);
        var directory = CreateDirectory(folderPath, ext);
        await using var fileStream = new FileStream(directory, FileMode.Create);
        await file.CopyToAsync(fileStream, token).ConfigureAwait(false); ;
        return directory;
    }
    private static string CreateDirectory(string path, string ext)
    {
        path = $"{rootPath}/{rootFolder}/{path}";
        var fullPath = Combine(path);
        CreateFolder(fullPath);
        var uniqueId = Guid.NewGuid().GenerateUniqueId();
        fullPath = $"{path}/{uniqueId + ext}";
        return Combine(fullPath);
    }
    private static string Combine(string path)
    {
        return Path.Combine(path);
    }
    private static void CreateFolder(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath!);
        }
    }
    private static string GetFileExtension(string fileName)
    {
        return Path.GetExtension(fileName);
    }
}
