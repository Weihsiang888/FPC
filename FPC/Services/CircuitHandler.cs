using Microsoft.AspNetCore.Components.Server.Circuits;

namespace FPC.Services
{
    public class CustomCircuitHandler : CircuitHandler
    {
        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            //Console.WriteLine("Browser disconnected!");
            return Task.CompletedTask;
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            //Console.WriteLine("Circuit closed!");
            return Task.CompletedTask;
        }
    }
}
