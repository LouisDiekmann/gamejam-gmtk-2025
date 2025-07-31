using Godot;
using System;
[GlobalClass]
public partial class WeaponResource : Resource {
    [Export] public bool melee = false;
    [Export] public int ammo = 0;
    [Export] public string name;
    [Export] public Texture2D texture;
}
