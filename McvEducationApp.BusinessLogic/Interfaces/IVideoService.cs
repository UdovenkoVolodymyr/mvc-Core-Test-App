using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace McvEducationApp.BusinessLogic.Interfaces
{
    public interface IVideoService
    {
        void UploadFileAsync(Stream fileStream, string rootPath, int lessonId);
    }
}
