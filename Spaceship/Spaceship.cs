using Godot;
using System;

public partial class Spaceship : StaticBody2D
{
    private static int[][] ResourcesNeeded = new int[4][]{
        new int[3]{20, 0, 0},
        new int[3]{20, 20, 0},
        new int[3]{10, 20, 10},
        new int[3]{10, 10, 20}
    };
}
