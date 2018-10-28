using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todoer.Models.DbModels
{
    public class Checkpoint
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
    }
}
