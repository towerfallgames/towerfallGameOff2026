using System;
using System.ComponentModel;
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
    public delegate Planes PlaneChangeSignalEventHandler(Planes plane);

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

        EchoPlane.Visible = false;
        MaterialPlane.Visible = true;
        CurrentPlane = Planes.Material;
        Player.SetCollisionMaskValue(2, true);
        Player.SetCollisionMaskValue(3, false);
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
                CurrentPlane = Planes.Echo;
                EchoPlane.Visible = true;
                MaterialPlane.Visible = false;
                Player.SetCollisionMaskValue(2, false);
                Player.SetCollisionMaskValue(3, true);

                GD.Print($"Changed Planes to {CurrentPlane}");
                return;
            }
            CurrentPlane = Planes.Material;
            EchoPlane.Visible = false;
            MaterialPlane.Visible = true;
            Player.SetCollisionMaskValue(2, true);
            Player.SetCollisionMaskValue(3, false);

            GD.Print($"Changed Planes to {CurrentPlane}");
        }
    }
}
