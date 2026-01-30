using System;
using System.Linq;
using Godot;

public partial class StateMachine : Node
{
    [Export]
    private Node currentState;

    [Export]
    private Node[] states;

    public override void _Ready()
    {
        // currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        // Node newState = states.FirstOrDefault(x => x is T);
        // if (newState == null)
        // {
        //     return;
        // }

        // if (currentState is T)
        // {
        //     return;
        // }

        // currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        // currentState = newState;
        // currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
