using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Signal]
    public delegate void StartGameEventHandler();

	[Signal]
    public delegate void QuitEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();

		GetNode<Timer>("MessageTimer").Start();
	}
	async public void ShowGameOver()
	{
		ShowMessage("Game Over");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, "timeout");

		var message = GetNode<Label>("Message");
		message.Text = "Dodge the\nCreeps!";
		message.Show();

		await ToSignal(GetTree().CreateTimer(1), "timeout");
		GetNode<Button>("StartButton").Show();
	}
	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	public void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		(GetNode<LifeCounter>("LifeCounter")).zeroLifes();
		
		EmitSignal(SignalName.StartGame);
	}

	public void OnMessageTimerTimeout()
	{
		GetNode<Label>("Message").Hide();
	}

	public void OnQuitButtonsPressed()
	{
		EmitSignal(SignalName.Quit);
	}

	public void AddLife()
	{
		(GetNode<LifeCounter>("LifeCounter")).AddLife();
	}

	public void RemoveLife()
	{
		(GetNode<LifeCounter>("LifeCounter")).RemoveLife();
	}
}
