using System;
using Godot;

public partial class PlaneController : Node
{
    //Variables assigned in engine.
    [Export]
    public Node2D MaterialPlane;
    [Export]
    public Node2D EchoPlane;
    [Export]
    public CharacterBody2D Player;

    //Signals
    [Signal]
    public delegate void PlaneChangeSignalEventHandler(Planes plane);

    //Enums
    public enum Planes
    {
        Material,
        Echo
    }

    // Public Variables Assigned in Code
    public Planes CurrentPlane;

    public override void _Ready()
    {
        if (MaterialPlane == null){throw new NullReferenceException();}
        if (EchoPlane == null){throw new NullReferenceException();}
        if (Player == null){throw new NullReferenceException();}

        CurrentPlane = Planes.Material;
        ApplyDecision(CurrentPlane);
    }

    public override void _Process(double delta)
    {
        ShiftPlanes();
    }

    private void ShiftPlanes()
    {
        //Listen for input and then swap the plane
        if (Input.IsActionJustPressed("shift_plane"))
        {
            GD.Print("Shift pressed.");
            if (CurrentPlane == Planes.Material)
            {
                ApplyDecision(CurrentPlane);
            }
            ApplyDecision(CurrentPlane);
        }
    }

    private void ApplyDecision(Planes destination)
    {
        switch (destination)
        {
            case Planes.Echo:
                CurrentPlane = Planes.Echo;
                EchoPlane.Visible = true;
                MaterialPlane.Visible = false;
                Player.SetCollisionMaskValue(2, false);
                Player.SetCollisionMaskValue(3, true);
                break;

            case Planes.Material:
                CurrentPlane = Planes.Material;
                EchoPlane.Visible = false;
                MaterialPlane.Visible = true;
                Player.SetCollisionMaskValue(2, true);
                Player.SetCollisionMaskValue(3, false);
                break;
        }

        EmitSignal(SignalName.PlaneChangeSignal, (int)CurrentPlane);
    }
}
