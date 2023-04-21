using Godot;
using System;

public partial class Oxygen : Collectible
{
    public override void _Ready()
    {
        SetItem(new Stack(ItemEnum.OXYGEN, 1));
    }
}
