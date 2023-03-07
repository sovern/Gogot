using Godot;
using System;

public partial class LifeCounter : Node2D
{
	int currentLives = 0;
	public void AddLife()
	{
		if(currentLives<3)
		{
			currentLives++;
			GetNode<Sprite2D>("H"+currentLives).Show();
		}

	}
	public void RemoveLife()
	{
		if(currentLives>0)
		{
			GetNode<Sprite2D>("H"+currentLives).Hide();
			currentLives--;
			
		}
	}
	public void zeroLifes()
	{
		currentLives = 0;
		GetNode<Sprite2D>("H1").Hide();
		GetNode<Sprite2D>("H2").Hide();
		GetNode<Sprite2D>("H3").Hide();
	}
}
