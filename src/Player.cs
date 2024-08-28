using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody3D{
	private List<Skill> pow; 
	private float health;
	private float critChance;
	private float critMult;
	private float atkspd;
	private ushort level = 1;
    private float movspeed = 5.0f;


	public override void _Ready(){
		atkspd = 1.0f;
		health = 100.0f;
		critChance = 0.05f;
		critMult = 1.15f;
	}

	public override void _PhysicsProcess(double delta){
		var direction = Vector3.Zero;
        if (Input.IsActionPressed("space")){
            direction.Y += 1;
        }
        if (Input.IsActionPressed("right")){
            direction.X += 1;
        }
        if (Input.IsActionPressed("left")){
            direction.X -= 1;
        }
        if (Input.IsActionPressed("up")){
            direction.Z += 1;
        }
        if (Input.IsActionPressed("down")){
            direction.Z -= 1;
        }

        if (direction != Vector3.Zero){
            direction = direction.Normalized();

        }

        MoveAndCollide((float)delta * direction * movspeed);
	}

	public void AddCharacterSkill(Skill s){
		pow.Add(s);
		ProcEffect(s, 1);
	}
	
	public void RemoveCharacterSkill(Skill s){
		pow.Remove(s);
		ProcEffect(s, -1);
	}


	private void ProcEffect(Skill s, int sign){
		switch(s){
			case Skill.S_CRITCHANCE:
				critChance += sign * (critChance * 0.15f);
				break;

			case Skill.S_CRITMULT:
				critMult += sign * (critMult * 0.15f);
				break;

			case Skill.S_HEALTH_PERC:
				health += sign * (health * 0.10f);
				break;

			case Skill.S_ATTACKSPEED:
				atkspd += sign * (atkspd * 0.15f);
				break;
		}
	}





	
}
