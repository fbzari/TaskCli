using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCli.Enums;

namespace TaskCli.Dtos
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public TaskProgress Progress { get; set; } = TaskProgress.todo;
    }
}
