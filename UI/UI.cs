using Godot;
using System;

public partial class UI : Control
{
    private OxygenLevel OxygenLevelUI;
    private EnergyLevel EnergyLevelUI;
    private PlayerData PlayerData;
    public override void _Ready()
    {
        OxygenLevelUI = GetNode<OxygenLevel>("MarginContainer/VBoxContainer/HBoxContainer/OxygenLevel");
        EnergyLevelUI = GetNode<EnergyLevel>("MarginContainer/VBoxContainer/HBoxContainer2/EnergyLevel");
    }

    public void SetPlayerData(PlayerData playerData)
    {
        PlayerData = playerData;
        PlayerData.Breath += _OnBreath;
        PlayerData.Energy += _OnEnergy;
        _OnBreath();
        _OnEnergy();
    }

    public void _OnBreath()
    {
        OxygenLevelUI.SetOxygen(PlayerData.GetOxygen());
    }

    public void _OnEnergy()
    {
        EnergyLevelUI.SetEnergy(PlayerData.GetEnergy());
    }
}
