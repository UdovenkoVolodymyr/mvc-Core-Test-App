using McvEducationApp.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace McvEducationApp.BusinessLogic.Interfaces
{
    public interface ILessonService
    {
        LessonDTO GetLesson(int id);
        int CreateLesson(LessonDTO courseDTO);
        void DeleteLesson(int[] Ids);
        void EditLesson(LessonDTO courseDTO);
    }
}
