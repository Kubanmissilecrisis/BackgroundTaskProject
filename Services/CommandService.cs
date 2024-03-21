using System.Collections.Concurrent;
using WebCheckerAPI.EntityFrameworkStuff;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using WebCheckerAPI.DataModels;
using WebCheckerAPI.Models;


namespace WebCheckerAPI.BackgroundTasks;

public class CommandService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;



    public CommandService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = Task.Run(() => ExecuteAsync(cancellationToken), cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(" Stop is Working");
        return Task.CompletedTask;
    }
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<RequestDbContext>();


            var commandObjects = await db.Endpoints.ToListAsync(cancellationToken);
            // if (commandObjects == null)
            // {
            //     Console.WriteLine("Database table is empty");

            // }

            foreach (var commandObject in commandObjects)
            {
                var response = await RequestAsync(commandObject.Url);
                if (response.IsSuccessStatusCode)
                {
                    var result = new WebCheckerAPI.Models.EndpointResult();

                    result.EndpointResultId = Guid.NewGuid();
                    result.EndpointId = commandObject.EndpointId;

                    result.CreatedTime = DateTime.UtcNow;
                    result.State = response.StatusCode.ToString(); ;
                    await db.EndpointResults.AddAsync(result, cancellationToken);
                }
                Console.WriteLine(response.StatusCode);
            }
            await db.SaveChangesAsync(cancellationToken);
        }


    }
    private async Task<HttpResponseMessage> RequestAsync(string? url)
    {
        if (url == null)
        {
            return new HttpResponseMessage();
        }
        // Creating  a named client 
        HttpClient client = new HttpClient();

        // HTTP GET request
        HttpResponseMessage response = await client.GetAsync(url);
        return response;

    }
}













