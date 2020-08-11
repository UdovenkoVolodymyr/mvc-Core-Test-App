using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.Services
{
    public interface IModelService<TModel>
    {
        IActionResult DeleteModelHandler(TModel model);
    }
}
