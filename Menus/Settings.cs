using Godot;
using System;

public partial class Settings : Control
{
    public override void _Ready()
    {
        GetNode<Slider>("MarginContainer/VBoxContainer/Effects/VBoxContainer/SoundSlider").Value = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master"));
        GetNode<Slider>("MarginContainer/VBoxContainer/Main/VBoxContainer/MusicSlider").Value = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music"));
    }
    public void _OnSoundSliderValueChanged(float value)
    {
        if(value <= -10){
            AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), true);
        }else{
            AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), false);
        }
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), value);
    }

    public void _OnMusicSliderValueChanged(float value)
    {
        if(value <= -10){
            AudioServer.SetBusMute(AudioServer.GetBusIndex("Music"), true);
        }else{
            AudioServer.SetBusMute(AudioServer.GetBusIndex("Music"), false);
        }
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), value);
    }

    public void _OnBackPressed(){
        if(GetParent().Name != "SettingsCanvas"){
            GetNode<Transition>("/root/Transition").WhiteTransition("res://Menus/main_menu.tscn");
        }else{
            GetTree().Paused = false;
            QueueFree();
        }
       
    }
}
