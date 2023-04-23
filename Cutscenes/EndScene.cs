using Godot;
using System;

public partial class EndScene : Node2D
{
    public void LaunchScene(){
        Visible = true;
        GetNode<Camera2D>("/root/World/FollowingCamera").Enabled = false;
        GetNode<Camera2D>("Camera2D").Enabled = true;
        GetNode<Camera2D>("Camera2D").MakeCurrent();
        GetNode<AudioStreamPlayer>("/root/World/MusicPlayer").Stop();
        GetNode<AnimationPlayer>("AnimationPlayer").Play("go");
    }

    public void ReturnToMainMenu(){
        GetNode<Transition>("/root/Transition").WhiteTransition("res://Menus/main_menu.tscn");
    }
}
