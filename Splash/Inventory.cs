public class Inventory{
    private int InventorySize;
    private Stack[] InventoryStacks;

    public Inventory(int inventorySize){
        InventorySize = inventorySize;
        InventoryStacks = new Stack[InventorySize];
    }

    public void AddStack(Stack stack){
        for(int i = 0; i < InventorySize; i++){
            if(InventoryStacks[i] == null){
                InventoryStacks[i] = stack;
                break;
            }else if(InventoryStacks[i].GetItem() == stack.GetItem() && InventoryStacks[i].GetQuantity() < InventoryStacks[i].GetItem().GetMaxStack()){
                AddStack(InventoryStacks[i].AddQuantity(stack));
                break;
            }
        }
    }
}