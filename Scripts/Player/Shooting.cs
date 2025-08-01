using Godot;
using System;

public partial class Shooting : Node2D {
    [Export] private Vector2 shootingDir = new Vector2(1, 0);
    [Export] public WeaponResource weaponResource;

    public override void _PhysicsProcess(double delta) {
        attack();
        throwItem();
    }

    private void attack() {
        if (Input.IsActionJustPressed("Attack")) {
            if (weaponResource == null) {

            } else if (weaponResource.melee) {

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

}
