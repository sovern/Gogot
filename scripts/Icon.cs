using Godot;
using System;

public partial class Icon : Node2D
{
	double timePassed;
	double SPEED = 100;

	private const float ROTATION_SPEED = 15.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timePassed = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) 
	{
		timePassed +=delta;
		// Check if the left or right arrow key is pressed
        if (Input.IsActionPressed("ui_left"))
        {
            // Move the object to the left
            Position -= new Vector2((float)(SPEED * delta), 0);
			GD.Print("Move left\n");

        }
        else if (Input.IsActionPressed("ui_right"))
        {
            // Move the object to the right
            Position += new Vector2((float)(SPEED * delta), 0);
			GD.Print("Move rigth\n");
        }
      if (Input.IsKeyPressed(Key.Q))
        {
            // Rotate the object counterclockwise by 45 degrees
            Rotation -= ROTATION_SPEED *(float)delta;
        }
        // Check if the "e" key is pressed
        else if (Input.IsKeyPressed(Key.E))
        {
            // Rotate the object clockwise by 45 degrees
            Rotation += ROTATION_SPEED * (float)delta;
        }
	}
}
