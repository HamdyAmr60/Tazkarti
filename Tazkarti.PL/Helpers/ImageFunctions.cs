namespace Tazkarti.PL.Helpers
{
    public static class ImageFunctions
    {
        public static string Upload(IFormFile file, string FolderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", FolderName);
            var FileName = Guid.NewGuid() + file.FileName;

            var FilePath = Path.Combine(FolderPath, FileName);

            var fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(fs);
            return Path.Combine("images\\Parties", FileName);
        }
        public static void DeleteFile(string fileName)
        {
            // Combine folder name and file name to get the full path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", fileName);

            // Check if the file exists and delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
