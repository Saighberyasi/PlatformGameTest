using Godot;
using System;

public partial class Cannon : StaticBody2D
{
    [Export]private Cannonball _cannonBall;
    [Export] private float _shotCooldown = 1.5f;
    private float _shotTimer;
    
    
    public override void _Ready()
    {
        _shotTimer = _shotCooldown;
    }

    private void Shoot()
    {
        Cannonball newCannonball = (Cannonball)_cannonBall.Duplicate();
        AddChild(newCannonball);
        newCannonball.Visible = true;
        newCannonball.IsOriginal = false;
        newCannonball.GlobalPosition = GlobalPosition + Vector2.Left * 100f;
    }
    public override void _PhysicsProcess(double delta)
    {
        _shotTimer -= (float)delta;
        if (_shotTimer <= 0f)
        {
            Shoot();
            _shotTimer = _shotCooldown;
        }
    }
}
