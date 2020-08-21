using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MvcEducationApp.Infrastructure.Data
{
    public class FileStorage : IFileStorage
    {
        public string StoreFile(Stream fileStream, string rootPath, string fileName)
        {
            var path = $"{rootPath}/files/{fileName}.mp4";
            var pathForBrowser = $"/files/{fileName}.mp4";
            using (var resultStream = new FileStream( path, FileMode.Create))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.CopyTo(resultStream);
                fileStream.Close();
            }
            return pathForBrowser;
        }
    }
}
