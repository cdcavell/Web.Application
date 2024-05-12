
using ClassLibrary.Mvc.Extensions;

namespace Web.Application.Services
{
    public sealed class TimedHostedService : IHostedService, IDisposable
    {
        const string _logMessageHeader = "[Hosted Service]: TimedHostedService";

        private readonly ILogger<TimedHostedService> _logger;

        private Timer? _timer = null;
        private bool serviceRunning = false;
 
        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
        }

        public bool ServiceRunning { get { return serviceRunning; } }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{@logMessageHeader} [Action]: StartAsync - Timed Hosted Service running.", _logMessageHeader);
            if (!serviceRunning)
            {
                if (_timer == null)
                    _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
                else
                    _timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(5));
                
                serviceRunning = true;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{@logMessageHeader} [Action]: StopAsync -Timed Hosted Service is stopping.", _logMessageHeader);
            _timer?.Change(Timeout.Infinite, 0);

            serviceRunning = false;

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _logger.LogInformation("{@logMessageHeader} [Action]: Dispose - Timed Hosted Service is disposed.", _logMessageHeader);
            _timer?.Dispose();
        }

        private void DoWork(object? state)
        {
            _logger.LogInformation("{@logMessageHeader} [Action]: DoWork - Timed Hosted Service is working.", _logMessageHeader);
        }
    }
}
