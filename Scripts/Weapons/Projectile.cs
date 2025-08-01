using Godot;
using System;

public partial class Projectile : Area2D {
    [Export] public float speed = 16;
    [Export] public float ttl = 5;

    private double time = 0;

    public override void _PhysicsProcess(double delta) {
        time += delta;
        Position += new Vector2(0, -speed).Rotated(Rotation);
        if (ttl < time) {
            QueueFree();
        }
    }
    
    public void hit(Node2D body) {
        if (body.IsInGroup("Enemy")) {
            EnemyType enemy = (EnemyType)body;
            enemy.death();
            QueueFree();
        }
        if (body.IsInGroup("Player")) {
            Player player = (Player)body;
            player.death();
            QueueFree();
        }
    }
}
