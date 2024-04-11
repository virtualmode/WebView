namespace WebView.Circuits;

public sealed class CircuitService : ICircuitService
{
    private Dictionary<string, CircuitClient> _clients;

    public Dictionary<string, CircuitClient> Clients => GetClients();
    public int ClientCount => GetClientCount();

    public event EventHandler<CircuitStateChangeEventArgs>? CircuitStateChanged;

    public CircuitService()
    {
        _clients = new Dictionary<string, CircuitClient>();
    }

    private Dictionary<string, CircuitClient> GetClients()
    {
        lock (_clients)
        {
            return _clients.ToDictionary(keySelector => keySelector.Key, keySelector => keySelector.Value);
        }
    }

    private int GetClientCount()
    {
        lock (_clients)
        {
            return _clients.Count;
        }
    }

    public void AddOrUpdate(CircuitClient client)
    {
        bool updated;

        lock (_clients)
        {
            updated = _clients.ContainsKey(client.Id);
            _clients[client.Id] = client;
        }

        CircuitStateChanged?.Invoke(this, new CircuitStateChangeEventArgs() { Client = client, State = updated ? CircuitState.Updated : CircuitState.Added });
    }

    public CircuitClient? Remove(string circuitId)
    {
        bool removed;
        CircuitClient? client;

        lock (_clients)
        {
            removed = _clients.Remove(circuitId, out client);
        }

        if (removed && client != null)
        {
            CircuitStateChanged?.Invoke(this, new CircuitStateChangeEventArgs() { Client = client, State = CircuitState.Removed });
            return client;
        }

        return null;
    }
}
