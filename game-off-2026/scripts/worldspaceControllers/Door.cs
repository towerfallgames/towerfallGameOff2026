using Godot;


public partial class Door : StaticBody2D
{
	[Export]
	public WorldState WorldState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldState.GateUnlockedSignal += (bool gateUnlocked) => UnlockGate(gateUnlocked);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void UnlockGate(bool pressureplateTriggered)
	{
		
	}
}
