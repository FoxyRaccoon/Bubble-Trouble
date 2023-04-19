using Godot;
using System;

public delegate void OutOfScreenHandler();
public partial class Splash : FloatingBody
{
    private bool Impulse = true;
    private const float WaterSpeed = 600.0f;
    private const float GrabSpeed = 600.0f;
    private PlayerData SplashData = new PlayerData();
    private Vector2 GrabPoint = new Vector2(0, 0);
    public event OutOfScreenHandler OutOfScreen;
    public override async void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
        bool swimming = Input.IsActionPressed("swimming");
        bool dash = Input.IsActionPressed("dash");
        if(dash){
            velocity = Vector2.FromAngle(Rotation) * WaterSpeed;
            GetNode<AnimationPlayer>("AnimationPlayer").Play("move");
            GetNode<GpuParticles2D>("InkParticles").Emitting = true;
        }else{
            if(Input.IsActionPressed("grab")){
                if(GrabPoint != Vector2.Zero){
                    if((GrabPoint - GlobalPosition).Length() > 10){
                        Vector2 direction = (GrabPoint - GlobalPosition).Normalized();
                        velocity = direction * GrabSpeed;
                    }else{
                        velocity = Vector2.Zero;
                    }
                    
                }
            }else{
                if(IsInWater()){
                    velocity.Y = Mathf.MoveToward(velocity.Y, 0, WaterFriction);
                    velocity.X = Mathf.MoveToward(velocity.X, 0, WaterFriction);
                    if(Impulse && swimming){
                        velocity += Vector2.FromAngle(Rotation) * WaterSpeed;
                        GetNode<AnimationPlayer>("AnimationPlayer").Play("move");
                        
                        Impulse = false;
                        GetNode<Timer>("Timer").Start();
                    }
                }else{
                    velocity.Y += gravity * (float)delta;
                    velocity.X = Mathf.MoveToward(velocity.X, 0, AirFriction);
                }
            }
            GetNode<GpuParticles2D>("InkParticles").Emitting = false;
        }
        
		Velocity = velocity;
        if(Velocity == Vector2.Zero || dash){
            Rotation = Mathf.LerpAngle(Rotation, (GetGlobalMousePosition() - GlobalPosition).Normalized().Angle(), 0.1f);
        }else{
            Rotation = Mathf.LerpAngle(Rotation, Velocity.Angle(), 0.1f);
        }
        

		MoveAndSlide();

        if(Input.IsActionJustPressed("grab")){
            GrabPoint = new Vector2(0, 0);
            GetNode<AnimationPlayer>("ActionAnimationPlayer").Play("grab");
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
        if(body is Collectible){
            Collectible collectible = (Collectible)body;
            collectible.Grab(this);
        }else{
            GrabPoint = GetNode<Area2D>("Tongue").GlobalPosition;
        }
        GetNode<AnimationPlayer>("ActionAnimationPlayer").PlayBackwards("grab");
    }

    public void FinishGrab(){
        GetNode<AnimationPlayer>("ActionAnimationPlayer").PlayBackwards("grab");
    }

    public void _OnCollectionAreaBodyEntered(Node2D body){
        if(body is Collectible){
            Collectible collectible = (Collectible)body;
            collectible.Drop(this);
        }
    }

    public PlayerData GetSplashData(){
        return SplashData;
    }
}
