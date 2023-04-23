using Godot;
using System;

public delegate void OnTimeChanged();
public partial class World : Node2D
{
    private PackedScene WaterParticles = GD.Load<PackedScene>("res://World/water_particles.tscn");
    private bool Day = true;
    public event OnTimeChanged OnNight;
    public event OnTimeChanged OnDay;
    public void _OnWaterBodyEntered(Node2D body)
    {
        if (body is FloatingBody)
        {
            ((FloatingBody)body).SetInWater(true);
            GpuParticles2D localInstance = (GpuParticles2D)WaterParticles.Instantiate();
            localInstance.Position = new Vector2(body.GlobalPosition.X, -1800);
            CallDeferred("add_child", localInstance);
        }
    }

    public void _OnWaterBodyExited(Node2D body)
    {
        if (body is FloatingBody)
        {
            ((FloatingBody)body).SetInWater(false);
            GpuParticles2D localInstance = (GpuParticles2D)WaterParticles.Instantiate();
            localInstance.Position = new Vector2(body.GlobalPosition.X, -1800);
            CallDeferred("add_child", localInstance);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        GetNode<Marker2D>("WorldCenter").RotationDegrees = GetNode<Marker2D>("WorldCenter").RotationDegrees + 1f * (float)delta;
        if(GetNode<Marker2D>("WorldCenter").RotationDegrees > 360)
            GetNode<Marker2D>("WorldCenter").RotationDegrees = 0;

        if(GetNode<Marker2D>("WorldCenter").RotationDegrees > 90 && GetNode<Marker2D>("WorldCenter").RotationDegrees < 270 && Day){
            Day = false;
            GetNode<Marker2D>("WorldCenter/Sun").Visible = false;
            GetNode<Marker2D>("WorldCenter/Moon").Visible = true;
            OnNight?.Invoke();

        }else if((GetNode<Marker2D>("WorldCenter").RotationDegrees < 90 || GetNode<Marker2D>("WorldCenter").RotationDegrees > 270) && !Day){
            Day = true;
            GetNode<Marker2D>("WorldCenter/Sun").Visible = true;
            GetNode<Marker2D>("WorldCenter/Moon").Visible = false;
            OnDay?.Invoke();
        }
        
    }

    public bool IsDay()
    {
        return Day;
    }

    public void AdddHours(float hours)
    {
        GetNode<Marker2D>("WorldCenter").RotationDegrees = GetNode<Marker2D>("WorldCenter").RotationDegrees + hours * 15;
    }

    public void _OnMusicPlayerFinished(){
        GetNode<Timer>("MusicTimer").WaitTime = (float)GD.RandRange(10, 30);
        GetNode<Timer>("MusicTimer").Start();
    }

    public void _OnMusicTimerTimeout()
    {
        GetNode<AudioStreamPlayer>("MusicPlayer").Play();
    }
}
