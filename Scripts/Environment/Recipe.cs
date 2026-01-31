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
    [Export]
    protected Texture2D[] _ingredients = [];
    protected IngredientType[] _insertedIngredients = [];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        int i = 0;
        foreach(var option in _options)
        {
            option.Clear();
            foreach( var ingredient in _ingredients)
            {
                option.AddIconItem(ingredient, "", i);
            }
            i++;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
