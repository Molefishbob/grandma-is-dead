using Godot;
using System;

public partial class HiddenObject : Sprite2D
{
	[Export]
	public Player playerNode;

	public override void _Ready()
	{
		playerNode = GetTree().GetCurrentScene().GetNode<Player>("Player");

		HideObject();
		base._Ready();
	}

	public override void _Process(double delta)
	{
		if (playerNode.isMasked)
		{
			ShowObject();
		}
		else
		{
			HideObject();			
		}
	}

	private void HideObject()
	{
		Visible = false;
	}

	private void ShowObject()
	{
		Visible = true;
	}
}
