using Microsoft.AspNetCore.Components.Server.Circuits;

namespace WebView.Circuits;

public class CircuitHandlerService : CircuitHandler
{
    public string? CircuitId { get; private set; }

    private readonly ICircuitService _circuitService;

    public CircuitHandlerService(ICircuitService circuitService)
    {
        _circuitService = circuitService;
    }

    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        CircuitId = circuit.Id;
        _circuitService.AddOrUpdate(new CircuitClient()
        {
            Id = circuit.Id,
            UserId = string.Empty,
        });
        return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }

    public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        _circuitService.Remove(circuit.Id);
        return base.OnCircuitClosedAsync(circuit, cancellationToken);
    }

    public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        return base.OnConnectionUpAsync(circuit, cancellationToken);
    }

    public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        return base.OnConnectionDownAsync(circuit, cancellationToken);
    }
}
