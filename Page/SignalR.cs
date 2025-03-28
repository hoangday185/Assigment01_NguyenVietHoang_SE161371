using Microsoft.AspNetCore.SignalR;

namespace Assigment01_NguyenVietHoang_SE161371
{
    public class SignalR : Hub
    {
        private readonly ILogger<SignalR> _logger;

        public SignalR(ILogger<SignalR> logger)
        {
            _logger = logger;
        }

        public async Task TestMessage()
        {
            await Clients.All.SendAsync("TestEvent", "Hello from the server!");
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("A client connected with connection ID: {ConnectionId}", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("A client disconnected with connection ID: {ConnectionId}", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
