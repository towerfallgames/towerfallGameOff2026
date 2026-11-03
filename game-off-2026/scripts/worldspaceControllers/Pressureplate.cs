using Godot;

public partial class Pressureplate : Area2D
{
    [Export]
    public PlaneController PlaneController;

    private bool _temporaryPressurePlateActivated;

    [Signal]
    public delegate void PressurePlateActivatedSignalEventHandler();

    public override void _Ready()
    {
        PlaneController.PlaneChangeSignal += (PlaneController.Planes plane) => IsBodyOverlapping(plane);
    }


    public override void _PhysicsProcess(double delta)
    {
        if (_temporaryPressurePlateActivated)
        {
            EmitSignal(SignalName.PressurePlateActivatedSignal);
        }
    }


    public void OnBodyEntered(Node2D body)
    {
        if (PlaneController.CurrentPlane == PlaneController.Planes.Echo)
        {
            ActivatePressurePlate();
        }
    }

    public void IsBodyOverlapping(PlaneController.Planes plane)
    {
        if (OverlapsBody(PlaneController.Player) && plane == PlaneController.Planes.Echo)
        {
            ActivatePressurePlate();
        }
    }

    private void ActivatePressurePlate()
    {
        EmitSignal(SignalName.PressurePlateActivatedSignal);
        GD.Print("PlayerBody Detected");
        SetCollisionMaskValue(4, false);
        _temporaryPressurePlateActivated = true; 
    }
}
