using Godot;
using System;

public partial class World : Node2D
{
    public void _OnWaterBodyEntered(Node2D body)
    {
        if (body is FloatingBody)
        {
            ((FloatingBody)body).SetInWater(true);
        }
    }

    public void _OnWaterBodyExited(Node2D body)
    {
        if (body is FloatingBody)
        {
            ((FloatingBody)body).SetInWater(false);
        }
    }
}
