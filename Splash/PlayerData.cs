using Godot;

public class PlayerData{
    private float MaxOxygen;
    private float CurrentOxygen;
    private Inventory PlayerInventory;
    private bool IsAlive = true;
    public PlayerData(){
        MaxOxygen = 10f;
        CurrentOxygen = 10f;
        PlayerInventory = new Inventory(10);
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
    }

    public bool IsPlayerAlive(){
        return IsAlive;
    }

    public Inventory GetInventory(){
        return PlayerInventory;
    }
}