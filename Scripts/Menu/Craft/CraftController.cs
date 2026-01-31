using Godot;
using System;

public partial class CraftController : Node
{
    [Export]
    private Node2D craftMenu;

    public override void _Ready()
    {
        craftMenu.Visible = false;
    }

    public void ToggleCraftMenu()
    {
        craftMenu.Visible = !craftMenu.Visible;
        if (craftMenu.Visible)
        {
            GetTree().Paused = true;
        }
        else
        {
            GetTree().Paused = false;
        }
    }
}
