using Godot;
using System;

public partial class FloatingBody : CharacterBody2D
{
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public static float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() / 2f;
	public static float WaterFriction = 12f;
	public static float AirFriction = 0.6f;
	private bool InWater = false;

	public bool IsInWater()
	{
		return InWater;
	}

	public void SetInWater(bool inWater)
	{
		InWater = inWater;
	}
}
