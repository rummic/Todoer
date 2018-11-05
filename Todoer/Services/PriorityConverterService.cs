using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todoer.Enums;
using Todoer.Services.Interfaces;

namespace Todoer.Services
{
    public class PriorityConverterService : IPriorityConverterService
    {
        public string EnumToString(Priority priority)
        {
            switch (priority)
            {
                case Priority.Important:
                    return "card text-white bg-danger mb-3";
                case Priority.Moderate:
                    return "card text-white bg-warning mb-3";
                case Priority.Neutral:
                    return "card text-white bg-info mb-3";
                default:
                    return "card text-white bg-secondary mb-3";

            }
        }
    }
}
