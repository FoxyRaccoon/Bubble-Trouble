using Godot;
using System;

public partial class Oil : Collectible
{
    public override void _Ready()
    {
        SetItem(new Stack(ItemEnum.OIL, 1));
    }
}
