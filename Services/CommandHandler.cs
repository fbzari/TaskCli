using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCli.Dtos;
using TaskCli.Enums;

namespace TaskCli.Services
{
    internal class CommandHandler : ICommandHandler
    {
        private readonly ITaskService _taskService;

        public CommandHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Core method
        public void ExcecuteCommand(string command, string[] args)
        {
            switch (command)
            {
                case "add":
                    if (args.Length > 1)
                    {
                        string description = string.Join(" ", args.Skip(1));
                        int result = _taskService.AddTasks(description);
                        Console.WriteLine($"Task added successfully (ID: {result})");
                    }
                    else
                    {
                        Console.WriteLine("Usage: task-cli add <description>");
                    }
                    break;

                case "update":
                    if (args.Length > 2 && int.TryParse(args[1], out int taskId) && !string.IsNullOrEmpty(args[2]))
                    {
                        string desc = string.Join(" ", args.Skip(2));
                        _taskService.UpdateTasks(taskId, desc);
                        Console.WriteLine("Task updated successfully");
                    }
                    else
                    {
                        Console.WriteLine("Usage: task-cli update <id> <description>");
                    }
                    break;

                case "mark-in-progress":
                    if (args.Length > 1 && int.TryParse(args[1], out int progressId))
                    {
                        _taskService.UpdateProgress(progressId, TaskProgress.InProgress);
                        Console.WriteLine("Progress updated successfully");
                    }
                    else
                    {
                        Console.WriteLine("Usage: task-cli mark-in-progress <id>");
                    }
                    break;

                case "mark-done":
                    if (args.Length > 1 && int.TryParse(args[1], out int doneId))
                    {
                        _taskService.UpdateProgress(doneId, TaskProgress.Done);
                        Console.WriteLine("Progress updated successfully");
                    }
                    else
                    {
                        Console.WriteLine("Usage: task-cli mark-done <id>");
                    }
                    break;

                case "delete":
                    if (args.Length > 1 && int.TryParse(args[1], out int deleteTaskId))
                    {
                        _taskService.DeleteTasks(deleteTaskId);
                        Console.WriteLine("Task deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Usage: task-cli delete <id>");
                    }
                    break;

                case "delete-all":
                    _taskService.deleteAll();
                    Console.WriteLine("All Task deleted successfully");
                    break;

                case "list":
                    if (args.Length > 1)
                    {
                        var progressFilter = args[1].ToLower();
                        if (Enum.TryParse(progressFilter, ignoreCase: true, out TaskProgress taskProgress))
                        {
                            var tasks = _taskService.GetTasks(taskProgress);
                            DisplayTasks(tasks);
                        }
                        else
                        {
                            ShowListUsage();
                        }
                    }
                    else
                    {
                        var tasks = _taskService.GetTasks();
                        DisplayTasks(tasks);
                    }
                    break;

                case "get-file-path":
                    Console.WriteLine($"Json File Path ==> {_taskService.getFIlePath()}");
                    break;


                default:
                    Console.WriteLine("Usage: task-cli <command> [options]");
                    Console.WriteLine("Commands:");
                    Console.WriteLine("  add <description>   - Add a new task");
                    Console.WriteLine("  update <id> <description> - Update task");
                    Console.WriteLine("  delete <id>         - Delete task");
                    Console.WriteLine("  mark-in-progress <id>         - Mark task as progress");
                    Console.WriteLine("  mark-done <id>         - Mark task as done");
                    Console.WriteLine("  delete-all          - Delete task");
                    Console.WriteLine("  list                - List all tasks");
                    Console.WriteLine("  list done              - List done tasks");
                    Console.WriteLine("  list todo              - List todo tasks");
                    Console.WriteLine("  list in-progress        - List in-progress tasks");
                    Console.WriteLine("  get-file-path        - get Json file path which contains all details");
                    break;
            }
        }

       
        public void HandleCommand(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Task CLI started. Type 'exit' to quit.");

                while (true)
                {
                    Console.Write("> "); // Display prompt
                    var input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input)) continue;

                    var inputArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (inputArgs.Length == 0) continue;

                    string command = inputArgs[0].ToLower();

                    if (command == "exit")
                    {
                        break;
                    }

                    ExcecuteCommand(command, inputArgs);
                }
            }
            else
            {
                string command = args[0].ToLower();
                ExcecuteCommand(command, args);
            }

        }

        #region Helper Function
        private void DisplayTasks(Dictionary<int, Tasks> tasks)
        {
            int maxIdLength = tasks.Keys.Max().ToString().Length;
            int maxDescriptionLength = tasks.Values.Max(task => task.Description.Length);
            int maxProgressLength = tasks.Values.Max(task => task.Progress.ToString().Length);

            maxIdLength = Math.Max(maxIdLength, 2); 
            maxDescriptionLength = Math.Max(maxDescriptionLength, 18); 
            maxProgressLength = Math.Max(maxProgressLength, 10); 

            Console.WriteLine($"{"Id".PadRight(maxIdLength)} | {"Description".PadRight(maxDescriptionLength)} | {"Progress".PadRight(maxProgressLength)}");
            Console.WriteLine($"{new string('-', maxIdLength)}-|-{new string('-', maxDescriptionLength)}-|-{new string('-', maxProgressLength)}");

            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Key.ToString().PadRight(maxIdLength)} | {task.Value.Description.PadRight(maxDescriptionLength)} | {task.Value.Progress.ToString().PadRight(maxProgressLength)}");
            }
        }


        // Method to show usage instructions
        private void ShowListUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  list done              - List done tasks");
            Console.WriteLine("  list todo              - List todo tasks");
            Console.WriteLine("  list in-progress        - List in-progress tasks");
        }
        #endregion
    }
}

