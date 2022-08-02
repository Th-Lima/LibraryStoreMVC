using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryStore.App.Helpers
{
    public static class ImageHelper
    {
        public static async Task<bool> UploadImage(IFormFile file, string imgPrefix, ModelStateDictionary modelState)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

            if (File.Exists(path))
            {
                modelState.AddModelError(string.Empty, "Já existe um arquivo com este nome");

                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }
    }
}
