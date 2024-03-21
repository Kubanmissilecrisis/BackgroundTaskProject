using WebCheckerAPI.EntityFrameworkStuff;
using WebCheckerAPI.DataModels;
namespace WebCheckerAPI.BackgroundTasks;

public class BackgroundTask1 : BackgroundService, IUrlMonitoringService
{
    private string _url;
    private int _time;
    private int cardId;
    private readonly ILogger<BackgroundTask1> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    // private CancellationTokenSource _cts;
    private CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly RequestDbContext db;
    // private bool startTask = false;


    public BackgroundTask1(ILogger<BackgroundTask1> logger, IHttpClientFactory httpClientFactory, RequestDbContext db)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        this.db = db;
        // _cancellationToken = cancellationToken;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        // _cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {

                // Creating  a named client 
                HttpClient client = _httpClientFactory.CreateClient();

                Console.WriteLine("task 1 before check");
                // HTTP GET request
                HttpResponseMessage response = await client.GetAsync(_url);

                Console.WriteLine("task 1 after check");
                if (response.IsSuccessStatusCode)
                {
                    // var request = new RequestModel
                    // {
                    //     CardNumber = cardId, 
                    //     Date = new DateTime(),
                    //     Status = "OK"
                    // };

                    // db.Request.Add(request);
                    // db.SaveChanges();
                    await SaveRequestAsync(_url, cardId, DateTime.UtcNow, "OK");
                    _logger.LogInformation($"{_url} status was stored as reachable (Status: {response.StatusCode})");
                }
                else
                {
                    await SaveRequestAsync(_url, cardId, DateTime.UtcNow, "Failed");
                    _logger.LogError($"{_url} status was stored as not reachable (Status: {response.StatusCode})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking {_url}");
            }
            // I can also use Timer to triger a Dowork() but "Delay" avoids the overhead of managing a timer.
            await Task.Delay(TimeSpan.FromSeconds(_time), _cts.Token);
        }
    }

    private async Task SaveRequestAsync(string url, int cardId, DateTime date, string status)
    {
        try
        {

            var request = new RequestStatus
            {
                CardNumber = cardId,
                Date = date,
                Url = url,
                Status = status
            };

            await db.RequestResults.AddAsync(request);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving checkup result for {url}");
        }
    }

    public async Task StartMonitoringAsync(string Url, DateTime Date, int intervalInSeconds, int TaskId)
    {

        _time = intervalInSeconds;
        _url = Url;
        cardId = TaskId;
        if (TaskId != 1)
        {
            throw new ArgumentOutOfRangeException(nameof(TaskId), "Invalid cardId for this service.");
        }
        // _cts = CancellationTokenSource.CreateLinkedTokenSource();
        // _cts.Cancel(); // Stop any previous monitoring loop

        _cts?.Cancel(); // Cancel any previous monitoring loop
        _cts = new CancellationTokenSource();

        await ExecuteAsync(_cts.Token); // Resume monitoring
    }

    public Task StopMonitoringAsync(int taskId)
    {
        Console.WriteLine("Stop Task is working");
        if (taskId != 1)
        {
            throw new ArgumentOutOfRangeException(nameof(taskId), "Invalid cardId for this service.");
        }


        _cts.Cancel();
        return Task.CompletedTask;
    }
}