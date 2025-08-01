using Godot;
using System;

public partial class Shooting : Node2D {
    [Export] private Vector2 shootingDir = new Vector2(1, 0);
    [Export] public WeaponResource weaponResource;
    private Area2D hurtbox;
    [Signal] public delegate void KillEventHandler();
    [Signal] public delegate void PunchEventHandler();

    public override void _Ready() {
        hurtbox = GetNode<Area2D>("Hurtbox");
    }

    public override void _PhysicsProcess(double delta) {
        attack();
        throwItem();
    }

    private void attack() {
        if (Input.IsActionJustPressed("Attack")) {
            if (weaponResource == null) {
                EmitSignal(SignalName.Punch);
            } else if (weaponResource.melee) {
                EmitSignal(SignalName.Kill);
            } else {
                if (weaponResource.ammo > 0) {
                    PackedScene projectileScene = GD.Load<PackedScene>("res://Scenes/projectile.tscn");
                    Projectile projectile = projectileScene.Instantiate<Projectile>();
                    projectile.Position = GlobalPosition;
                    projectile.Rotation = GlobalRotation;
                    GetTree().Root.AddChild(projectile);
                    weaponResource.ammo -= 1;
                }
            }
        }
    }

    private void throwItem() {
        if (Input.IsActionJustPressed("Throw") && weaponResource != null) {
            PackedScene weaponScene = GD.Load<PackedScene>("res://Scenes/weapon.tscn");
            Item weaponDump = weaponScene.Instantiate<Item>();
            weaponDump.weaponResource = weaponResource;
            weaponDump.Position = GlobalPosition;
            weaponDump.Rotation = GlobalRotation;
            GetTree().Root.AddChild(weaponDump);
            weaponResource = null;
        }
    }

    public void hurtable(Node2D body) {
        if (body.IsInGroup("Enemy")) {
            EnemyType enemy = (EnemyType)body;
            Kill += enemy.death;
            Punch += enemy.knockout;
        }
    }
    public void notHurtable(Node2D body) {
        if (body.IsInGroup("Enemy")) {
            EnemyType enemy = (EnemyType)body;
            Kill -= enemy.death;
            Punch -= enemy.knockout;
        }
    }

}
