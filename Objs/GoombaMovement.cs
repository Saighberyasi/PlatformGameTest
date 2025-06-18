using Godot;
using System;

public partial class GoombaMovement : CharacterBody2D
{
	[Export] private Vector2 _walkDirection = new Vector2(1, 0);
	[Export] private float _moveSpeed = 300f;
	[Export] private float _gravity = 800f;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = _walkDirection.Normalized() * _moveSpeed;
		Velocity = velocity;
		MoveAndSlide();
		
		if (!IsOnFloor())
		{
			velocity.Y += _gravity * (float)delta;
		}

		if (IsOnWall())
		{
			_walkDirection = _walkDirection * -1;
		}
	}
}
