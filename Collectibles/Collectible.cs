using Godot;
using System;

public partial class Collectible : RigidBody2D
{
    private Stack Item;
    public void Grab(Splash player){
        LinearVelocity = (player.GlobalPosition - GlobalPosition)*4f;
    }

    public void Drop(Splash player){
        player.GetSplashData().GetInventory().AddStack(Item);
        QueueFree();
    }

    public void SetItem(Stack item){
        Item = item;
    }

}
