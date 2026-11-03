using Godot;

public partial class Pressureplate : Area2D
{
    [Export]
    public PlaneController PlaneController;

    [Signal]
    public delegate void PressurePlateActivatedSignalEventHandler();
    
    public void OnBodyEntered(Node2D body)
    {
        if (PlaneController.CurrentPlane == PlaneController.Planes.Echo)
        {
            EmitSignal(SignalName.PressurePlateActivatedSignal);
            GD.Print("PlayerBody Detected");
            SetCollisionMaskValue(4, false);          
        }
    }
}
