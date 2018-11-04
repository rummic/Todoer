using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todoer.Enums;

namespace Todoer.Models.DbModels
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Done { get; set; }
        public DateTime Deadline { get; set; }
        public virtual List<Checkpoint> Checkpoints { get; set; }
        public Priority Priority { get; set; }

    }
}
