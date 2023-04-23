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
    private Insect Grabbed = null;
    private bool Protected = false;
    public event OutOfScreenHandler OutOfScreen;

    public override void _Ready()
    {
        GetNode<UI>("CanvasLayer/UI").SetPlayerData(SplashData);
    }
    
    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
        bool swimming = Input.IsActionPressed("swimming");
        bool dash = Input.IsActionPressed("dash");

        if(Input.IsActionJustPressed("grab")){
            GrabPoint = new Vector2(0, 0);
            GetNode<AnimationPlayer>("ActionAnimationPlayer").Play("grab");
        }

        if(SplashData.IsPlayerAlive()){
            if(dash && SplashData.UseEnergy(0.5f)){
                velocity = Vector2.FromAngle(Rotation) * WaterSpeed;
                GetNode<AnimationPlayer>("AnimationPlayer").Play("move");
                GetNode<GpuParticles2D>("InkParticles").Emitting = true;
                Grabbed = null;
            }else{
                if(Grabbed == null){
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
                        GetNode<Marker2D>("TongueOrigin").Rotation = (GetGlobalMousePosition() - GetNode<Marker2D>("TongueOrigin").GlobalPosition).Normalized().Angle() - Rotation;
                        if(IsInWater()){
                            velocity.Y = Mathf.MoveToward(velocity.Y, 0, WaterFriction);
                            velocity.X = Mathf.MoveToward(velocity.X, 0, WaterFriction);
                            if(Impulse && swimming){
                                velocity += Vector2.FromAngle(Rotation) * WaterSpeed;
                                GetNode<AnimationPlayer>("AnimationPlayer").Play("move");
                                
                                Impulse = false;
                                GetNode<Timer>("Timer").Start();
                            }
                        }else if(GlobalPosition.Y < -1810){
                            velocity.Y += gravity * (float)delta;
                            velocity.X = Mathf.MoveToward(velocity.X, 0, AirFriction);
                        }
                    }
                    GetNode<GpuParticles2D>("InkParticles").Emitting = false;
                }else{
                    GlobalPosition = Grabbed.GlobalPosition + new Vector2(0, 50);
                }
            }
        }
        
		Velocity = velocity;
        if(Velocity == Vector2.Zero || dash){
            Rotation = Mathf.LerpAngle(Rotation, (GetGlobalMousePosition() - GlobalPosition).Normalized().Angle(), 0.1f);
        }else{
            Rotation = Mathf.LerpAngle(Rotation, Velocity.Angle(), 0.1f);
        }
        
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
        if(SplashData.IsPlayerAlive()){
            SplashData.Kill();
        }
        GetNode<AudioStreamPlayer>("/root/World/MusicPlayer").Stop();
        GetSplashData().GetInventory().Clear();
        GetNode<AnimationPlayer>("ActionAnimationPlayer").Play("die");
    }

    public void Respawn(){
        if(!SplashData.IsPlayerAlive()){
            GlobalPosition = new Vector2(-160, 2150);
            SplashData = new PlayerData();
            GetNode<UI>("CanvasLayer/UI").SetPlayerData(SplashData);
            GetNode<World>("/root/World").AdddHours(6f);
            GetNode<AnimationPlayer>("ActionAnimationPlayer").PlayBackwards("die");
        }
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
            GrabPoint = GetNode<Area2D>("TongueOrigin/Tongue").GlobalPosition;
        }
        GetNode<AnimationPlayer>("ActionAnimationPlayer").PlayBackwards("grab");
    }

    public void _OnGrabArea(Area2D area){
        GrabPoint = GetNode<Area2D>("TongueOrigin/Tongue").GlobalPosition;
        GetNode<AnimationPlayer>("ActionAnimationPlayer").PlayBackwards("grab");
    }

    public void FinishGrab(){
        GetNode<AnimationPlayer>("ActionAnimationPlayer").PlayBackwards("grab");
    }

    public void _OnCollectionAreaBodyEntered(Node2D body){
        if(body is Collectible){
            Collectible collectible = (Collectible)body;
            collectible.Drop(this);
            if(collectible is Fruit){
                GetSplashData().Eat();
            }
        }
    }

    public PlayerData GetSplashData(){
        return SplashData;
    }

    public bool IsProtected(){
        return Protected;
    }

    public void Protect(bool protect){
        Protected = protect;
    }

    public void Grab(Insect insect){
        Grabbed = insect;
    }
}
