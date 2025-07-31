using Godot;
using System;

public partial class Projectile : Area2D
{
    [Export] public float speed = 20;

    public override void _PhysicsProcess(double delta) {
        Position += new Vector2(0, -speed).Rotated(Rotation);
    }
}
