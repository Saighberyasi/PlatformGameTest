using Godot;
using System;

public partial class Movement : CharacterBody2D
{
    [Export] private float _moveSpeed = 300.0f;
    [Export] private float _jumpVelocity = -600.0f;
    [Export] private float _secondJumpMultiplier = 3f;
    [Export] private float _gravityBase = 800f;
    [Export] private float _gravityMultiplier = 3f;
    [Export] private float _timeToStartFalling = 0.3f;
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
            _timeToStartFallingCounter -= (float)delta;
            if (_timeToStartFallingCounter <= 0)
            {
                _gravity = _gravityBase * _gravityMultiplier;
            }
            
            if (Input.IsActionJustReleased("Jump"))
            {
                _timeToStartFallingCounter = 0f;
            }

            if (Input.IsActionJustPressed("Jump") && _jumpsLeft > 0)
            {
                Jump();
            }

            if (IsOnWall())
            {
                _jumpsLeft = _maxJumps;
                _gravity = _gravity / 1.3f;
            }
        }

        if (IsOnFloor())
        {
            _gravity = _gravityBase;
            _jumpsLeft = _maxJumps;
            _timeToStartFallingCounter = _timeToStartFalling;
            if (Input.IsActionJustPressed("Jump"))
            {
                Jump();
            }
        }

        void Jump()
        {
            if (_jumpsLeft < _maxJumps)
            {
                velocity.Y = 0f;
                velocity.Y += _jumpVelocity * _secondJumpMultiplier;
                _timeToStartFallingCounter = _timeToStartFalling;
                _jumpsLeft--;           
                return;
            }
            velocity.Y += _jumpVelocity;
            _timeToStartFallingCounter = _timeToStartFalling;
            _jumpsLeft--;           
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
        GD.Print(_timeToStartFallingCounter);
        MoveAndSlide();
    }
}