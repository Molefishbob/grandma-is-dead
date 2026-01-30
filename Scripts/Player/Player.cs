using System;
using System.Linq;
using Godot;

public partial class Player : CharacterBody2D
{
	[ExportGroup("Required Nodes")]
	[Export]
	public AnimationPlayer AnimPlayerNode { get; private set; }

	[Export]
	public StateMachine StateMachineNode { get; private set; }

	[Export]
	public Vector2 speed = new();

	[Export]
	public Sprite2D SpriteNode { get; private set; }

	public Vector2 direction = new();

	public override void _Ready()
	{
		base._Ready();
	}

	public override void _ExitTree() { }

	public override void _Input(InputEvent @event)
	{
		direction = Input.GetVector(
			GameConstants.INPUT_MOVE_LEFT,
			GameConstants.INPUT_MOVE_RIGHT,
			GameConstants.INPUT_MOVE_UP,
			GameConstants.INPUT_MOVE_DOWN
		);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (direction == Vector2.Zero)
		{
			// StateMachineNode.SwitchState<PlayerIdleState>();
			return;
		}

		Velocity = new Vector2(
			direction.X,
			direction.Y
		);
		Velocity *= speed;

		MoveAndSlide();
		Flip();
	}

	public void HandleCollisionBoxAreaEntered(Area2D area)
	{
		GD.Print($"{area.Name} hit");
	}


	public void Flip()
	{
		bool isNotMovingHorizontally = Velocity.X == 0;

		if (isNotMovingHorizontally)
		{
			return;
		}

		bool isMovingLeft = Velocity.X < 0;
		SpriteNode.FlipH = isMovingLeft;
	}

	public void ToggleCollisionBox(bool value)
	{
		// CollisionNode.Disabled = value;
	}
}
