using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Helpers
{
    public class UploadImages
    {
        public string UploadFile(IFormFile file, string id, string folder = "", string imgUrl = "", bool isEditMode = false)
        {
            if (isEditMode && file == null)
            {
                return imgUrl;
            }
            string relativePath = $"/Imgs/{folder}/{id}";
            string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{relativePath}");

            if (!Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string completePath = Path.Combine(absolutePath, fileName);

            using (var stream = new FileStream(completePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            if (isEditMode)
            {
                string[] oldImg = imgUrl.Split("/");
                string oldImgName = oldImg[^1];
                string completeOldImg = Path.Combine(absolutePath, oldImgName);
                if (System.IO.File.Exists(completeOldImg))
                {
                    System.IO.File.Delete(completeOldImg);
                }
            }

            return $"{relativePath}/{fileName}";
        }
    }
}
