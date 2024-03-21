// using Microsoft.AspNetCore.Mvc;
// using WebCheckerAPI.Filters;
// // using WebCheckerAPI.BackgroundTasks;
// using WebCheckerAPI.BackgroundTasks;
// using System.ComponentModel.DataAnnotations;
// using WebCheckerAPI.DataModels;
// using WebCheckerAPI.EntityFrameworkStuff;
// namespace WebCheckerAPI.Controllers;

// [ApiController]
// [Route("WebChekerApi/[controller]")]
// public class CardRequestController : ControllerBase
// {
//     private readonly RequestDbContext _db;

//     private readonly IUrlMonitoringService _service1;
//     private readonly IUrlMonitoringService2 _service2;
//     private readonly IUrlMonitoringService3 _service3;
//     // private readonly CommandPerformer _commandPerformer;
//     // private readonly ITaskStoper _taskStoper;
//     // private readonly BackgroundTaskManager _taskManager;
//     private readonly ILogger<BackgroundTask1> _logger;
//     public CardRequestController(ILogger<BackgroundTask1> logger, RequestDbContext db, IUrlMonitoringService service1,
//         IUrlMonitoringService2 service2,
//         // IUrlMonitoringService3 service3, CommandPerformer commandPerformer)
//     {
//         _db = db;
//         // _commandPerformer = commandPerformer;
//         // _taskStoper = taskStoper;
//         _logger = logger;

//         _service1 = service1;
//         _service2 = service2;
//         _service3 = service3;
//         // _taskManager = taskManager;
//     }

//     [HttpPost("add-command")]
//     public IActionResult AddCommand([FromBody] CommandObjectsModel commandObject)
//     {
//         _db.CommandObjects.Add(commandObject);
//         _db.SaveChanges();
//         Console.WriteLine("new command was stored in CommandObjects table");
//         // _logger.LogInformation($"{commandObject} new command was stored in CommandObjects table");
//         return Ok("The new Request Command is successfuly added to the list and soon to be performed");

//     }

//     [HttpGet("request-results")]

//     public IActionResult PullRequest(RequestStatus status)
//     {
//         var totalrequest = _db.RequestResults.Where(x => x.Url == status.Url);

//         return Ok(totalrequest);

//     }

//     [HttpPost("stop-task")]
//     public async Task<IActionResult> StopTask(int taskId)
//     {
//         // await _service1.StopMonitoringAsync(taskId);
//         await _commandPerformer.StopTaskAsync(taskId);
//         // await CommandPerformer.Instance.StopTaskAsync(taskId);
//         //if the task is stope we just delete that task from database
//         // Remove the command from the database
//         var commandToRemove = _db.Commands.Find(taskId);
//         if (commandToRemove != null)
//         {
//             _db.Commands.Remove(commandToRemove);
//             await _db.SaveChangesAsync();
//         }

//         return Ok("Service of a card {request.TaskId} stopped successfully");
//     }

















//     // private readonly BackgroundTaskManager _taskManager;

//     // public CardRequestController(BackgroundTaskManager taskManager)
//     // {

//     //     _taskManager = taskManager;
//     // }

//     // [HttpPost("start-task")]
//     // public async Task<IActionResult> StartTask([FromBody] TaskRequest request)
//     // {
//     //     try
//     //     {
//     //         // return Ok($"Service of a card {request.TaskId} is working");
//     //         await _taskManager.StartTaskAsync(request.Url, request.TaskId, request.TimeAmount, request.TimeType);
//     //         return Ok($"Service of a card {request.TaskId} started successfully");
//     //     }
//     //     catch (Exception ex)
//     //     {
//     //         return BadRequest(ex.Message); // Handle exceptions appropriately
//     //     }
//     // }

//     // [HttpPost("stop-task")]
//     // public async Task<IActionResult> StopTask(int taskId)
//     // {
//     //     await _taskManager.StopTaskAsync(taskId);
//     //     return Ok("Service of a card {request.TaskId} stopped successfully");
//     // }


//     // public class TaskRequest
//     // {
//     //     public string? Url { get; set; }
//     //     public int TaskId { get; set; }
//     //     public int TimeAmount { get; set; }
//     //     public string? TimeType { get; set; }
//     // }
//     // [HttpPost]
//     // [TypeFilter(typeof(NewRequest_ActionFilter))]
//     // public IActionResult AddRequest(RequestModel request)
//     // {
//     //     db.Request.Add(request);
//     //     db.SaveChanges();




//     //     // display the success messsage with details
//     //     return Ok();

//     // }
// }
