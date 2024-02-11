
namespace ApartmentManagementSystem.API.BackgroundServices
{
    public class ApartmentManagementEmailSenderBackgroundService: BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
