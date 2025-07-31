using Godot;

namespace gamejamgmtk2025.Scripts;
[GlobalClass]
public partial class SettingsResource : Resource {
    [Export] public float MasterVolume = 0.5f;
    [Export] public float MusicVolume = 0.5f;
    [Export] public float EffectsVolume = 0.5f;
    [Export] public float Sensitivity = 1.0f;
}