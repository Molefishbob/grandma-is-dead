using Godot;
using System;

public partial class Recipe : Node
{
    [Export]
    protected int[] _readyIngredients = [];

    [Export]
    protected int[] _requiredIngredients = [];
    [Export]
    protected OptionButton[] _options = [];
    protected IngredientType[] _insertedIngredients = [];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach(var option in _options)
        {
            option.Clear();
            foreach(int i in _requiredIngredients)
            {
                option.AddItem(((IngredientType)i).ToString());
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
