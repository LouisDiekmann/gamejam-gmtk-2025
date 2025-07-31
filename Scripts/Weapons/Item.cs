using Godot;
using System;

public partial class Item : Node2D {
    [Export] public WeaponResource weaponResource;

    private Sprite2D sprite2D;

    public override void _Ready() {
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        sprite2D.Texture = weaponResource.texture;
    }


    public override void _PhysicsProcess(double delta) {
        pickupItem();
    }

    
    private void pickupItem() {
        if (Input.IsActionJustPressed("Throw") && Global.Instance.player.shooting.weaponResource == null) {
            if ((Global.Instance.player.GlobalPosition - GlobalPosition).Length() < 10) {
                Global.Instance.player.shooting.weaponResource = weaponResource;
                QueueFree();
            }
        }
    }

}
