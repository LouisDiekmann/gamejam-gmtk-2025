using Godot;
using System;
using Range = Godot.Range;

public partial class FootStepsSound : Node {
    [Export(PropertyHint.Range, "0,1")] public float stepInterval { get; set; } = 0.5f;
    private AudioStreamPlayer step1;
    private AudioStreamPlayer step2;
    private AudioStreamPlayer step3;
    private AudioStreamPlayer step4;
    private AudioStreamPlayer step5;
    private AudioStreamPlayer step6;
    private AudioStreamPlayer step7;
    
    private AudioStreamPlayer[] steps;
    
    static public bool playing = false;
    private float timePassed = 0.0f;
    
    public override void _Ready() {
        step1 = GetNode<AudioStreamPlayer>("step1");
        step2 = GetNode<AudioStreamPlayer>("step2");
        step3 = GetNode<AudioStreamPlayer>("step3");
        step4 = GetNode<AudioStreamPlayer>("step4");
        step5 = GetNode<AudioStreamPlayer>("step5");
        step6 = GetNode<AudioStreamPlayer>("step6");
        step7 = GetNode<AudioStreamPlayer>("step7");
        
        steps = new AudioStreamPlayer[] { step1, step2, step3, step4, step5, step6, step7 };
    }
    
    public override void _Process(double delta) {
        timePassed += (float)delta;
        if (timePassed >= stepInterval) {
            timePassed = 0.0f;
            if (playing) {
                PlayFootStepSound();
            }
        }
    }
    
    public void PlayFootStepSound() {
        int randomIndex = (int)GD.RandRange(0, steps.Length - 1);
        AudioStreamPlayer selectedStep = steps[randomIndex];
        
        selectedStep.PitchScale = (float)GD.RandRange(0.8, 1.2);
        selectedStep.Play();
    }
}
