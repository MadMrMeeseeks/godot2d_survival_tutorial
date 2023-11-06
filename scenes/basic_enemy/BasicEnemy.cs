using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D
{
	public int MaxSpeed = 75;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area2D area2D = GetNode<Area2D>("Area2D");
		area2D.Connect("area_entered", new Callable(this, "_OnAreaEntered"));

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var direction = GetDirectionToPlayer();
		Velocity = direction * MaxSpeed;
		MoveAndSlide();

	}

	private Vector2 GetDirectionToPlayer()
	{
		var playerNode = (Node2D)GetTree().GetFirstNodeInGroup("player");
		if (playerNode != null)
		{
			return (playerNode.GlobalPosition - GlobalPosition).Normalized();
		}

		return Vector2.Zero;

	}
	
	private void _OnAreaEntered(Area2D otherArea)
	{
		QueueFree();
	}
	
	
	
}
