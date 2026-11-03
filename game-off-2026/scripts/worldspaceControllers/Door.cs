using Godot;


public partial class Door : StaticBody2D
{
	[Export]
	public WorldState WorldState;
	[Export]
	public CollisionShape2D CollisionShape;
	[Export]
	public Sprite2D DoorSprite;

	private const string _openDoorSpritePath = "res://assets/manipulatable/PlaceholderOpenDoor.png";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldState.GateUnlockedSignal += (bool gateUnlocked) => UnlockGate(gateUnlocked);
	}

	private void UnlockGate(bool pressureplateTriggered)
	{
		GD.Print("UnlockGate signal: RECEIVED");
		DoorSprite.Texture = GD.Load<Texture2D>(_openDoorSpritePath);
		CollisionShape.QueueFree();
	}
}
