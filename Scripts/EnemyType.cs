using Godot;
using System;

public partial class EnemyType : RigidBody2D {
    private RayCast2D detectCast;
    private Vector2 goTo;
    private Vector2 ogPos;
    private double timer = 0;
    [Export] private float timeToGoBack = 5;
    [Export] private float speed = 0.01f;

    public override void _Ready() {
        detectCast = GetNode<RayCast2D>("DetectCast");
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
}
