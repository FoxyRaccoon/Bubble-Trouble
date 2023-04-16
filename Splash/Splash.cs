using Godot;
using System;

public partial class Splash : FloatingBody
{
    private bool Impulse = true;
    private const float WaterSpeed = 600.0f;
    private PlayerData SplashData = new PlayerData();
    public override async void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
        if(IsInWater()){
            velocity.Y = Mathf.MoveToward(velocity.Y, 0, WaterFriction);
            velocity.X = Mathf.MoveToward(velocity.X, 0, WaterFriction);
            if(direction != Vector2.Zero && Impulse){
                velocity += direction * WaterSpeed;
                Impulse = false;
                GetNode<Timer>("Timer").Start();
            }
        }else{
            velocity.Y += gravity * (float)delta;
            velocity.X = Mathf.MoveToward(velocity.X, 0, AirFriction);
        }
		Velocity = velocity;
		MoveAndSlide();

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
}
