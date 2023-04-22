using Godot;
using System;

public partial class OxygenLevel : HBoxContainer
{
    private static PackedScene BubbleScene = GD.Load<PackedScene>("res://UI/bubble.tscn");
    public override void _Ready()
    {
        for(int i = 0; i < 10; i++)
            AddChild((Control)BubbleScene.Instantiate());
    }

    public void SetOxygen(float oxygen){
        for(int i = 0; i < 10; i++){
            var bubble = (Bubble)GetChild(9-i);
            if(i < oxygen){
                bubble.GetNode<Sprite2D>("Sprite2D").Show();
                bubble.SetFrame(0);
                if(i == (int)oxygen){
                    bubble.SetFrame(6 - (int)(7*(oxygen - i)));
                }
            }else{
                if(!bubble.GetNode<AnimationPlayer>("AnimationPlayer").IsPlaying())
                   bubble.GetNode<Sprite2D>("Sprite2D").Hide();
            }
        }
    }
}
