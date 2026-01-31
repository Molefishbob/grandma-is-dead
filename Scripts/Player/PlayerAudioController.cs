using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerAudioController : AudioStreamPlayer
{
	[Export]
	public Resource[] sounds = [];

	public Resource currentSound = null;

	public override void _Ready()
	{
		currentSound = sounds.Length > 0 ? sounds[0] : null;
		base._Ready();
	}

	public override void _Process(double delta)
	{
	}

	public void PlaySound()
	{
		if (!IsPlaying() && currentSound != null)
		{
			Stream.ResourcePath = currentSound.ResourcePath;
			Play();
		}
	}
}
