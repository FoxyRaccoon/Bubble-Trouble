using Godot;
using System;

public partial class JamScreen : Control
{
    public void OnTimeOut()
    {
        GetNode<Transition>("/root/Transition").WhiteTransition("res://Menus/logo_screen.tscn");
    }
}

