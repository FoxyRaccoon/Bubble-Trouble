using Godot;
using System;

public partial class WaterParticles : GpuParticles2D
{
    public override void _Ready()
    {
        Emitting = true;
    }
    public void _OnTimeout()
    {
        QueueFree();
    }
}
