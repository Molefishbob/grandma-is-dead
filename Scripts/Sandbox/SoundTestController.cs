using Godot;

public partial class SoundTestController : Node2D
{
	private AudioStreamPlayer _audioPlayer;
	private HSlider _volumeSlider;
	private HSlider _pitchSlider;
	private Label _volumeLabel;
	private Label _pitchLabel;
	
	public override void _Ready()
	{
		// Create an audio stream player for playing sounds
		_audioPlayer = new AudioStreamPlayer();
		AddChild(_audioPlayer);
		
		// Set default values
		_audioPlayer.VolumeDb = 0.0f; // 0dB = full volume
		_audioPlayer.PitchScale = 1.0f; // 1.0 = normal pitch
		
		// Set up UI
		SetupUI();
		
		// Set up clickable items
		SetupSoundItems();
	}
	
	private void SetupUI()
	{
		var control = new Control();
		AddChild(control);
		
		var vbox = new VBoxContainer();
		vbox.Position = new Vector2(100, 50);
		control.AddChild(vbox);
		
		// Volume control
		_volumeLabel = new Label();
		_volumeLabel.Text = "Volume: 0 dB";
		vbox.AddChild(_volumeLabel);
		
		_volumeSlider = new HSlider();
		_volumeSlider.MinValue = -80.0f; // -80dB to 24dB range
		_volumeSlider.MaxValue = 24.0f;
		_volumeSlider.Value = 0.0f;
		_volumeSlider.Step = 1.0f;
		_volumeSlider.ValueChanged += OnVolumeChanged;
		vbox.AddChild(_volumeSlider);
		
		// Pitch control
		_pitchLabel = new Label();
		_pitchLabel.Text = "Pitch: 1.0";
		vbox.AddChild(_pitchLabel);
		
		_pitchSlider = new HSlider();
		_pitchSlider.MinValue = 0.1f; // 0.1 to 4.0 range
		_pitchSlider.MaxValue = 4.0f;
		_pitchSlider.Value = 1.0f;
		_pitchSlider.Step = 0.1f;
		_pitchSlider.ValueChanged += OnPitchChanged;
		vbox.AddChild(_pitchSlider);
	}
	
	private void OnVolumeChanged(double value)
	{
		_audioPlayer.VolumeDb = (float)value;
		_volumeLabel.Text = $"Volume: {value:F1} dB";
	}
	
	private void OnPitchChanged(double value)
	{
		_audioPlayer.PitchScale = (float)value;
		_pitchLabel.Text = $"Pitch: {value:F1}";
	}
	
	private void SetupSoundItems()
	{
		// Create buttons for each sound
		var sounds = new[]
		{
			("Blueberry Plop", "res://Assets/SFX/BlueberryPlop.wav"),
			("Fruit Pick", "res://Assets/SFX/FruitPick.wav"),
			("Herb Rustle", "res://Assets/SFX/HerbRustle.wav"),
			("Recipe Scribble", "res://Assets/SFX/RecipeScribble.wav")
		};
		
		var vbox = new VBoxContainer();
		vbox.Position = new Vector2(100, 200); // Moved down to make room for controls
		AddChild(vbox);
		
		foreach (var (name, path) in sounds)
		{
			var button = new Button();
			button.Text = name;
			button.SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter;
			
			// Connect button to play sound
			button.Pressed += () => PlaySound(path);
			
			vbox.AddChild(button);
		}
	}
	
	private void PlaySound(string soundPath)
	{
		var stream = GD.Load<AudioStream>(soundPath);
		if (stream != null)
		{
			_audioPlayer.Stream = stream;
			_audioPlayer.Play();
			GD.Print($"Playing sound: {soundPath}");
		}
		else
		{
			GD.PrintErr($"Failed to load sound: {soundPath}");
		}
	}
}
