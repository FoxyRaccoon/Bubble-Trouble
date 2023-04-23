using Godot;
using System;

public partial class InsectSpawner : Node2D
{
    private static PackedScene  InsectScene = (PackedScene)ResourceLoader.Load("res://Foes/insect.tscn");

    public override void _Ready()
    {
        GetParent<World>().OnDay += _OnDayEntering;
        GetParent<World>().OnNight += _OnNightEntering;
    }
    public void _OnDayEntering(){
        //Spawn 2 or 3 insects
        int insectCount = new Random().Next(2, 4);
        for(int i = 0; i < insectCount; i++){
            Insect insect = (Insect)InsectScene.Instantiate();
            insect.Position = new Vector2(new Random().Next(-100, 100), new Random().Next(-100, 100));
            AddChild(insect);
        }
    }

    public void _OnNightEntering(){
        GetNode<Splash>("/root/World/Splash").Ungrabbed();
        foreach(Node child in GetChildren()){
            child.CallDeferred("queue_free");
        }
    }

}
