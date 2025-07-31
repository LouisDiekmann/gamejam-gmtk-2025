using Godot;
using System;

public partial class Player : CharacterBody2D {
    [Export] private int speed = 100;
    [Export] private float acceleration = 0.1f;

    private Node2D body;

    public override void _Ready() {
        body = GetNode<Node2D>("Body");
    }

    public void GetInput() {
        body.LookAt(GetGlobalMousePosition());
        body.Rotate(90 * Mathf.Pi / 180);
        Vector2 inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
        if (inputDirection != Vector2.Zero) {
            Velocity = new Vector2(Mathf.Lerp(Velocity.X, inputDirection.X * speed, acceleration), Mathf.Lerp(Velocity.Y, inputDirection.Y * speed, acceleration));
        }
        else {
            Velocity = new Vector2(Mathf.Lerp(Velocity.X, 0, acceleration), Mathf.Lerp(Velocity.Y, 0, acceleration));
        }
    }

    public override void _PhysicsProcess(double delta) {
        GetInput();
        MoveAndSlide();
    }
}
