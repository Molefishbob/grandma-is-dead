using Godot;
using System;

public partial class MaskController : Sprite2D
{
	public Player playerNode;

	public override void _Ready()
	{
		playerNode = GetParent<Player>();
		Modulate =  new Color(1f, 1f, 1f, 0f);
		base._Ready();
	}

	public override void _Process(double delta)
	{
		if (playerNode.Velocity.Length() > 0)
		{
			GD.Print($"Velocity: {playerNode.GetRealVelocity().Length()}");
			Modulate =  new Color(1f, 1f, 1f, 0f);
		}

		if (playerNode.SpriteNode.FlipH != FlipH)
		{
			FlipH = playerNode.SpriteNode.FlipH;
		}
	}
}
