using Godot;

public class PlayerData{
    private float MaxOxygen;
    private float CurrentOxygen;
    private bool IsAlive = true;
    public PlayerData(){
        MaxOxygen = 10f;
        CurrentOxygen = 10f;
    }

    public float GetOxygen(){
        return CurrentOxygen;
    }

    public void ComputeOxygen(float delta, bool inWater){
        if(inWater){
            CurrentOxygen += delta;
            if(CurrentOxygen > MaxOxygen){
                CurrentOxygen = MaxOxygen;
            }
        }else{
            CurrentOxygen -= delta;
            if(CurrentOxygen < 0){
                CurrentOxygen = 0;
                IsAlive = false;
            }
        }
        GD.Print(CurrentOxygen);
    }

    public bool IsPlayerAlive(){
        return IsAlive;
    }
}