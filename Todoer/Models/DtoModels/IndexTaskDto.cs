using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todoer.Models.DtoModels
{
    public class IndexTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DeadlineDate { get; set; }
        public string DeadlineTime { get; set; }
        public string Priority { get; set; }

    }
}
