using Godot;
using System;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	[Signal]
	public delegate void PointEventHandler();
	[Signal]
	public delegate void LifeAddedEventHandler();
	[Signal]
	public delegate void LifeRemovedEventHandler();
	[Export]public int Speed = 400; // How fast the player will move (pixels/sec).

	Vector2 velocity = Vector2.Zero; // The player's movement vector.
	public Vector2 ScreenSize; // Size of the game window.
	// Called when the node enters the scene tree for the first time.
	int lives = 0;
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Hide();
	}

	public void Start(Vector2 pos)
	{
		Position = pos;
		 
		Show();
		// GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", false);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += (float)(delta);
			if(velocity.X>0.3)
			{
				velocity.X = 0.3f;
			}
		}
		else if(velocity.X>0)
		{
			velocity.X -=(float)delta;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= (float)(delta);
			if(velocity.X<-0.3f)
			{
				velocity.X = -0.3f;
			}
		}
		else if(velocity.X<0)
		{
			velocity.X +=(float)delta;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += (float)(delta);
			if(velocity.Y>0.3f)
			{
				velocity.Y = 0.3f;
			}
		}
		else if(velocity.Y>0)
		{
			velocity.Y -=(float)delta;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= (float)(delta);
			if(velocity.Y<-0.3f)
			{
				velocity.Y = -0.3f;
			}
		}
		else if(velocity.Y<0)
		{
			velocity.Y +=(float)delta;
		}

		if(Math.Abs(velocity.X)<(float)delta)
		{
			velocity.X = 0;
		}
		if(Math.Abs(velocity.Y)<(float)delta)
		{
			velocity.Y = 0;
		}
		
		var currentSpeed = Speed * Mathf.Max(Mathf.Abs(velocity.X),Mathf.Abs(velocity.Y)) * 5;

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var velocityLocal = velocity;

		// if (velocity.X != 0)
		// {
		// 	animatedSprite2D.Animation = "walk";
		// 	animatedSprite2D.FlipV = false;
		// 	// See the note below about boolean assignment.
		// 	animatedSprite2D.FlipH = velocity.X < 0;
		// }
		animatedSprite2D.Animation = "up";
		if(velocity.X !=0 || velocity.Y!=0)
		{
			float angle = Mathf.Atan2(velocity.X, -velocity.Y);
			Rotate(angle - Transform.Rotation);
		}

		if (velocityLocal.Length() > 0)
		{
			velocityLocal = velocityLocal.Normalized() * currentSpeed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		Position += velocityLocal * (float)delta ;
		Position = new Vector2(
		x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
		y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
	} 
	private void _on_body_entered(PhysicsBody2D body)
	{
		GD.Print("hit");
		if(body is mob)
		{
			if(lives<1)
			{

				Hide(); // Player disappears after being hit.
				EmitSignal(SignalName.Hit);
			}
			else
			{
				EmitSignal(SignalName.LifeRemoved);
				lives--;
				(body as mob).kill();
			}
		// Must be deferred as we can't change physics properties on a physics callback.
		// GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
		}
		else if(body is PowerUp)
		{
			if(lives<3)
			{
				EmitSignal(SignalName.LifeAdded);
				lives++;
			}
			else
			{
				EmitSignal(SignalName.Point);
			}
			(body as PowerUp).kill();
		}

	}

	
}


