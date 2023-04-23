using Godot;
using System;

public partial class GameStart : Control
{
    public void _OnButtonPressed(){
        GetNode<Transition>("/root/Transition").WhiteTransition("res://Menus/main_menu.tscn");
    }
}
