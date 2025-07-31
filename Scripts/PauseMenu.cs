using Godot;
using System;

public partial class PauseMenu : CanvasLayer {
    [Export] public Panel settingsPanel;
    
    [ExportGroup("Sounds")]
    [Export] public AudioStreamPlayer fourClicksSound;
    [Export] public AudioStreamPlayer synthErrorSound;
    void _on_resume_button_down() {
        GetTree().Paused = false;
        Visible = false;
    }
    void _on_settings_button_down() {
        play4ClicksSound();
        settingsPanel.Visible = !settingsPanel.Visible;
    }
    void play4ClicksSound() {
        fourClicksSound.PitchScale = (float)GD.RandRange(0.8, 1.2);
        fourClicksSound.Play();
    }
}
