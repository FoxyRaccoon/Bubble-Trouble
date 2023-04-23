using Godot;
using System;

public partial class LogoScreen : Control
{
    public void OnTimeOut()
    {
        GetNode<Transition>("/root/Transition").GreenTransition("res://Menus/game_start.tscn");
    }
}
