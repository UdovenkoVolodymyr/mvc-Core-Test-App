using AutoMapper;
using McvEducationApp.BusinessLogic.DTO;
using McvEducationApp.BusinessLogic.Interfaces;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace McvEducationApp.BusinessLogic.Services
{
    public class LessonService: ILessonService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public LessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public LessonDTO GetLesson(int id)
        {
            var lesson = _unitOfWork.GetRepository<Lesson>().GetWithInclude(x => x.Id == id, p => p.Course, p => p.CreatedBy, v => v.VideoFile).FirstOrDefault();
            var lessonDTO = _mapper.Map<Lesson, LessonDTO>(lesson);
            return lessonDTO;
        }

        public int CreateLesson(LessonDTO lessonDTO)
        {
            var lesson = _mapper.Map<LessonDTO, Lesson>(lessonDTO);
            lessonDTO.LastUpdated = DateTime.UtcNow;

            _unitOfWork.GetRepository<Lesson>().Create(lesson);
            return lesson.Id;
        }

        public void DeleteLesson(int[] IdsToDelete)
        {
            foreach (var lessonId in IdsToDelete)
            {
                _unitOfWork.GetRepository<Lesson>().RemoveById(lessonId);
            }
        }

        public void EditLesson(LessonDTO lessonDTO)
        {
            var modelToEdit = GetLesson(lessonDTO.Id);
            modelToEdit.LastUpdated = DateTime.UtcNow;
            modelToEdit.Title = lessonDTO.Title;
            modelToEdit.Description = lessonDTO.Description;
            modelToEdit.Text = lessonDTO.Text;
            modelToEdit.InfoType = lessonDTO.InfoType;

            var lesson = _mapper.Map<LessonDTO, Lesson>(modelToEdit);
            lesson.LastUpdated = DateTime.UtcNow;
            _unitOfWork.GetRepository<Lesson>().Update(lesson);
        }
    }
}
