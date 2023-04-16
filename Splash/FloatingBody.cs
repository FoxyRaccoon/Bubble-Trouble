using Godot;
using System;

public partial class FloatingBody : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private bool InWater = false;
	private static float WaterLevel = -1780;
	private static float WaterPush = -12f;
	public static float WaterFriction = 12f;
	public static float AirFriction = 0.6f;
	private bool DashUsed = false;

	private void WaterPhysics1(double delta){
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction.X != 0){
			if(!IsInWater()){
				velocity.X += direction.X * Speed * (float)delta * 0.5f;
			}else{
				velocity.X += direction.X * Speed * (float)delta * 2f;
			}
			
		}else{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed * (float)delta * 2f);
		}

		if (!IsInWater()){
			velocity.Y += gravity * (float)delta;
		}else{
			if(Position.Y > WaterLevel){
				velocity.Y += Position.Y * (float)delta * 0.2f;
			}
			if(direction.Y != 0){
				velocity.Y += direction.Y * Speed * (float)delta * 2f;
			}

		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void WaterPhysics2(double delta){
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		Vector2 dashVect = new Vector2(0, 0);
		if(Input.IsActionJustPressed("dash")){
			if(IsInWater() || !DashUsed){
				dashVect = direction * 10f * Speed;
				DashUsed = true;
			}
		}
		if(IsInWater()){
			DashUsed = false;
		}
		if (direction.X != 0){
			velocity.X = direction.X * Speed;
			
		}else{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		if (!IsInWater()){
			velocity.Y += gravity * (float)delta;
		}else{
			velocity.Y = velocity.Y -= WaterPush;
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed * (float)delta);
			if(direction.Y != 0){
				velocity.Y = direction.Y * Speed;
			}
		}
		velocity += dashVect;

		if(IsInWater() && Position.Y > WaterLevel){
			Velocity = velocity + new Vector2(0, WaterPush);
		}else{
			Velocity = velocity;
		}
		MoveAndSlide();
	}

	private void WaterPhysics3(double delta){
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
	
		if(!IsInWater()){
			velocity.Y += gravity * (float)delta;
		}else{
			if(direction.Y != 0){
				velocity.Y = direction.Y * Speed;
			}else{
				velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed * (float)delta);
			}
			if(Position.Y > WaterLevel){
				velocity.Y += Position.Y * (float)delta * 0.2f;
			}
		}
		MoveAndSlide();
	}

	public bool IsInWater()
	{
		return InWater;
	}

	public void SetInWater(bool inWater)
	{
		InWater = inWater;
		// Velocity = new Vector2(Velocity.X, Velocity.Y/2f);
	}
}
