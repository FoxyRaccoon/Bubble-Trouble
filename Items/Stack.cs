public class Stack{
    private int Quantity;
    private Item Item;

    public Stack(Item item, int quantity){
        Item = item;
        Quantity = quantity;
    }

    public int GetQuantity(){
        return Quantity;
    }

    public Item GetItem(){
        return Item;
    }

    public Stack RemoveQuantity(int quantity){
        if(quantity > Quantity){
            return null;
        }else{
            return new Stack(Item, Quantity - quantity);
        }
    }

    public Stack AddQuantity(Stack stack){
        if(stack.GetItem() == Item){
            if(stack.GetItem().GetMaxStack() < Quantity + stack.GetQuantity()){
                int overstack = stack.GetItem().GetMaxStack() - Quantity + stack.GetQuantity();
                Quantity = stack.GetItem().GetMaxStack();
                return new Stack(Item, overstack);
            }else{
                Quantity += stack.GetQuantity();
                return null;
            }
        }else{
            return stack;
        }
    }
}