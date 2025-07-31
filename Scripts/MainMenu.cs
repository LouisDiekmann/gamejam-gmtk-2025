using Godot;
using System;
using System.Net;
using gamejamgmtk2025.Scripts;
public partial class MainMenu : Control {
    private const String settingsDirectory = "user://resource/";
    private const String settingsPath = "user://resource/settings.tres";
    private const String level1ScenePath = "res://Scenes/level.tscn";
    
    private ResourceLoader.ThreadLoadStatus scenceLoadProgress = 0.0f;
    
    [ExportGroup("Panels")]
    [Export] public SettingsResource settingsResource;
    [Export] public Panel settingsPanel;
    [Export] public Panel creditsPanel;
    [Export] public ColorRect loadingScreen;
    
    [ExportGroup("Settings Sliders")]
    [Export] public HSlider masterVolumeSlider;
    [Export] public HSlider musicVolumeSlider;
    [Export] public HSlider effectsVolumeSlider;
    [Export] public HSlider sensitivitySlider;
    
    [ExportGroup("Sounds")]
    [Export] public AudioStreamPlayer fourClicksSound;
    [Export] public AudioStreamPlayer synthErrorSound;

    
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
    public override void _Process(double delta) {
        scenceLoadProgress = ResourceLoader.LoadThreadedGetStatus(level1ScenePath);
        if (scenceLoadProgress == ResourceLoader.ThreadLoadStatus.Loaded) {
            Resource newScene = ResourceLoader.LoadThreadedGet(level1ScenePath);
            GetTree().ChangeSceneToPacked(newScene as PackedScene);
        }
    }
    void _on_play_button_down() {
        loadingScreen.Show();
        play4ClicksSound();
        ResourceLoader.LoadThreadedRequest(level1ScenePath);
    }
    void _on_settings_button_down() {
        play4ClicksSound();
        settingsPanel.Visible = !settingsPanel.Visible;
    }
    void _on_credits_button_down() {
        play4ClicksSound();
        creditsPanel.Visible = !creditsPanel.Visible;
    }
    void saveSettings() {
        ResourceSaver.Save(settingsResource, settingsPath);
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

    void _on_open_level_data_button_down() {
        String localPath = OS.GetUserDataDir() + "/resource";
        OS.ShellShowInFileManager(localPath, true);
    }

    void _on_hslider_drag_ended(bool value_changed) {
        saveSettings();
    }
}
