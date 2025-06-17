using Godot;
using System;

public partial class Movement : CharacterBody2D
{
    [Export] private float _moveSpeed = 300.0f;
    [Export] private float _jumpVelocity = -600.0f;
    [Export] private float _gravityBase = 800f;
    [Export] private float _gravityMultiplier = 3f;
    [Export] private float _timeToStartFalling = 0.15f;
    [Export] private int _maxJumps = 2;
    private float _timeToStartFallingCounter;
    private int _jumpsLeft;
    private float _gravity;
    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
    
        if (!IsOnFloor())
        {
            velocity.Y += _gravity * (float)delta;
            if (Input.IsActionJustReleased("Jump"))
            {
                _gravity = _gravityBase * _gravityMultiplier;
            }
        }

        if (IsOnFloor())
        {
            _gravity = _gravityBase;
            _jumpsLeft = _maxJumps;
            _timeToStartFallingCounter = _timeToStartFalling;
            if (Input.IsActionJustPressed("Jump") && _jumpsLeft > 0)
            {
                velocity.Y += _jumpVelocity;
                _jumpsLeft--;
            }
        }

        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * _moveSpeed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, _moveSpeed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}