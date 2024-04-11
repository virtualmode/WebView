namespace WebView.Circuits;

public class CircuitStateChangeEventArgs : EventArgs
{
    public CircuitState State { get; set; }

    public CircuitClient Client { get; set; } = null!;
}
