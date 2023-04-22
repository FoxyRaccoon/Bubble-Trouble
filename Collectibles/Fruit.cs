using Godot;
using System;

public partial class Fruit : Collectible
{
    public override void _Ready()
    {
        SetItem(new Stack(ItemEnum.FRUIT, 1));
    }
}
