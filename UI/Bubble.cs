using Godot;
using System;

public partial class Bubble : Control
{
    public void SetFrame(int frame){
        if(frame == 6 && GetNode<Sprite2D>("Sprite2D").Frame == 5){
            GetNode<AnimationPlayer>("AnimationPlayer").Play("explode");
        }
        GetNode<Sprite2D>("Sprite2D").Frame = frame;
        
    }
}
