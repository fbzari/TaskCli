using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCli.Dtos;
using TaskCli.Enums;

namespace TaskCli.Services
{
    internal interface ITaskService
    {
        int AddTasks(string task);
        void UpdateProgress(int id, TaskProgress progress);
        void UpdateTasks(int id, string progress);
        void DeleteTasks(int task);
        Dictionary<int, Tasks> GetTasks(); 
        Dictionary<int, Tasks> GetTasks(TaskProgress taskProgress);
        void deleteAll();

        string getFIlePath();
    }
}
