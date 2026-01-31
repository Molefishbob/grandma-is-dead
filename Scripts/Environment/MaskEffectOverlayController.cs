using Godot;
using System;

public partial class MaskEffectOverlayController : ParallaxBackground
{

	[Export]
	public Player playerNode;
	[Export]
	public ColorRect colorRect;
	private float currentAlpha = 0f;
	private float goalAlpha = 0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerNode = GetParent<Player>();
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (playerNode.isMasked)
		{
			goalAlpha = 1f;
			Visible = true;
		}
		else
		{
			goalAlpha = 0f;
			if (currentAlpha <= 0.01f)
				Visible = false;
		}

		currentAlpha = Mathf.Lerp(currentAlpha, goalAlpha, 0.05f);
		colorRect.Modulate = new Color(1f, 1f, 1f, currentAlpha);
	}
}
