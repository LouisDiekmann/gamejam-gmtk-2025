using Godot;
using System;

public partial class Level : Node2D {
    [Export] private CanvasLayer pauseMenu;

    public override void _Process(double delta) {
        if (Input.IsActionJustPressed("Escape")) {
            pauseMenu.Visible = !pauseMenu.Visible;
            GetTree().Paused = pauseMenu.Visible;
        }
    }
}
