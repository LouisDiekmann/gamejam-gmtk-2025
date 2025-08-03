using Godot;
using System;

public partial class Player : CharacterBody2D {
    [Export] private int speed = 100;
    [Export] private float acceleration = 0.1f;
    [Export] private float legLerpFactor = 0.2f;
    [Export] public Shooting shooting;
    [Export] public AnimationPlayer animationPlayer;
    [Export] public FootStepsSound footSteps;

    private Node2D body;
    private Node2D legs;
    private Area2D mouse;

    public override void _Ready() {
        body = GetNode<Node2D>("Body");
        legs = GetNode<Node2D>("Legs");
        Global.Instance.player = this;
        mouse = GetNode<Area2D>("Mouse");
    }

    public void GetInput() {
        body.LookAt(GetGlobalMousePosition());
        //body.Rotate(90 * Mathf.Pi / 180);
        Vector2 inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
        if (inputDirection != Vector2.Zero) {
            Velocity = new Vector2(Mathf.Lerp(Velocity.X, inputDirection.X * speed, acceleration), Mathf.Lerp(Velocity.Y, inputDirection.Y * speed, acceleration));
            legs.Rotation = Mathf.LerpAngle(legs.Rotation, inputDirection.Angle(), legLerpFactor);
            FootStepsSound.playing = true;
            animationPlayer.Play("walk");
        } else {
            Velocity = new Vector2(Mathf.Lerp(Velocity.X, 0, acceleration), Mathf.Lerp(Velocity.Y, 0, acceleration));
            FootStepsSound.playing = false;
            animationPlayer.Stop();
        }

    }

    public override void _PhysicsProcess(double delta) {
        GetInput();
        MoveAndSlide();
        mouse.GlobalPosition = GetGlobalMousePosition();
    }

    public void death() {
        GetTree().ReloadCurrentScene();
    }
    
}
