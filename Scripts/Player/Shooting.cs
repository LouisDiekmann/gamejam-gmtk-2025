using Godot;
using System;

public partial class Shooting : Node2D {
    [Export] private Vector2 shootingDir = new Vector2(1, 0);
    [Export] public WeaponResource weaponResource;
    private Area2D hurtbox;
    [Signal] public delegate void KillEventHandler();
    [Signal] public delegate void PunchEventHandler();
    private double timer = 12;
    private Sprite2D bat;
    private Sprite2D gun;
    private Sprite2D gun2;
    private Sprite2D gun3;

    public override void _Ready() {
        hurtbox = GetNode<Area2D>("Hurtbox");
        bat = GetNode<Sprite2D>("Bat");
        gun = GetNode<Sprite2D>("Gun");
        gun2 = GetNode<Sprite2D>("Gun2");
        gun3 = GetNode<Sprite2D>("Gun3");
    }

    public override void _PhysicsProcess(double delta) {
        attackStyle(delta);
        throwItem();
        setItem();
    }

    private void attackStyle(double delta) {
        if (weaponResource == null) {
            if (Input.IsActionJustPressed("Attack")) {
                //EmitSignal(SignalName.Punch);
                EmitSignal(SignalName.Kill);
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
        float spreadRng = (float)((rng.NextDouble() * 2.0) - 1.0) * weaponResource.spread;
        projectile.Rotation = GlobalRotation + spreadRng;
        projectile.rng = spreadRng;
        projectile.controllable = true;
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

    private void setItem() {
        if (weaponResource == null) {
            bat.Visible = true;
            gun.Visible = false;
            gun2.Visible = false;
            gun3.Visible = false;
        } else {
            switch (weaponResource.name) {
                case "autoStapler":
                    bat.Visible = false;
                    gun.Visible = true;
                    gun2.Visible = false;
                    gun3.Visible = false;
                    break;
                case "doubleBarrel":
                    bat.Visible = false;
                    gun.Visible = false;
                    gun2.Visible = true;
                    gun3.Visible = false;
                    break;
                case "stapler":
                    bat.Visible = false;
                    gun.Visible = false;
                    gun2.Visible = false;
                    gun3.Visible = true;
                    break;
            }
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
