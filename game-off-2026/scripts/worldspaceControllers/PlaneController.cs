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
    [Export]
    public CollisionShape2D PlayerCollisionShape;

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
        if (PlayerCollisionShape == null){throw new NullReferenceException();};

        CurrentPlane = Planes.Material;
        ApplyDecision(CurrentPlane);
    }

    public override void _PhysicsProcess(double delta)
    {
        ShiftPlanes();
    }

    private void ShiftPlanes()
    {
        Planes destination;
        
        //Listen for input and then swap the plane
        if (Input.IsActionJustPressed("shift_plane"))
        {
            if (CurrentPlane == Planes.Material)
            {
                destination = Planes.Echo;
            }
            else
            {
                destination = Planes.Material;
            }

            bool canShift = CanShiftTo(destination);

            if (canShift)
            {
            ApplyDecision(destination);
            }
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
                EmitSignal(SignalName.PlaneChangeSignal, (int)destination);
                break;

            case Planes.Material:
                CurrentPlane = Planes.Material;
                EchoPlane.Visible = false;
                MaterialPlane.Visible = true;
                Player.SetCollisionMaskValue(2, true);
                Player.SetCollisionMaskValue(3, false);
                EmitSignal(SignalName.PlaneChangeSignal, (int)destination);
                break;
        }

        GD.Print(Player.CollisionMask);
        EmitSignal(SignalName.PlaneChangeSignal, (int)CurrentPlane);
    }

    private bool CanShiftTo(Planes destination)
    {
        var query = new PhysicsShapeQueryParameters2D();

        uint bitmask = destination switch
        {
            Planes.Material => 2,
            Planes.Echo => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(destination))
        };

        query.Shape = PlayerCollisionShape.Shape;
        query.Transform = PlayerCollisionShape.GlobalTransform;
        query.CollisionMask = bitmask;
        query.CollideWithAreas = false;
        query.CollideWithBodies = true;

        var spaceState = Player.GetWorld2D().DirectSpaceState;
        
        var results = spaceState.IntersectShape(query, 1);

        if (results.Count == 0)
        {
            return true;
        }
        
        GD.Print("Cannot shift.");
        return false;
    }
}
