using Godot;

public partial class WorldState : Node
{
    [Export]
    public Pressureplate PressurePlate;

    [Signal]
    public delegate void GateUnlockedSignalEventHandler(bool gateUnlocked);

    public bool GateUnlocked;

    public override void _Ready()
    {
        GateUnlocked = false;
        PressurePlate.PressurePlateActivatedSignal += SendGateUnlockedSignal;
    }

    public void SendGateUnlockedSignal()
    {
        GD.Print("Pressure Plate activation: RECEIVED");
        GateUnlocked = true;
        EmitSignal(SignalName.GateUnlockedSignal, GateUnlocked);
    }
}
