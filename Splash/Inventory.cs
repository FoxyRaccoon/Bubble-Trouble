public class Inventory{
    private int InventorySize;
    private Stack[] InventoryStacks;

    public Inventory(int inventorySize){
        InventorySize = inventorySize;
        InventoryStacks = new Stack[InventorySize];
    }
}