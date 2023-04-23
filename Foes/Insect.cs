using Godot;
using System;

public partial class Insect : CharacterBody2D
{
    private Node2D Target;
    private float Speed = 200f;
    private static PackedScene ShellScene = (PackedScene)ResourceLoader.Load("res://Collectibles/shell.tscn");
    public void _OnGrabEntered(Node2D body)
    {
        if(body is Splash){
            Splash splash = (Splash)body;
            splash.Grab(this);
        }else if(body is Fruit){
            Target = null;
            body.CallDeferred("queue_free");
            if(new Random().Next(0, 10) == 0){
                SpawnShell();
            }
        }
    }

    public void _OnVisionEntered(Node2D body)
    {
        if(body is Splash || (body is Fruit && Target == null)){
            Target = body;
        }
    }

    public void _OnVisionExited(Node2D body)
    {
        if(Target == body){
            Target = null;
        }
    }

    public void _OnNavigationAgent2DTargetReached()
    {
        Target = null;
    }

    public void _OnTimerTimeout(){
        if(Target != null){
            if(Target.IsVisibleInTree()){
                GetNode<NavigationAgent2D>("NavigationAgent2D").TargetPosition = Target.GlobalPosition + new Vector2(0, 30);
            }else{
                Target = null;
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
		NavigationAgent2D navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		if (navAgent.IsNavigationFinished() == false)
		{
			Vector2 direction = GlobalPosition.DirectionTo(navAgent.GetNextPathPosition());
			velocity = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		
		if(velocity.X < 0){
			GetNode<Sprite2D>("Sprite2D").FlipH = false;
		}else if(velocity.X > 0){
			GetNode<Sprite2D>("Sprite2D").FlipH = true;
		}

		Velocity = velocity;
		MoveAndSlide();
    }

    private void SpawnShell(){
        Shell shell = (Shell)ShellScene.Instantiate();
        shell.Position = GlobalPosition - new Vector2(0, 30);
        GetParent().GetParent().GetNode("Collectibles").CallDeferred("add_child", shell);
    }

    public void _OnAudioFinished(){
        GetNode<Timer>("Timer2").WaitTime = new Random().Next(5, 10);
        GetNode<Timer>("Timer2").Start();
    }

    public void _OnTimer2Timeout(){
        GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
    }
}
