using Godot;
using System;

public partial class BiggieDetection : Area2D
{
    public void _OnBiggieDetect(Node2D body)
    {
        if(body is Splash){
            GetParent<Biggie>().StepOn();
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
            GD.Print("Biggie detected splash");
        }
    }
}
