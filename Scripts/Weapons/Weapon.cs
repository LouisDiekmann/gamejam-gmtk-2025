using Godot;
using System;
[GlobalClass]
public partial class Weapon : Resource {
    [Export] public bool melee = false;
    [Export] public int ammo = 0;
    [Export] public String name;
}
