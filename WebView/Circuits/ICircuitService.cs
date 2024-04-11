namespace WebView.Circuits;

public interface ICircuitService
{
    event EventHandler<CircuitStateChangeEventArgs> CircuitStateChanged;

    Dictionary<string, CircuitClient> Clients { get; }

    public int ClientCount { get; }

    void AddOrUpdate(CircuitClient client);
    CircuitClient? Remove(string circuitId);
}
