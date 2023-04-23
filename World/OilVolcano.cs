using Godot;
using System;

public partial class OilVolcano : Node2D
{
    private static PackedScene OilScene = GD.Load<PackedScene>("res://Collectibles/oil.tscn");
    private static RandomNumberGenerator RNG = new RandomNumberGenerator();

    public override void _Ready()
    {
        GetNode<WorldTick>("/root/WorldTick").Timeout += _OnWorldTickTimeout;
    }
    public void _OnWorldTickTimeout()
    {
        for(int i = 0; i < 4; i++){
            if(RNG.Randf() < 0.2f){
                var oil = (Oil)OilScene.Instantiate();
                oil.GlobalPosition = new Vector2(RNG.RandfRange(-8, 8), RNG.RandfRange(-16, -8));
                oil.ShowBehindParent = true;
                AddChild(oil);
            }
        }
        GetNode<AnimationPlayer>("AnimationPlayer").Play("push");
        
    }
}
