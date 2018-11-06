using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todoer.Enums;

namespace Todoer.Models.DtoModels
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public TimeSpan DeadlineTime { get; set; }
        public Priority Priority { get; set; }

    }
}
