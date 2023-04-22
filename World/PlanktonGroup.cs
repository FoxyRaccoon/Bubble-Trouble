using Godot;
using System;

public partial class PlanktonGroup : Node2D
{
    private static PackedScene PlanktonScene = GD.Load<PackedScene>("res://World/plankton.tscn");
    private static RandomNumberGenerator RNG = new RandomNumberGenerator();

    public override void _Ready()
    {
        for(int i = 0; i < RNG.RandiRange(10,40); i++){
            Polygon2D plankton = (Polygon2D)PlanktonScene.Instantiate();
            plankton.GlobalPosition = GlobalPosition + new Vector2(RNG.RandfRange(-32, 32), RNG.RandfRange(-32, 32));
            AddChild(plankton);
        }
    }

}
