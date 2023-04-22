using Godot;

public delegate void BreathHandler();
public delegate void EnergyHandler();
public class PlayerData{
    private float MaxOxygen;
    private float CurrentOxygen;
    private float MaxEnergy;
    private float CurrentEnergy;
    private Inventory PlayerInventory;
    private bool IsAlive = true;
    public event BreathHandler Breath;
    public event EnergyHandler Energy;
    public PlayerData(){
        MaxOxygen = 10f;
        CurrentOxygen = 10f;
        MaxEnergy = 100f;
        CurrentEnergy = 100f;
        PlayerInventory = new Inventory();
    }

    public float GetOxygen(){
        return CurrentOxygen;
    }
    
    public float GetEnergy(){
        return CurrentEnergy;
    }

    public float GetMaxEnergy(){
        return MaxEnergy;
    }

    public void ComputeOxygen(float delta, bool inWater){
        if(inWater){
            CurrentOxygen += delta;
            if(CurrentOxygen > MaxOxygen){
                CurrentOxygen = MaxOxygen;
            }else{
                Breath?.Invoke();
            }
        }else{
            CurrentOxygen -= delta;
            if(CurrentOxygen < 0){
                CurrentOxygen = 0;
                IsAlive = false;
            }else{
                Breath?.Invoke();
            }
        }
        
    }

    public bool UseEnergy(float energy){
        CurrentEnergy -= energy;
        if(CurrentEnergy <= MaxEnergy-10){
            Eat();
        }
        if(CurrentEnergy < 0){
            CurrentEnergy = 0;
            Energy?.Invoke();
            return false;
        }else{
            Energy?.Invoke();
            return true;
        }
    }

    public void AddEnergy(float energy){
        CurrentEnergy += energy;
        if(CurrentEnergy > MaxEnergy){
            CurrentEnergy = MaxEnergy;
        }
        Energy?.Invoke();
    }

    public void Eat(){
        if(GetEnergy() < GetMaxEnergy()){
            int nbFruit = GetInventory().HasItem(ItemEnum.FRUIT);
            if(nbFruit > 0){
                int neededFruits = Mathf.CeilToInt(GetMaxEnergy() - GetEnergy()/10);
                AddEnergy(10 * Mathf.Min(nbFruit, neededFruits));
                GetInventory().RemoveStack(ItemEnum.FRUIT, Mathf.Min(nbFruit, neededFruits));
            }
        }
    }

    public bool IsPlayerAlive(){
        return IsAlive;
    }

    public void Kill(){
        IsAlive = false;
    }

    public Inventory GetInventory(){
        return PlayerInventory;
    }
}