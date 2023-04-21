public class Item{
    private string Name;
    private string Description;
    private int MaxStack;
    
    public Item(string name, string description, int maxStack){
        Name = name;
        Description = description;
        MaxStack = maxStack;
    }

    public string GetName(){
        return Name;
    }

    public string GetDescription(){
        return Description;
    }

    public int GetMaxStack(){
        return MaxStack;
    }
}