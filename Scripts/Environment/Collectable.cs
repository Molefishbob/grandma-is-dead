using Godot;
using System;

public partial class Collectable: Node
{
    protected bool _isCollected = false;
    public bool IsCollected
    {
        get { return _isCollected; }
        protected set { _isCollected = value; }
    }

    [Export]
    protected bool _isVisible = true;
    public bool IsVisible
    {
        get { return _isVisible; }
        protected set { _isVisible = value; }
    }

    [Export]
    protected IngredientType _ingredient;

    public IngredientType Ingredient
    {
        get { return _ingredient; }
        protected set { _ingredient = value; }
    }
    public override void _Ready()
    {
        base._Ready();
        SetVisibility(_isVisible);
    }

    public IngredientType? Collect()
    {
        if (!_isCollected)
        {
            IsCollected = true;
            SetVisibility(false);
            return Ingredient;
        }
        return null;
    }

    protected void SetVisibility(bool visible)
    {
        IsVisible = visible;
        if (visible)
        {
            GetChild<Node2D>(0).Visible = true;
        }
        else
        {
            GetChild<Node2D>(0).Visible = false;
        }
    }
}
