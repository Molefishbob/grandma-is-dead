using Godot;
using System;
using System.Linq;

public partial class MapController : Node
{
	public enum Maps { MainMenu, CabinMap, GardenMap }

	[Export]
	public Node2D[] mapRooms;

	[Export]
	public Sprite2D fadingOverlay;
	
	[Export]
	public Timer fadeDelayTimer;

	[Export]
	public Maps currentMap = Maps.CabinMap;

	private bool hasFadedInStarted = false;
	private bool hasFadeOutStarted = false;
	private bool isFading = false;
	private float currentAlpha = 0f;

	public override void _Ready()
	{
		fadingOverlay.Modulate = new Color(1f, 1f, 1f, 0f);
		fadingOverlay.Visible = false;
		fadeDelayTimer.Timeout += () =>
		{
			hasFadeOutStarted = true;
			HandleMapChange();
		};

		HandleMapChange();
		base._Ready();
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.F) && !isFading)
		{
			currentMap = currentMap == Maps.CabinMap ? Maps.GardenMap : Maps.CabinMap;
			ChangeMap(currentMap);	
		}

		HandleFade();
	}

	private void ChangeMap(Maps newMap)
	{
		currentMap = newMap;
		FadeInStart();
	}

	private void HandleMapChange()
	{
		GD.Print($"Changing to map: {currentMap}");
		foreach (var room in mapRooms)
		{
			room.SetProcess(false);
			room.Visible = false;
			DisableColliders(room);
		}

		switch (currentMap)
		{
			case Maps.MainMenu:
				mapRooms[0].SetProcess(true);
				mapRooms[0].Visible = true;
				DisableColliders(mapRooms[0], false);
				break;
			case Maps.CabinMap:
				mapRooms[1].SetProcess(true);
				mapRooms[1].Visible = true;
				DisableColliders(mapRooms[1], false);
				break;
			case Maps.GardenMap:
				mapRooms[2].SetProcess(true);
				mapRooms[2].Visible = true;
				DisableColliders(mapRooms[2], false);
				break;
			default:
				break;
		}
	}

	private void DisableColliders(Node2D node, bool disable = true)
	{
		var colliders = node.FindChildren("*")
		.Where(n => n is CollisionShape2D or CollisionPolygon2D)
		.Where(c => c.GetGroups().FirstOrDefault(x => x.ToString().Contains("collider")) != null)
		.ToArray();
		foreach (var collider in colliders)
		{
			collider.Set("disabled", disable);
		}
	}

	private void FadeInStart()
	{
		if (hasFadedInStarted || hasFadeOutStarted)
			return;

		hasFadedInStarted = true;
		isFading = true;
		fadingOverlay.Modulate = new Color(0f, 0f, 0f, 0f);
		fadingOverlay.Visible = true;
	}

	private void HandleFade()
	{
		if (!hasFadedInStarted && !hasFadeOutStarted)
			return;

		if (hasFadedInStarted && currentAlpha <= 0.99f)
		{
			currentAlpha = Mathf.Lerp(currentAlpha, 1f, 0.1f);
			fadingOverlay.Modulate = new Color(1f, 1f, 1f, currentAlpha);
			fadingOverlay.Visible = true;
			return;
		}
		else if (hasFadedInStarted && currentAlpha >= 0.99f)
		{
			hasFadedInStarted = false;
			fadingOverlay.Modulate = new Color(1f, 1f, 1f, 1f);
			fadeDelayTimer.Start();
		}

		if (hasFadeOutStarted && currentAlpha >= 0.01f)
		{
			float goalAlpha = hasFadeOutStarted ? 0f : 1f;
			currentAlpha = Mathf.Lerp(currentAlpha, goalAlpha, 0.1f);
			fadingOverlay.Modulate = new Color(1f, 1f, 1f, currentAlpha);
			fadingOverlay.Visible = true;
		}
		else if (hasFadeOutStarted && currentAlpha <= 0.01f)
		{
			hasFadeOutStarted = false;
			isFading = false;
			fadingOverlay.Modulate = new Color(1f, 1f, 1f, 0f);
			fadingOverlay.Visible = false;
		}
	}
}
