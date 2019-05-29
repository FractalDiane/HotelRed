using Godot;
using System;


public class CodeLock : KinematicBody2D
{
	// Refs
	private AnimatedSprite spr;
	private Sprite interact;
    private bool isOpen = false;
    private int current = 0;
    private static int[] digitList = {-1, -1, -1, -1};
	// ================================================================
	// ================================================================

	public override void _Ready()
	{
        
		spr = GetNode<AnimatedSprite>("Sprite");
		interact = GetNode<Sprite>("Interact");
		interact.Hide();
        
	}


	public override void _Process(float delta)
	{
		ZIndex = (int)Position.y;
        if (isOpen)
        {
            if (Input.IsActionJustPressed("ui_cancel"))
            {
                clearCells();
                Player.State = Player.ST.MOVE;
                isOpen = false;
                Player.InventoryLock = false;
                Player.CodeOverlay.SetVisible(false);
            }
            else if (current == 4)
            {
                clearCells();
                Player.State = Player.ST.MOVE;
                isOpen = false;
                Player.InventoryLock = false;
                Player.CodeOverlay.SetVisible(false);
            }
            else if (Input.IsActionJustPressed("Backspace"))
                backSpace();
            else if (Input.IsActionJustPressed("Zero"))
                setCell(0);
            else if (Input.IsActionJustPressed("One"))
                setCell(1);
            else if (Input.IsActionJustPressed("Two"))
                setCell(2);
            else if (Input.IsActionJustPressed("Three"))
                setCell(3);
            else if (Input.IsActionJustPressed("Four"))
                setCell(4);
            else if (Input.IsActionJustPressed("Five"))
                setCell(5);
            else if (Input.IsActionJustPressed("Six"))
                setCell(6);
            else if (Input.IsActionJustPressed("Seven"))
                setCell(7);
            else if (Input.IsActionJustPressed("Eight"))
                setCell(8);
            else if (Input.IsActionJustPressed("Nine"))
                setCell(9);
        }
		else if (Input.IsActionJustPressed("sys_accept") && Player.State == Player.ST.MOVE && interact.Visible)
		{
			Player.State = Player.ST.NO_INPUT;
			interact.Hide();
            isOpen = true;
            Player.InventoryLock = true;
            Player.CodeOverlay.SetVisible(true);
        }
        
    }
	// ================================================================


	private void InteractAreaEntered(Area2D area)
	{
		if (area.IsInGroup("PlayerSight") && Player.State == Player.ST.MOVE)
		{
			interact.Show();
		}
	}


	private void InteractAreaExited(Area2D area)
	{
		if (area.IsInGroup("PlayerSight"))
		{
			interact.Hide();
		}
	}

    private void setCell(int digit)
    {
        digitList[current] = digit;
        // switch(current)
        // {
        //     case 0:
        //         Player.CodeOverlay.GetNode<Control>("Control").GetChild<Sprite>(digit).SetVisible(true);
        //         break;
        //     case 1:
        //         Player.CodeOverlay.GetNode<Control>("Control2").GetChild<Sprite>(digit).SetVisible(true);
        //         break;
        //     case 2:
        //         Player.CodeOverlay.GetNode<Control>("Control3").GetChild<Sprite>(digit).SetVisible(true);
        //         break;
        //     case 3:
        //         Player.CodeOverlay.GetNode<Control>("Control4").GetChild<Sprite>(digit).SetVisible(true);
        //         break;
        // }
        Player.CodeOverlay.GetChild<Control>(current+1).GetChild<Sprite>(digit).SetVisible(true);
        current++;
    }

    private void backSpace()
    {
        if (current != 0)
        {
           Player.CodeOverlay.GetChild<Control>(current).GetChild<Sprite>(digitList[current-1]).SetVisible(false);
           current--;
        }
    }

    private void clearCells()
    {
        for (int i = 0; i < current; i++)
        {
            // switch(current)
            // {
            //     case 0:
            //         Player.CodeOverlay.GetNode<Control>("Control").GetChild<Sprite>(digitList[i]).SetVisible(false);
            //         break;
            //     case 1:
            //         Player.CodeOverlay.GetNode<Control>("Control1").GetChild<Sprite>(digitList[i]).SetVisible(false);
            //         break;
            //     case 2:
            //         Player.CodeOverlay.GetNode<Control>("Control2").GetChild<Sprite>(digitList[i]).SetVisible(false);
            //         break;
            //     case 3:
            //         Player.CodeOverlay.GetNode<Control>("Control3").GetChild<Sprite>(digitList[i]).SetVisible(false);
            //         break;
            // }
            Player.CodeOverlay.GetChild<Control>(i+1).GetChild<Sprite>(digitList[i]).SetVisible(false);
        }
        current = 0;
    }
}