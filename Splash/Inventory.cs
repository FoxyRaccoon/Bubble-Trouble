using Godot;
using System;
using System.Collections.Generic;

public class Inventory{
    private Stack OxygenStack;
    private Stack OilStack;
    private Stack ShellStack;
    private Stack FruitStack;

    public Inventory(){
        OxygenStack = new Stack(ItemEnum.OXYGEN, 0);
        OilStack = new Stack(ItemEnum.OIL, 0);
        ShellStack = new Stack(ItemEnum.SHELL, 0);
        FruitStack = new Stack(ItemEnum.FRUIT, 0);
    }

    public int HasItem(Item item){
        switch(item.GetName()){
            case "Oxygen":
                return OxygenStack.GetQuantity();
            case "Oil":
                return OilStack.GetQuantity();
            case "Shell":
                return ShellStack.GetQuantity();
            case "Fruit":
                return FruitStack.GetQuantity();
            default:
                return 0;
        }
    }

    public Stack AddStack(Stack stack){
        switch(stack.GetItem().GetName()){
            case "Oxygen":
                return OxygenStack.AddQuantity(stack);
            case "Oil":
                return OilStack.AddQuantity(stack);
            case "Shell":
                return ShellStack.AddQuantity(stack);
            case "Fruit":
                return FruitStack.AddQuantity(stack);
            default:
                return null;
        }
    }

    public Stack RemoveStack(Item item, int quantity){
        switch(item.GetName()){
            case "Oxygen":
                return OxygenStack.RemoveQuantity(quantity);
            case "Oil":
                return OilStack.RemoveQuantity(quantity);
            case "Shell":
                return ShellStack.RemoveQuantity(quantity);
            case "Fruit":
                return FruitStack.RemoveQuantity(quantity);
            default:
                return null;
        }
    }
}