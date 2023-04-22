using Godot;
using System;

public partial class Insect : CharacterBody2D
{
    public void _OnGrabEntered(Node2D body)
    {
        if(body is Splash){
            Splash splash = (Splash)body;
            splash.Grab(this);
        }
    }
}
