using System;
using Godot;

public partial class WorldState : Node
{
    [Signal]
    public delegate void GateUnlockedSignalEventHandler(bool gateUnlocked);

    public bool GateUnlocked;

    public override void _Ready()
    {
        GateUnlocked = false;
    }

    public void OnPressurePlateBodyEntered()
    {
        GateUnlocked = true;
        EmitSignal(SignalName.GateUnlockedSignal, GateUnlocked);
    }
}
