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
        public static void DeleteFile(string FolderName, string FileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", FolderName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
