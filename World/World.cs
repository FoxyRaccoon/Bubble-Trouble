using Godot;
using System;

public partial class World : Node2D
{
    private PackedScene WaterParticles = GD.Load<PackedScene>("res://World/water_particles.tscn");
    public void _OnWaterBodyEntered(Node2D body)
    {
        if (body is FloatingBody)
        {
            ((FloatingBody)body).SetInWater(true);
            GpuParticles2D localInstance = (GpuParticles2D)WaterParticles.Instantiate();
            localInstance.Position = new Vector2(body.GlobalPosition.X, -1800);
            AddChild(localInstance);
        }
    }

    public void _OnWaterBodyExited(Node2D body)
    {
        if (body is FloatingBody)
        {
            ((FloatingBody)body).SetInWater(false);
            GpuParticles2D localInstance = (GpuParticles2D)WaterParticles.Instantiate();
            localInstance.Position = new Vector2(body.GlobalPosition.X, -1800);
            AddChild(localInstance);
        }
    }
}
