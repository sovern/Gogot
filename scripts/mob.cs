using Godot;
using System;

public partial class mob : RigidBody2D
{
  private double _quitTime = 1;
  private bool _kill = false;
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    var animSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    animSprite2D.Play();
    string[] mobTypes = animSprite2D.SpriteFrames.GetAnimationNames();
    animSprite2D.Animation = mobTypes[GD.Randi() % mobTypes.Length];
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
    if(_kill)
    {
      _quitTime-=delta;
      if(_quitTime<=0)
      {
        QueueFree();
      }
    }
  }
  private void _on_visible_on_screen_notifier_2d_screen_exited()
  {
    _kill = true;
  }
  public void kill() // kill it now
  {
    _kill = true;
    _quitTime = 0;
  }
}


