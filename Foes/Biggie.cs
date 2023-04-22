using Godot;
using System;

public partial class Biggie : Node2D
{
    private static RandomNumberGenerator RNG = new RandomNumberGenerator();
    private int Step = 0;

    public override void _Ready()
    {
        GetNode<CollisionShape2D>("Area2D2/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<CollisionShape2D>("Area2D3/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<CollisionShape2D>("Area2D4/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<World>("/root/World").OnDay += _OnDayEntering;
        GetNode<World>("/root/World").OnNight += _OnNightEntering;
    }
    public void _OnTimerTimeout()
    {
        if(GetNode<Splash>("/root/World/Splash").IsInWater() && !GetNode<Splash>("/root/World/Splash").IsProtected()){
            GlobalPosition = GetNode<Splash>("/root/World/Splash").GlobalPosition;
            GetNode<AnimationPlayer>("AnimationPlayer").Play("appear");
        }
    }

    public void _OnBodyEntered(Node body)
    {
        if(body is Splash){
            ((Splash)body).Die();
        }
    }

    public void SetRandomPositions(){
        if(!GetNode<World>("/root/World").IsDay()){
            Step = 0;
            GetNode<CollisionShape2D>("Area2D2/CollisionShape2D").GlobalPosition = new Vector2(0, RNG.RandfRange(-1600, 1800));
            GetNode<CollisionShape2D>("Area2D3/CollisionShape2D").GlobalPosition = new Vector2(0, RNG.RandfRange(-1600, 1800));
            GetNode<CollisionShape2D>("Area2D4/CollisionShape2D").GlobalPosition = new Vector2(0, RNG.RandfRange(-1600, 1800));
            GD.Print(GetNode<CollisionShape2D>("Area2D2/CollisionShape2D").GlobalPosition);
            GetNode<CollisionShape2D>("Area2D2/CollisionShape2D").SetDeferred("disabled", false);
            GetNode<CollisionShape2D>("Area2D3/CollisionShape2D").SetDeferred("disabled", false);
            GetNode<CollisionShape2D>("Area2D4/CollisionShape2D").SetDeferred("disabled", false);
        }
    }

    public void _OnDayEntering(){
        GetNode<CollisionShape2D>("Area2D2/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<CollisionShape2D>("Area2D3/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<CollisionShape2D>("Area2D4/CollisionShape2D").SetDeferred("disabled", true);
    }

    public void _OnNightEntering(){
        SetRandomPositions();
        GD.Print("Biggie is ready to appear");
    }

    public void StepOn(){
        Step++;
        if(Step == 3){
            GetNode<Timer>("Timer").Start();
        }
    }
}
