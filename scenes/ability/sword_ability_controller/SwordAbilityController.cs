using Godot;
using System.Linq;
using System;



namespace dSurvivorsGame.scenes.ability.sword_ability_controller;

public partial class SwordAbilityController : Node
{
	private const float MaxRange = 150;
	private Random _random = new Random();
	
	[Export] private PackedScene _swordAbilityScene;

	private Timer _timer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimerTimeout;
	}

		private void OnTimerTimeout()
		{
			var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
			if (player == null)
			{
				return;
			}
			
			var enemies = GetTree().GetNodesInGroup("enemy").ToList();
			
				// This will filter out all enemies that are greater than 150 pixels from the player
			var sortedEnemies = enemies.ConvertAll(e => (Node2D)e)
				.Where(e => e.GlobalPosition.DistanceSquaredTo(player.GlobalPosition) < Mathf.Pow(MaxRange, 2)).ToList();
			
			if (sortedEnemies.Count <= 0)
			{
				return;
			}
			
			
			var swordAbilityInstance = _swordAbilityScene.Instantiate() as SwordAbility;
			
			player.GetParent().AddChild(swordAbilityInstance);
			swordAbilityInstance.GlobalPosition = sortedEnemies[0].GlobalPosition;
			
			const float tau = (float)(2 * Math.PI);
			float randomAngle = (float)(_random.NextDouble() * tau);
			
			swordAbilityInstance.GlobalPosition += Vector2.Right.Rotated(randomAngle) * 4;

			var enemyDirection = sortedEnemies[0].GlobalPosition - swordAbilityInstance.GlobalPosition;
			swordAbilityInstance.Rotation = enemyDirection.Angle();
		}




	
}

