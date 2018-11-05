using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todoer.Enums;

namespace Todoer.Services.Interfaces
{
    public interface IPriorityConverterService
    {
        string EnumToString(Priority priority);
    }
}
