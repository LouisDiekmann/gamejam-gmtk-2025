using Godot;
using System;
using gamejamgmtk2025.Scripts;

public partial class PauseMenu : CanvasLayer {
    private const String settingsDirectory = "user://resource/";
    private const String settingsPath = "user://resource/settings.tres";
    [Export] public Panel settingsPanel;
    
    [ExportGroup("Sounds")]
    [Export] public AudioStreamPlayer fourClicksSound;
    [Export] public AudioStreamPlayer synthErrorSound;
    [Export] public SettingsResource settingsResource;
    
    [ExportGroup("Settings Sliders")]
    [Export] public HSlider masterVolumeSlider;
    [Export] public HSlider musicVolumeSlider;
    [Export] public HSlider effectsVolumeSlider;
    [Export] public HSlider sensitivitySlider;
    public override void _Ready() {
        DirAccess.MakeDirAbsolute(settingsDirectory);
        if (FileAccess.FileExists(settingsPath)) {
            SettingsResource savedSettingsResource = GD.Load<SettingsResource>(settingsPath);
            settingsResource = savedSettingsResource;
        } else {
            saveSettings();
        }
        masterVolumeSlider.Value = settingsResource.MasterVolume;
        musicVolumeSlider.Value = settingsResource.MusicVolume;
        effectsVolumeSlider.Value = settingsResource.EffectsVolume;
        sensitivitySlider.Value = settingsResource.Sensitivity;
    }
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
    
    void _on_master_hslider_value_changed(float value) {
        settingsResource.MasterVolume = value;
    }
    void _on_music_hslider_value_changed(float value) {
        settingsResource.MusicVolume = value;
    }
    void _on_effects_hslider_value_changed(float value) {
        settingsResource.EffectsVolume = value;
    }
    void _on_sensitivity_hslider_value_changed(float value) {
        settingsResource.Sensitivity = value;
    }
    
    void saveSettings() {
        ResourceSaver.Save(settingsResource, settingsPath);
    }
}
