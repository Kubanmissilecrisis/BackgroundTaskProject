using WebCheckerAPI.EntityFrameworkStuff;
using WebCheckerAPI.DataModels;
namespace WebCheckerAPI.BackgroundTasks;

public class BackgroundTask3 : BackgroundService, IUrlMonitoringService3
{
    private string _url;
    private int _time;
    private int cardId;
    private readonly ILogger<BackgroundTask1> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private CancellationTokenSource _cts;
    private readonly RequestDbContext db;


    public BackgroundTask3(ILogger<BackgroundTask1> logger, IHttpClientFactory httpClientFactory, RequestDbContext db)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        this.db = db;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

        while (!_cts.IsCancellationRequested)
        {
            try
            {
                // Creating  a named client 
                HttpClient client = _httpClientFactory.CreateClient();

                // HTTP GET request
                HttpResponseMessage response = await client.GetAsync(_url);


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
            // I can also use Timer to triger a Dowork() but this voids the overhead of managing a timer.
            await Task.Delay(TimeSpan.FromSeconds(_time), _cts.Token);
        }
    }

    private async Task SaveRequestAsync(string url, int cardId, DateTime date, string status)
    {
        try
        {

            var request = new RequestModel
            {
                CardNumber = cardId,
                Date = date,
                Status = status
            };

            await db.Request.AddAsync(request);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving request for {url}");
        }
    }

    public async Task StartMonitoringAsync(string url, DateTime Date, int intervalInSeconds, int taskId)
    {
        _time = intervalInSeconds;
        _url = url;
        cardId = taskId;
        if (taskId != 3)
        {
            throw new ArgumentOutOfRangeException(nameof(taskId), "Invalid taskId for this service.");
        }

        _cts.Cancel(); // Stop any previous monitoring loop
        _cts = CancellationTokenSource.CreateLinkedTokenSource();
        await ExecuteAsync(_cts.Token); // Resume monitoring
    }

    public Task StopMonitoringAsync(int taskId)
    {
        if (taskId != 3)
        {
            throw new ArgumentOutOfRangeException(nameof(taskId), "Invalid taskId for this service.");
        }

        _cts.Cancel();
        return Task.CompletedTask;
    }
}