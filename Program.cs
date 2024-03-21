using Microsoft.EntityFrameworkCore;
using WebCheckerAPI.BackgroundTasks;
using WebCheckerAPI.EntityFrameworkStuff;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
builder.Services.AddDbContext<RequestDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("RequestStoreManagment"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:8080").AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddHttpClient();
//adding background Tasks as a service

// builder.Services.AddScoped<BackgroundService, CommandPerformer>(
// );

builder.Services.AddSingleton<IHostedService, CommandService>();

// builder.Services.AddHostedService<CommandPerformer>((serviceProvider) =>
// {


//     // Create a scope to resolve scoped services
//     using var scope = serviceProvider.CreateScope();
//     // var connectionString = builder.Configuration.GetConnectionString("RequestStoreManagment");

//     var service1 = scope.ServiceProvider.GetRequiredService<IUrlMonitoringService>();
//     var service2 = scope.ServiceProvider.GetRequiredService<IUrlMonitoringService2>();
//     var service3 = scope.ServiceProvider.GetRequiredService<IUrlMonitoringService3>();
//     var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

//     // var db = scope.ServiceProvider.GetRequiredService<RequestDbContext>();


//     return new CommandPerformer(serviceScopeFactory);
// });

// builder.Services.AddHostedService<CommandPerformer>(provider =>
// {
//     var connectionString = builder.Configuration.GetConnectionString("RequestStoreManagment");
//     var service1 = provider.GetRequiredService<IUrlMonitoringService>();
//     var service2 = provider.GetRequiredService<IUrlMonitoringService2>();
//     var service3 = provider.GetRequiredService<IUrlMonitoringService3>();
//     var db = provider.GetRequiredService<RequestDbContext>();

//     return new CommandPerformer(connectionString, service1, service2, service3, db);
// });


// builder.Services.AddSingleton<ITaskStoper, CommandPerformer>();
// builder.Services.AddScoped<BackgroundTaskManager>();

builder.Services.AddScoped<IUrlMonitoringService, BackgroundTask1>();
builder.Services.AddScoped<IUrlMonitoringService2, BackgroundTask2>();
builder.Services.AddScoped<IUrlMonitoringService3, BackgroundTask3>();
// builder.Services.AddScoped<BackgroundTask1>();
// builder.Services.AddScoped<BackgroundTask2>();
// builder.Services.AddScoped<BackgroundTask3>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
