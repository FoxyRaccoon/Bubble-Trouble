using Godot;
using System;

public partial class GodotScreen : Control
{
    public void OnTimeOut()
    {
        GetNode<Transition>("/root/Transition").BlueTransition("res://Menus/jam_screen.tscn");
    }
}
