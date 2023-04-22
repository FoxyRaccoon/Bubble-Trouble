using Godot;
using System;

public partial class BeginningScene : Node2D
{
    public void EnterScene(){
        GetNode<UI>("/root/World/Splash/CanvasLayer/UI").Visible = false;
    }

    public void PlayerSpawn(){
        GetNode<Splash>("/root/World/Splash").Die();
    }

    public void FinishScene()
    {
        Visible = false;
        GetNode<UI>("/root/World/Splash/CanvasLayer/UI").Visible = true;
        GetNode<Camera2D>("/root/World/FollowingCamera").Enabled = true;
        GetNode<Camera2D>("/root/World/FollowingCamera").MakeCurrent();
    }
}
