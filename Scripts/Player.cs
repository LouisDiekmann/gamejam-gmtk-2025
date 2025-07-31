using Godot;
using System;

public partial class Player : CharacterBody2D {
    [Export] public int Speed { get; set; } = 400;
    private float acceleration = 0.01f;

    public void GetInput(double delta) {
        Vector2 inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
        if (inputDirection != Vector2.Zero) {
            Velocity = inputDirection * Speed;
            Velocity.Lerp(Velocity, acceleration);
        }
        else {
        
        
            
        }
    }

    public override void _PhysicsProcess(double delta) {
        GetInput(delta);
        
        GD.Print(Velocity);
        MoveAndSlide();
    }
}
