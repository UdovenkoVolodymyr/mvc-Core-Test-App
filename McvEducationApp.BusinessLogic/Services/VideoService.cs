using McvEducationApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MvcEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace McvEducationApp.BusinessLogic.Services
{
    public class VideoService : IVideoFileService
    {
        /*private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _appEnvironment;

        public VideoService(IUnitOfWork unitOfWork, IWebHostEnvironment appEnvironment)
        {
            _unitOfWork = unitOfWork;
            _appEnvironment = appEnvironment;
        }

        public async System.Threading.Tasks.Task UploadFileAsync(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var file = new VideoFile { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }*/
        public Task UploadFileAsync(IFormFile uploadedFile)
        {
            throw new NotImplementedException();
        }
    }
}
