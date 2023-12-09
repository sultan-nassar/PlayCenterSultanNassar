using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace gameCenter.Projects.TodoList.Models
{
    internal class TodoListModel
    {
        public ObservableCollection<TodoTask> Tasks { get; set; }

        public TodoListModel()
        {
            Tasks = new ObservableCollection<TodoTask>();
            LoadTasksFromJson();  // Load tasks when the model is created

        }

        //Update Task(int id)
        public void UpdateTask(int taskId, string newDescription)
        {

            TodoTask task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                task.Description = newDescription;
                SaveTasksToJson();  // Save tasks when a task is removed

            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }

        //ToggleTaskIsComplete(int id)
        public void ToggleTaskIsComplete(int taskId)
        {
            TodoTask task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                task.IsComplete = !task.IsComplete;
            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }

        //AddNewTask(task)
        public void AddNewTask(TodoTask task)
        {
            Tasks.Add(task);
            SaveTasksToJson();  // Save tasks when a new task is added

        }

        public bool RemoveTask(int taskId)
        {
            TodoTask taskToRemove = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (taskToRemove != null)
            {
                Tasks.Remove(taskToRemove);
                SaveTasksToJson();  // Save tasks when a task is removed

                return true;
            }
            return false;
        }



        // Save tasks to JSON file
        private void SaveTasksToJson()
        {
            string filePath = GetJsonFilePath();
            string jsonString = JsonSerializer.Serialize(Tasks);
            File.WriteAllText(filePath, jsonString);
        }


        // Load tasks from JSON file
        private void LoadTasksFromJson()
        {
            string filePath = GetJsonFilePath();
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);
                    Tasks = JsonSerializer.Deserialize<ObservableCollection<TodoTask>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}");
            }
        }

        // Get the full path of the JSON file
        private string GetJsonFilePath()
        {
            return Path.Combine(Environment.CurrentDirectory, "tasks.json");
        }

    }
}
