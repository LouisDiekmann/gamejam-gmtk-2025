using Godot;
using System;

public partial class Shooting : Node2D {
    [Export] private Vector2 shootingDir = new Vector2(1, 0);
    [Export] public WeaponResource weaponResource;
    private Area2D hurtbox;
    [Signal] public delegate void KillEventHandler();
    [Signal] public delegate void PunchEventHandler();
    private double timer = 12;

    public override void _Ready() {
        hurtbox = GetNode<Area2D>("Hurtbox");
    }

    public override void _PhysicsProcess(double delta) {
        attackStyle(delta);
        throwItem();
    }

    private void attackStyle(double delta) {
        if (weaponResource == null) {
            if (Input.IsActionJustPressed("Attack")) {
                EmitSignal(SignalName.Punch);
            }
        } else if (weaponResource.auto) {
            if (Input.IsActionPressed("Attack")) {
                attack(delta);
            }
        } else {
            if (Input.IsActionJustPressed("Attack")) {
                attack(delta);
            }
        }
        timer += delta;
    }

    private void attack(double delta) {
        if (timer > weaponResource.firerate) {
            if (weaponResource.melee) {
                EmitSignal(SignalName.Kill);
            } else if (weaponResource.ammo > 0) {
                if (weaponResource.pellets > 0) {
                    for (int i = 0; i < weaponResource.pellets; ++i) {
                        makeBullet();
                    }
                } else {
                    makeBullet();
                }
                weaponResource.ammo -= 1;
            }
            timer = 0;
        }
    }

    private void makeBullet() {
        PackedScene projectileScene = GD.Load<PackedScene>("res://Scenes/projectile.tscn");
        Projectile projectile = projectileScene.Instantiate<Projectile>();
        projectile.Position = GlobalPosition;
        Random rng = new Random();
        projectile.Rotation = GlobalRotation + (float)((rng.NextDouble() * 2.0) - 1.0)*weaponResource.spread;
        GetTree().Root.AddChild(projectile);
        
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
