using Godot;
using System;

public delegate void OutOfScreenHandler();
public partial class Splash : FloatingBody
{
    private bool Impulse = true;
    private const float WaterSpeed = 600.0f;
    private PlayerData SplashData = new PlayerData();
    public event OutOfScreenHandler OutOfScreen;
    public override async void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Vector2 direction = Input.GetVector("left", "right", "up", "down");
        bool diving = Input.IsActionPressed("dash");
        if(IsInWater()){
            velocity.Y = Mathf.MoveToward(velocity.Y, 0, WaterFriction);
            velocity.X = Mathf.MoveToward(velocity.X, 0, WaterFriction);
            if(Impulse && diving){
                velocity += Vector2.FromAngle(Rotation) * WaterSpeed;
                GetNode<AnimationPlayer>("AnimationPlayer").Play("move");
                
                Impulse = false;
                GetNode<Timer>("Timer").Start();
            }
        }else{
            velocity.Y += gravity * (float)delta;
            velocity.X = Mathf.MoveToward(velocity.X, 0, AirFriction);
        }
		Velocity = velocity;
        Rotation = Mathf.LerpAngle(Rotation, (GetGlobalMousePosition() - GlobalPosition).Normalized().Angle(), 0.1f);

		MoveAndSlide();

        if(Input.IsActionJustPressed("grab")){
            GetNode<AnimationPlayer>("AnimationPlayer").Play("grab");
        }

        SplashData.ComputeOxygen((float)delta, IsInWater());
        if(!SplashData.IsPlayerAlive()){
            Die();
        }
	}

    public void _OnTimerTimeout()
    {
        Impulse = true;
    }

    public void Die(){
        GD.Print("You died!");
    }

    public void _OnScreenExited()
    {
        OutOfScreen?.Invoke();
    }

    public void _OnGrab(Node2D body){
        GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("grab");
    }

    public void FinishGrab(){
        GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("grab");
    }
}
