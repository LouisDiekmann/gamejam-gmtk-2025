using Godot;
using System;
[GlobalClass]
public partial class WeaponResource : Resource {
    [Export] public string name;
    [Export] public bool melee = false;
    [Export] public bool auto = false;
    [Export] public float firerate = 0;
    [Export] public float spread = 0;
    [Export] public int pellets = 0;
    [Export] public int ammo = 0;
    [Export] public Texture2D texture;
}
