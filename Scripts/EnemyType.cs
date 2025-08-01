using Godot;
using System;

public partial class EnemyType : RigidBody2D {

    private RayCast2D detectCast;

    private Vector2 goTo;

    public override void _Ready() {
        detectCast = GetNode<RayCast2D>("DetectCast");
        goTo = GlobalPosition;
    }

    public override void _PhysicsProcess(double delta) {
        findPlayer();
    }

    private void findPlayer() {
        detectCast.TargetPosition = ToLocal(Global.Instance.player.GlobalPosition);
        if (detectCast.GetCollider() == Global.Instance.player) {
            goTo = Global.Instance.player.GlobalPosition;
        }

        GlobalPosition = new Vector2(Mathf.Lerp(GlobalPosition.X, goTo.X, 0.01f), Mathf.Lerp(GlobalPosition.Y, goTo.Y, 0.01f));
        
    }
}
