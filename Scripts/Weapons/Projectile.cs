using Godot;
using System;

public partial class Projectile : Area2D
{
    [Export] public float speed = 8;
    [Export] public float ttl = 5;

    private double time = 0;

    public override void _PhysicsProcess(double delta) {
        time += delta;
        Position += new Vector2(0, -speed).Rotated(Rotation);
        if (ttl < time) {
            QueueFree();
        }
    }
}
