using Godot;
using System;

public partial class fase1 : Node
{
#pragma warning disable 649
	// We assign this in the editor, so we don't need the warning about not being assigned.
	[Export]
	public PackedScene MobScene;
	[Export]
	public PackedScene PowerUpScene;
#pragma warning restore 649

	public int Score;

	private bool shoudStart = false;
	private double startTime = 2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// NewGame();
		GD.Randomize();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GameOver()
	{
		GD.Print("game is over\n");
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<Hud>("HUD").ShowGameOver();
	}

	public void NewGame()
	{
		Score = 0;

		GetTree().CallGroup("Mobs", "queue_free");
		GetNode<Timer>("StartTimer").Start();
		var hud = GetNode<Hud>("HUD");
		hud.UpdateScore(Score);
		hud.ShowMessage("Get Ready!");
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);
		player.Show();
	}

	public void OnScoreTimerTimeout()
	{
		Score++;
		GetNode<Hud>("HUD").UpdateScore(Score);
	}

	public void OnStartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}
	public void OnMobTimerTimeout()
	{
		// Note: Normally it is best to use explicit types rather than the `var`
		// keyword. However, var is acceptable to use here because the types are
		// obviously Mob and PathFollow2D, since they appear later on the line.

		// Create a new instance of the Mob scene.
		if(GD.RandRange(0,20)>0.03)
		{
			var mobi = MobScene.Instantiate<mob>();

			// Choose a random location on Path2D.
			var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawLocation");
			mobSpawnLocation.ProgressRatio = GD.Randf();

			// Set the mob's direction perpendicular to the path direction.
			float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

			// Set the mob's position to a random location.
			mobi.Position = mobSpawnLocation.Position;

			// Add some randomness to the direction.
			direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
			mobi.Rotation = direction;

			// Choose the velocity.
			var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
			mobi.LinearVelocity = velocity.Rotated(direction);

			// Spawn the mob by adding it to the Main scene.
			AddChild(mobi);
		}
		else
		{
			var mobi = PowerUpScene.Instantiate<PowerUp>();

			// Choose a random location on Path2D.
			var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawLocation");
			mobSpawnLocation.ProgressRatio = GD.Randf();

			// Set the mob's direction perpendicular to the path direction.
			float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

			// Set the mob's position to a random location.
			mobi.Position = mobSpawnLocation.Position;

			// Add some randomness to the direction.
			direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
			mobi.Rotation = direction;

			// Choose the velocity.
			var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
			mobi.LinearVelocity = velocity.Rotated(direction);

			// Spawn the mob by adding it to the Main scene.
			AddChild(mobi);
		}
	}

	public void OnQuit()
	{
		GD.Print("on quit\n");
		GetTree().Quit(); 
	}
	public void OnPointAdded()
	{
		Score++;
		GetNode<Hud>("HUD").UpdateScore(Score);
	}
	public void onLifeAdded()
	{
		GetNode<Hud>("HUD").AddLife();
	}

		public void onLifeRemoved()
	{
		GetNode<Hud>("HUD").RemoveLife();
	}

}



