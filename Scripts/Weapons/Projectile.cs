using System;
using Godot;

public partial class Projectile : Area2D {
    [Export] public float speed = 8;
    public bool controllable = false;
    public float rng = 0;
    private float oldLength = 100000;
    private Sprite2D sprite2D;
    private PointLight2D pointLight2D;


    public override void _Ready() {
        sprite2D = GetNode<Sprite2D>("Icon");
        pointLight2D = GetNode<PointLight2D>("PointLight2D");
    }

    public override void _PhysicsProcess(double delta) {
        Position += new Vector2(0, -speed).Rotated(Rotation);
        float turn = GetLocalMousePosition().Angle() + 90 * Mathf.Pi / 180;
        if (oldLength < (GetGlobalMousePosition() - Position).Length() && controllable) {
            if (turn < 1.5 && turn > -1.5) {
                Rotation += GetLocalMousePosition().Angle() + 90 * Mathf.Pi / 180 + rng;
            }
        }
        if (speed <= 0) {
            Sprite2D bullet = new Sprite2D();
            bullet.Texture = sprite2D.Texture;
            bullet.Rotation = Rotation;
            bullet.Position = sprite2D.GlobalPosition;
            bullet.Scale = sprite2D.Scale;
            GetTree().Root.AddChild(bullet);
            QueueFree();
        }
        oldLength = (GetGlobalMousePosition() - Position).Length();
        speed -= (float)delta;
        pointLight2D.Energy -= 0.125f*(float)delta;
    }

    public void hit(Node2D body) {
        if (body.IsInGroup("Enemy") && controllable) {
            EnemyType enemy = (EnemyType)body;
            enemy.death();
            QueueFree();
        }
        if (body.IsInGroup("Player")) {
            Player player = (Player)body;
            player.death();
            QueueFree();
        }
        QueueFree();
    }
}
