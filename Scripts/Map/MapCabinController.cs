using Godot;
using System;

public partial class MapCabinController : Node2D
{

	[Export]
	public Player playerNode;

	[Export]
	public Vector2 minMaxScale = new(0.4f, 1.4f);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (playerNode == null)
		{
			GD.PrintErr("Player node is not assigned in MapCabinController");
			return;
		}
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float screenHeight = GetViewportRect().Size.Y; // 500f in your case
		float normalizedY = Mathf.Clamp(playerNode.GlobalPosition.Y / screenHeight, 0f, 1f);

		// Interpolate between min and max scale
		float newScale = Mathf.Lerp(minMaxScale.X, minMaxScale.Y, normalizedY);
		playerNode.Scale = new Vector2(newScale, newScale);
	}
}
