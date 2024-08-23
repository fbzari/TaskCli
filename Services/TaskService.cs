using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCli.Dtos;
using TaskCli.Enums;

namespace TaskCli.Services
{
    internal class TaskService : ITaskService
    {
        private readonly string _filePath;
        private readonly Dictionary<int, Tasks> _tasks = new Dictionary<int, Tasks>();
        public TaskService(string filePath)
        {
            _filePath = filePath;
            if (File.Exists(_filePath))
            {
                var jsonData = File.ReadAllText(_filePath);
                _tasks = JsonConvert.DeserializeObject<Dictionary<int, Tasks>>(jsonData) ?? new Dictionary<int, Tasks>();
            }
            else
            {
                _tasks = new Dictionary<int, Tasks>();
            }
        }
        public int AddTasks(string description)
        {
            int newId = _tasks.Count > 0 ? _tasks.Keys.Max() + 1 : 1;
            var task = new Tasks
            {
                Description = description,
                Id = newId,
            };
            _tasks[newId] = task;
            SaveTasks();
            return newId;
        }

        public void deleteAll()
        {
            _tasks.Clear();
        }

        public void DeleteTasks(int id)
        {
            if (_tasks.ContainsKey(id))
            {
                _tasks.Remove(id);
                SaveTasks();
            }
            else
            {
                throw new Exception("Task not found");
            }
        }

        public Dictionary<int, Tasks> GetTasks()
        {
            return _tasks;
        }

        public Dictionary<int, Tasks> GetTasks(TaskProgress taskProgress)
        {
            var filteredTasks = _tasks.Where(task => task.Value.Progress == taskProgress)
                              .ToDictionary(task => task.Key, task => task.Value);

            return filteredTasks;
        }

        public void UpdateProgress(int id, TaskProgress progress)
        {
            if (_tasks.ContainsKey(id))
            {
                _tasks[id].Progress = progress;
                SaveTasks();
            }
            else
            {
                throw new Exception("Task not found");
            }
        }

        public void UpdateTasks(int id, string description)
        {
            if (_tasks.ContainsKey(id))
            {
                _tasks[id].Description = description;
                SaveTasks();
            }
            else
            {
                throw new Exception("Task not found");
            }
        }

        private void SaveTasks()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new StringEnumConverter() }, // Convert enum to string
                Formatting = Formatting.Indented
            };
            var jsonData = JsonConvert.SerializeObject(_tasks, jsonSettings);
            File.WriteAllText(_filePath, jsonData);
        }

        public string getFIlePath()
        {
            return _filePath;
        }

    }
}
