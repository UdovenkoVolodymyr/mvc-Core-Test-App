using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MvcEducation.Domain.Interfaces
{
    public interface IFileStorage
    {
        string StoreFile(Stream fileStream, string rootPath, string fileName);
    }
}
