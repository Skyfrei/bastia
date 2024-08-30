using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody3D{
	private List<Skill> pow; 
	private List<Bullet> bullets;
	private float health;
	private float critChance;
	private float critMult;
	private float atkspd;
	private ushort level = 1;
	private float movspeed = 5.0f;
	private int bulletNumber = 1;
	private CharacterBody3D p;

	public override void _Ready(){
		atkspd = 1.0f;
		health = 100.0f;
		critChance = 0.05f;
		critMult = 1.15f;
		bullets = new List<Bullet>();

	}

	public override void _PhysicsProcess(double delta){
        if (!(bullets.Count == bulletNumber))
            Reload();
		TakeInput(delta);
	}

    private void Reload(){
        var p = GetParent();

		for (int i = 0; i < bulletNumber; i++){
			Bullet b = new Bullet();
            p.AddChild(b);
            bullets.Add(b);
		}
    }


	private void TakeInput(double delta){
		var direction = Vector3.Zero;
		if (Input.IsActionPressed("space")){
			direction.Y += 1;
		}
		if (Input.IsActionPressed("right")){
			direction.X -= 1;
		}
		if (Input.IsActionPressed("left")){
			direction.X += 1;
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
		//Implement being clicked only once
		if (Input.IsActionPressed("q")){
            if (direction == Vector3.Zero)
                ShootBullet(new Vector3(0, 0, 0.5f));
            else
			    ShootBullet(direction);
		}
		Rotation = new Vector3(direction.X, direction.Y, direction.Z);
		MoveAndCollide((float)delta * direction * movspeed);
	}

	private void ShootBullet(Vector3 flyingDir){
	  foreach (var bullet in bullets){
		if (!bullet.IsFlying()){
			bullet.StartFlying(flyingDir, this.Position);
		}
	  }
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
