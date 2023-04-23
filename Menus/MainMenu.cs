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

    public void _OnSettingsPressed()
    {
        GetNode<Transition>("/root/Transition").WhiteTransition("res://Menus/settings.tscn");
    }
}
