using Godot;
using System;

public partial class Shooting : Node2D
{
    [Export] private Vector2 shootingDir = new Vector2(1, 0);
    
    public override void _PhysicsProcess(double delta) {
        if (Input.IsActionJustPressed("Attack")) {
            PackedScene projectileScene = GD.Load<PackedScene>("res://Scenes/projectile.tscn");
            Projectile projectile = projectileScene.Instantiate<Projectile>();
            projectile.Position = GlobalPosition;
            projectile.Rotation = GlobalRotation;
            GetTree().Root.AddChild(projectile);
            
        }
    }
}
