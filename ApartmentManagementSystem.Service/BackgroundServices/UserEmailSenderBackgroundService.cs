using ApartmentManagementSystem.Repository.Models;
using Microsoft.AspNetCore.Identity;

namespace ApartmentManagementSystem.API.BackgroundServices
{
    public class UserEmailSenderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }


        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                    var users = userManager.Users.ToList();

                }
                await Task.Delay(1000 * 60 * 60 * 12, stoppingToken);
            }
        }
    }
}