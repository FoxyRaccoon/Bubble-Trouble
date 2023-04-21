using Godot;
using System;

public partial class Shell : Collectible
{
    public override void _Ready()
    {
        SetItem(new Stack(ItemEnum.SHELL, 1));
    }
}
