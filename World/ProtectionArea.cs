using Godot;
using System;

public partial class ProtectionArea : Area2D
{
    public void _OnBodyEntered(Node body)
    {
        if(body is Splash){
            Splash splash = (Splash)body;
            splash.Protect(true);
        }
    }

    public void _OnBodyExited(Node body)
    {
        if(body is Splash){
            Splash splash = (Splash)body;
            splash.Protect(false);
        }
    }
}
