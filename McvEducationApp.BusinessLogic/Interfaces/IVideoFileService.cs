using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace McvEducationApp.BusinessLogic.Interfaces
{
    public interface IVideoFileService
    {
        Task UploadFileAsync(IFormFile uploadedFile);
    }
}
