using Godot;
using System;


public partial class Player : CharacterBody2D
{

	private int _maxSpeed = 150;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var movementVector = GetMovementVector();
		var direction = movementVector.Normalized();
		Velocity = direction * _maxSpeed;
		MoveAndSlide();
	}

	private Vector2 GetMovementVector()
	{
		// left pressed and right not -1 ; right pressed and left not 1 ; both pressed 0
		var xMovement = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		
		// up pressed and down not -1 ; down pressed and up not 1 ; both pressed 0
		var yMovement = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

		return new Vector2(xMovement, yMovement);

	}
}
 
