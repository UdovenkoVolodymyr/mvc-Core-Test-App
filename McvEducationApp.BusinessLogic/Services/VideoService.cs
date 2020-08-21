using McvEducationApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MvcEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MvcEducationApp.Domain.Core.Models;

namespace McvEducationApp.BusinessLogic.Services
{
    public class VideoService : IVideoService
    {
        private IFileStorage _fileStorage;
        private IUnitOfWork _unitOfWork;

        public VideoService(IFileStorage fileStorage, IUnitOfWork unitOfWork)
        {
            _fileStorage = fileStorage;
            _unitOfWork = unitOfWork;
        }
        public void UploadFileAsync(Stream fileStream, string rootPath, int lessonId)
        {
            var lessonName = $"lesson{lessonId}video";
            var filePath = _fileStorage.StoreFile(fileStream, rootPath, lessonName);

            var file = new VideoFile { Name = lessonName, Path = filePath, LessonId = lessonId };
            _unitOfWork.GetRepository<VideoFile>().Create(file);
        }
    }
}
