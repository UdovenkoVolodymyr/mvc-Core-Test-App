using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp
{
    public class DebugLogger
    {
        public ILogger logger;

        public DebugLogger()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddDebug();
            });
            logger = loggerFactory.CreateLogger<Program>();
        }
    }
}
