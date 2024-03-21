// using System.Collections.Concurrent;
// using WebCheckerAPI.EntityFrameworkStuff;
// using Npgsql;
// using Microsoft.EntityFrameworkCore;
// namespace WebCheckerAPI.BackgroundTasks;
// public interface ITaskStoper
// {
//     // void notifyDataChange(string payload, CancellationToken stoppingToken);
//     // Task StartTaskAsync(string Url, DateTime Date, int TimeAmount, string TimeType, int TaskId);
//     Task StopTaskAsync(int TaskId);
// }

// public class CommandPerformer : IHostedService, ITaskStoper
// {
//     private readonly IUrlMonitoringService _service1;
//     private readonly IUrlMonitoringService2 _service2;
//     private readonly IUrlMonitoringService3 _service3;
//     private readonly ConcurrentDictionary<int, bool> _runningTasks = new ConcurrentDictionary<int, bool>();
//     private readonly RequestDbContext db;

//     private readonly IServiceScopeFactory _serviceScopeFactory;

//     private readonly string _connectionString;

//     public CommandPerformer(IServiceScopeFactory serviceScopeFactory)
//     {

//         _serviceScopeFactory = serviceScopeFactory;

//     }

//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         using (IServiceScope scope = _serviceScopeFactory.CreateScope())
//         {
//             var serviceProvider = scope.ServiceProvider;

//             // var service1 = serviceProvider.GetRequiredService<IUrlMonitoringService>();
//             // var service2 = serviceProvider.GetRequiredService<IUrlMonitoringService2>();
//             // var service3 = serviceProvider.GetRequiredService<IUrlMonitoringService3>();
//             var db = serviceProvider.GetRequiredService<RequestDbContext>();

//             Console.WriteLine("CommandPerformer is working");

//             //esteblishinng a connection with database once the app is running
//             using var connection = new NpgsqlConnection("Host=localhost; Database=FlightsDataBase; Username=postgres; Password=Postgre");
//             await connection.OpenAsync();

//             //registering callback method (notifyDataChange):as an event handler for the Notification event of the database connection.
//             connection.Notification += (o, e) => notifyDataChange(e.Payload, stoppingToken);

//             Console.WriteLine("callback method for notification is registered");

//             //using database connection to send a command to create a notification channel 
//             using (var cmd = new NpgsqlCommand("LISTEN command_objects_channel", connection))
//                 await cmd.ExecuteNonQueryAsync(stoppingToken);
//             Console.WriteLine("Registration to notify is esteblished");

//             while (true)
//                 await connection.WaitAsync(stoppingToken);

//         }

//     }

//     // public async void notifyDataChange(string payload, CancellationToken stoppingToken)
//     // {
//     //     Console.WriteLine(payload);
//     //     if (int.TryParse(payload, out int commandId))
//     //     {
//     //         Console.WriteLine($"Received notification for CommandId: {commandId}");

//     //         try
//     //         {
//     //             using (IServiceScope scope = _serviceScopeFactory.CreateScope())
//     //             {
//     //                 var serviceProvider = scope.ServiceProvider;
//     //                 var db = serviceProvider.GetRequiredService<RequestDbContext>();
//     //                 // Contacting CommandObjects table to get the newly added command object
//     //                 var latestCommand = db.CommandObjects.Find(commandId);

//     //                 if (latestCommand != null)
//     //                 {
//     //                     Console.WriteLine($"new Command to perform: {latestCommand}");

//     //                     // Triggering particular task based on new Command received from Database
//     //                     await StartTaskAsync(latestCommand.Url, latestCommand.Date, latestCommand.TimeAmount, latestCommand.TimeType, latestCommand.CardID);
//     //                 }
//     //                 else
//     //                 {
//     //                     Console.WriteLine($"Command with CommandId {commandId} not found in the database.");
//     //                 }
//     //             }

//     //             // Contacting CommandObjects table to get the newly added command object
//     //             var latestCommand = await db.CommandObjects.FindAsync(commandId);

//     //             if (latestCommand != null)
//     //             {
//     //                 Console.WriteLine($"new Command to perform: {latestCommand}");

//     //                 // Triggering particular task based on new Command received from Database
//     //                 await StartTaskAsync(latestCommand.Url, latestCommand.Date, latestCommand.TimeAmount, latestCommand.TimeType, latestCommand.CardID);
//     //             }
//     //             else
//     //             {
//     //                 Console.WriteLine($"Command with CommandId {commandId} not found in the database.");
//     //             }
//     //         }
//     //         catch (Exception ex)
//     //         {
//     //             Console.WriteLine($"Error when trying to trigger the BackgroundTaskManager: {ex.Message}");
//     //         }
//     //     }
//     //     else
//     //     {
//     //         Console.WriteLine($"Error converting payload to int: {payload}");
//     //     }
//     // }



//     public async Task StartTaskAsync(string Url, DateTime Date, int TimeAmount, string TimeType, int TaskId)
//     {
//         Console.WriteLine("StartTask Is working");
//         if (!_runningTasks.TryAdd(TaskId, true))
//         {
//             // throw new InvalidOperationException("Service of this card " + TaskId + " is already running.");
//             Console.WriteLine($"Error : already running task ");
//         }

//         else
//         {
//             int intervalInSeconds = TimeType switch
//             {
//                 "second" => TimeAmount,
//                 "minute" => TimeAmount * 60,
//                 "hour" => TimeAmount * 60 * 60,
//                 _ => throw new ArgumentOutOfRangeException(nameof(TimeType), "Invalid time type.")
//             };

//             // using (IServiceScope scope = _serviceScopeFactory.CreateScope())
//             // {
//             //     var serviceProvider = scope.ServiceProvider;

//             //     var service1 = serviceProvider.GetRequiredService<IUrlMonitoringService>();
//             //     var service2 = serviceProvider.GetRequiredService<IUrlMonitoringService2>();
//             //     var service3 = serviceProvider.GetRequiredService<IUrlMonitoringService3>();

//             switch (TaskId)
//             {
//                 case 1:
//                     await _service1.StartMonitoringAsync(Url, Date, intervalInSeconds, TaskId);
//                     break;
//                 case 2:
//                     await _service2.StartMonitoringAsync(Url, Date, intervalInSeconds, TaskId);
//                     break;
//                 case 3:
//                     await _service3.StartMonitoringAsync(Url, Date, intervalInSeconds, TaskId);
//                     break;
//                 default:
//                     throw new ArgumentOutOfRangeException(nameof(TaskId));
//             }
//             // }
//         }
//     }

//     public async Task StopTaskAsync(int TaskId)
//     {
//         switch (TaskId)
//         {
//             case 1:
//                 await _service1.StopMonitoringAsync(TaskId);
//                 break;
//             case 2:
//                 await _service2.StopMonitoringAsync(TaskId);
//                 break;
//             case 3:
//                 await _service3.StopMonitoringAsync(TaskId);
//                 break;
//             default:
//                 throw new ArgumentOutOfRangeException(nameof(TaskId));
//         }
//         _runningTasks.TryRemove(TaskId, out _); // Remove regardless of success/failure
//         // }                                     // return Task.CompletedTask;
//     }

//     public Task StartAsync(CancellationToken cancellationToken)
//     {
//         throw new NotImplementedException();
//     }

//     public Task StopAsync(CancellationToken cancellationToken)
//     {
//         throw new NotImplementedException();
//     }
// }




