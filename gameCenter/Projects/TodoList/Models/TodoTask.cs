using System;


namespace gameCenter.Projects.TodoList.Models
{
    internal class TodoTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedTime { get; set; }
        public TodoTask(int id, string description, bool isComplete = false)
        {
            Id = id;
            Description = description;
            IsComplete = isComplete;

            // Initialize CreatedTime when a task is created
            CreatedTime = DateTime.Now;
        }
    }
}
