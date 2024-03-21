namespace WebCheckerAPI.BackgroundTasks;

public interface IUrlMonitoringService
{
    Task StartMonitoringAsync(string Url, DateTime Date, int intervalInSeconds, int TaskId);
    Task StopMonitoringAsync(int TaskId);
}
public interface IUrlMonitoringService2
{
    Task StartMonitoringAsync(string Url, DateTime Date, int intervalInSeconds, int TaskId);
    Task StopMonitoringAsync(int TaskId);
}
public interface IUrlMonitoringService3
{
    Task StartMonitoringAsync(string Url, DateTime Date, int intervalInSeconds, int TaskId);
    Task StopMonitoringAsync(int TaskId);
}
