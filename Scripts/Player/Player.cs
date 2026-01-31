using System;
using System.Diagnostics;
using System.Linq;
using Godot;

public partial class Player : CharacterBody2D
{
    const string INPUT_MOVE_LEFT = "MoveLeft";
    const string INPUT_MOVE_RIGHT = "MoveRight";
    const string INPUT_MOVE_UP = "MoveUp";
    const string INPUT_MOVE_DOWN = "MoveDown";
    const string ANIM_IDLE = "Idle";
    const string ANIM_MOVE = "Move";
    const string ANIM_INTERACT = "Interact";
    const string ANIM_GATHER = "Attack";
    const string ANIM_MASK = "Mask";

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
		GD.Print($"Player Ready");
		base._Ready();
	}

	public override void _ExitTree() { }

	public override void _Input(InputEvent @event)
	{
		direction = Input.GetVector(
			INPUT_MOVE_LEFT,
			INPUT_MOVE_RIGHT,
			INPUT_MOVE_UP,
			INPUT_MOVE_DOWN
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

		bool isMovingRight = Velocity.X > 0;
		SpriteNode.FlipH = isMovingRight;
	}
}
