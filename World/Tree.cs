using Godot;
using System;

public partial class Tree : StaticBody2D
{
    private static PackedScene FruitScene = GD.Load<PackedScene>("res://Collectibles/fruit.tscn");
    private static RandomNumberGenerator RNG = new RandomNumberGenerator();


    public override void _Ready()
    {
        GetNode<WorldTick>("/root/WorldTick").Timeout += _OnWorldTickTimeout;
    }

    private void FruitSpawn(){
        RayCast2D RandomRayCast = GetNode<Node2D>("RayCastGroup").GetChild<RayCast2D>(RNG.RandiRange(0, 6));
        RandomRayCast.Position = new Vector2(RNG.RandfRange(-400f, 400f), RandomRayCast.Position.Y);
        if(RandomRayCast.IsColliding()){
            var fruit = (Fruit)FruitScene.Instantiate();
            fruit.GlobalPosition = RandomRayCast.GetCollisionPoint();
            GetParent().GetNode("Collectibles").AddChild(fruit);
        }
    }

    public void _OnWorldTickTimeout()
    {
        for(int i = 0; i < 10; i++){
            if(RNG.Randf() < 0.6f){
                FruitSpawn();
            }
        }
    }
}
