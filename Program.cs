using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using TaskCli.Services;

namespace TaskCliApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonFile", "TaskManage.json");

            Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITaskService>(new TaskService(jsonFilePath))
                .AddSingleton<ICommandHandler, CommandHandler>() 
                .BuildServiceProvider();

            var commandHandler = serviceProvider.GetService<ICommandHandler>();

            if (commandHandler == null)
            {
                throw new Exception("Command handler is null. Contact service provider.");
            }

            commandHandler.HandleCommand(args);
        }
    }
}
