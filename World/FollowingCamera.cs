using Godot;
using System;

public partial class FollowingCamera : Camera2D
{
    private Splash Followed;
    private const float DisplayWidth = 1536;
    private float GoesToX = 0f;
    public override void _Ready()
    {
        Followed = GetNode<Splash>("../Splash");
        Followed.OutOfScreen += _FollowedChangedScreen;
    }

    public override void _Process(double delta)
    {
        Position = new Vector2(Position.X, Followed.GlobalPosition.Y);
        if(GoesToX != Position.X)
            Position = new Vector2(Position.X + (GoesToX - Position.X) * (float)delta * 5, Position.Y);
    }

    public void _FollowedChangedScreen()
    {
        float newX = Followed.GlobalPosition.X;
        float offset = 40;
        if(Position.X > 1 || Position.X < -1 || (newX+offset < DisplayWidth/(2*Zoom.X) && newX-offset > -DisplayWidth/(2*Zoom.X))){
            GoesToX = 0;
        }else if(newX > 1){
            GoesToX = DisplayWidth/Zoom.X;
        }else if(newX < -1){
            GoesToX = -DisplayWidth/Zoom.X;
        }
        Position = new Vector2(newX, Followed.GlobalPosition.Y);
    }
}
