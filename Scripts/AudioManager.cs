using Godot;
using System;
using gamejamgmtk2025.Scripts;

public partial class AudioManager : Node {
    private const String settingsDirectory = "user://resource/";
    private const String settingsPath = "user://resource/settings.tres";
    [Export] public SettingsResource settingsResource;
    public override void _Ready() {
        DirAccess.MakeDirAbsolute(settingsDirectory);
        loadSettings(); 
        setBusVolumes();
    }
    void saveSettings() {
        ResourceSaver.Save(settingsResource, settingsPath);
    }
    void loadSettings() {
        if (FileAccess.FileExists(settingsPath)) {
            SettingsResource savedSettingsResource = GD.Load<SettingsResource>(settingsPath);
            settingsResource = savedSettingsResource;
        } else {
            saveSettings();
        }
    }
    void setBusVolumes() {
        AudioServer.SetBusVolumeLinear(0, settingsResource.MasterVolume);
        //AudioServer.SetBusVolumeDb(0, settingsResource.MasterVolume - 100);
        AudioServer.SetBusVolumeDb(1, settingsResource.MusicVolume - 100);
        AudioServer.SetBusVolumeDb(2, settingsResource.EffectsVolume - 100);
    }
    public void _on_settings_volume_changed(float value) {
        loadSettings();
        setBusVolumes();
    }

    
}
