// using System.Collections.Concurrent;
// using WebCheckerAPI.EntityFrameworkStuff;
// using WebCheckerAPI.DataModels;

// namespace WebCheckerAPI.BackgroundTasks;

// public class BackgroundTaskManager
// {
//     private readonly IUrlMonitoringService _service1;
//     private readonly IUrlMonitoringService2 _service2;
//     private readonly IUrlMonitoringService3 _service3;
//     private readonly RequestDbContext db;

//     private readonly ConcurrentDictionary<int, bool> _runningTasks = new ConcurrentDictionary<int, bool>();

//     public BackgroundTaskManager(IUrlMonitoringService service1, IUrlMonitoringService2 service2, IUrlMonitoringService3 service3, RequestDbContext db)
//     {
//         _service1 = service1;
//         _service2 = service2;
//         _service3 = service3;
//         this.db = db;
//     }
//     public async void notifyReceiver(string commandId)
//     {
//         try
//         {
//             // Contacting CommandObjects table to get the newly added command object
//             var latestCommand = db.CommandObjects.Find(commandId);

//             if (latestCommand != null)
//             {
//                 Console.WriteLine($"new Command to perform: {latestCommand}");

//                 // Triggering particular task based on new Command received from Database
//                 await StartTaskAsync(latestCommand.Url, latestCommand.Date, latestCommand.TimeAmount, latestCommand.TimeType, latestCommand.CardID);
//             }
//             else
//             {
//                 Console.WriteLine($"Command with CommandId {commandId} not found in the database.");
//             }


//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Error retrieving Command object from CommandObjects table: {ex.Message}");
//         }
//     }

//     private async Task StartTaskAsync(string Url, DateTime Date, int TimeAmount, string TimeType, int TaskId)
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

// }
