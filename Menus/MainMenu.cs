using Godot;
using System;

public partial class MainMenu : Control
{
    public void _OnExitPressed()
    {
        GetTree().Quit();
    }

    public void _OnPlayPressed()
    {
        GetNode<Transition>("/root/Transition").WhiteTransition("res://World/world.tscn");
    }
}
