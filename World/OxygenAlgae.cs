using Godot;
using System;

public partial class OxygenAlgae : Node2D
{
    private static PackedScene OxygenScene = GD.Load<PackedScene>("res://Collectibles/oxygen.tscn");
    private static RandomNumberGenerator RNG = new RandomNumberGenerator();

    public override void _Ready()
    {
        GetNode<WorldTick>("/root/WorldTick").Timeout += _OnWorldTickTimeout;
    }
    public void _OnWorldTickTimeout()
    {
        for(int i = 0; i < 4; i++){
            if(RNG.Randf() < 0.4f){
                var oxygen = (Oxygen)OxygenScene.Instantiate();
                oxygen.GlobalPosition = GlobalPosition + new Vector2(RNG.RandfRange(-16, 16), RNG.RandfRange(-32, -16));
                GetParent().AddChild(oxygen);
            }
        }
        
    }
}
