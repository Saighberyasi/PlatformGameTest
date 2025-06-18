using Godot;
using System;

public partial class Cannonball : Area2D 
{
    [Export] private float _moveSpeed;
    [Export] private float _lifeTime = 2f;
    public bool IsOriginal = true;
    
    public override void _Ready()
    {
        BodyEntered += Hit;
    }

    private void Hit(Node body)
    {
        if (!IsOriginal)
        {
            QueueFree();
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (!IsOriginal)
        {
            GlobalPosition -= new Vector2(_moveSpeed * (float)delta, 0f);
            _lifeTime -= (float)delta;
            if (_lifeTime <= 0)
            {
                QueueFree();
            }           
        }
    }
}
