using Godot;
using System;

public partial class GlobalConstants : Node
{
    // Animations
    public const string ANIM_IDLE = "Idle";
    public const string ANIM_MOVE = "Move";
    public const string ANIM_INTERACT = "Interact";
    public const string ANIM_GATHER = "Attack";
    public const string ANIM_MASK = "Mask";

    // Inputs
    public const string INPUT_MOVE_LEFT = "MoveLeft";
    public const string INPUT_MOVE_RIGHT = "MoveRight";
    public const string INPUT_MOVE_UP = "MoveUp";
    public const string INPUT_MOVE_DOWN = "MoveDown";

    // Notifications
    public const int NOTIFICATION_ENTER_STATE = 5001;
    public const int NOTIFICATION_EXIT_STATE = 5002;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
