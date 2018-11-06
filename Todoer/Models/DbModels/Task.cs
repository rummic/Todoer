using System;
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
        public string ApplicationUserId { get; set; }
        public Priority Priority { get; set; }
    }
}
