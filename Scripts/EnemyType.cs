using Godot;
using System;

public partial class EnemyType : RigidBody2D {
    private RayCast2D detectCast;
    private Vector2 goTo;
    private Vector2 ogPos;
    private double timer = 0;
    private double attackTimer = 0;
    private Node2D shootPos;
    [Export] private float fireRate = 0.5f;
    [Export] private float timeToGoBack = 5;
    [Export] private float speed = 0.01f;
    [Export] public WeaponResource weaponResource;

    public override void _Ready() {
        detectCast = GetNode<RayCast2D>("DetectCast");
        shootPos = GetNode<Node2D>("ShootPos");
        goTo = GlobalPosition;
        ogPos = GlobalPosition;
    }

    public override void _PhysicsProcess(double delta) {
        findPlayer(delta);
    }

    private void findPlayer(double delta) {
        detectCast.TargetPosition = ToLocal(Global.Instance.player.GlobalPosition);
        if (detectCast.GetCollider() == Global.Instance.player) {
            goTo = Global.Instance.player.GlobalPosition;
            LookAt(Global.Instance.player.GlobalPosition);
            attackPlayer(delta);
        }
        GlobalPosition = new Vector2(Mathf.Lerp(GlobalPosition.X, goTo.X, speed), Mathf.Lerp(GlobalPosition.Y, goTo.Y, speed));
        if (GlobalPosition.Round() == goTo.Round()) {
            timer += delta;
        }
        if (timer > timeToGoBack) {
            goTo = ogPos;
            timer = 0;
            LookAt(ogPos);
        }
    }

    private void attackPlayer(double delta) {
        if (weaponResource.ammo > 0 && attackTimer > fireRate) {
            PackedScene projectileScene = GD.Load<PackedScene>("res://Scenes/projectile.tscn");
            Projectile projectile = projectileScene.Instantiate<Projectile>();
            projectile.Position = shootPos.GlobalPosition;
            projectile.Rotation = GlobalRotation + 90 * Mathf.Pi / 180;
            GetTree().Root.AddChild(projectile);
            weaponResource.ammo -= 1;
            attackTimer = 0;
        } else {
            attackTimer += delta;
        }
    }

    public void death() {
        PackedScene weaponScene = GD.Load<PackedScene>("res://Scenes/weapon.tscn");
        Item weaponDump = weaponScene.Instantiate<Item>();
        weaponDump.weaponResource = weaponResource;
        weaponDump.Position = GlobalPosition;
        weaponDump.Rotation = GlobalRotation;
        GetTree().Root.AddChild(weaponDump);
        QueueFree();
    }
}
