using Godot;
using System;

public partial class EnergyLevel : ProgressBar
{
    public void SetEnergy(float energy)
    {
        Value = energy;
    }
}
