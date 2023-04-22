using Godot;
using System;

public partial class Spaceship : Area2D
{
    //Resources needed to build each step of the spaceship, the resources are SHELL, OXYGEN and OIL
    private static int[][] ResourcesNeeded = new int[4][]{
        new int[3]{1, 0, 0},//20
        new int[3]{1, 20, 0},//20
        new int[3]{1, 20, 10},//10
        new int[3]{1, 10, 20}//10
    };

    private int[] CurrentResources = new int[3]{0, 0, 0};

    public override void _Ready()
    {
        UpdateUI();
    }

    public void _OnBodyEntered(Node2D body)
    {
        if(body is Splash && CurrentStep() < 4){
            Splash player = (Splash)body;
            int shellQuantity = Mathf.Min(player.GetSplashData().GetInventory().HasItem(ItemEnum.SHELL), ResourcesNeeded[CurrentStep()][0] - CurrentResources[0]);
            int oxygenQuantity = Mathf.Min(player.GetSplashData().GetInventory().HasItem(ItemEnum.OXYGEN), ResourcesNeeded[CurrentStep()][1] - CurrentResources[1]);
            int oilQuantity = Mathf.Min(player.GetSplashData().GetInventory().HasItem(ItemEnum.OIL), ResourcesNeeded[CurrentStep()][2] - CurrentResources[2]);
            if(shellQuantity > 0){
                CurrentResources[0] += player.GetSplashData().GetInventory().RemoveStack(ItemEnum.SHELL, shellQuantity).GetQuantity();
            }
            if(oxygenQuantity > 0){
                CurrentResources[1]+= player.GetSplashData().GetInventory().RemoveStack(ItemEnum.OXYGEN, oxygenQuantity).GetQuantity();
            }
            if(oilQuantity > 0){
                CurrentResources[2]+= player.GetSplashData().GetInventory().RemoveStack(ItemEnum.OIL, oilQuantity).GetQuantity();
            }
            if(CurrentResources[0] == ResourcesNeeded[CurrentStep()][0] && CurrentResources[1] == ResourcesNeeded[CurrentStep()][1] && CurrentResources[2] == ResourcesNeeded[CurrentStep()][2]){
                //Replace by animation
                GetNode<Sprite2D>("Sprite2D").Frame--;
                CurrentResources[0] = 0;
                CurrentResources[1] = 0;
                CurrentResources[2] = 0;
            }
            UpdateUI();
        }
    }

    public int CurrentStep(){
        return 4 - GetNode<Sprite2D>("Sprite2D").Frame;
    }

    public void UpdateUI(){
        Node2D ShellBar = GetNode<Node2D>("ShellBar");
        Node2D OxygenBar = GetNode<Node2D>("OxygenBar");
        Node2D OilBar = GetNode<Node2D>("OilBar");
        if(CurrentStep() == 4){
            ShellBar.Visible = false;
            OxygenBar.Visible = false;
            OilBar.Visible = false;
        }else{
            if(ResourcesNeeded[CurrentStep()][0] > 0){
                ShellBar.Visible = true;
                ShellBar.GetNode<ColorRect>("ColorRect2").Size = new Vector2(6, (float)CurrentResources[0] / ResourcesNeeded[CurrentStep()][0] * 40);
            }else{
                ShellBar.Visible = false;
            }
            if(ResourcesNeeded[CurrentStep()][1] > 0){
                OxygenBar.Visible = true;
                OxygenBar.GetNode<ColorRect>("ColorRect2").Size = new Vector2(6, (float)CurrentResources[1] / ResourcesNeeded[CurrentStep()][1] * 40);
            }else{
                OxygenBar.Visible = false;
            }
            if(ResourcesNeeded[CurrentStep()][2] > 0){
                OilBar.Visible = true;
                OilBar.GetNode<ColorRect>("ColorRect2").Size = new Vector2(6, (float)CurrentResources[2] / ResourcesNeeded[CurrentStep()][2] * 40);
            }else{
                OilBar.Visible = false;
            }
        }
    }
}
