using System.Collections.Generic;

namespace LearnerProgressApp.Models
{
    public class Subject
    {
        public string Name { get; set; }
        public List<TaskItem> Tasks { get; set; } = new();
    }
}
