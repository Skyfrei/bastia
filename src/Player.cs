using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;


public partial class Player : CharacterBody3D{
	private List<Skill> pow; 
	private List<Bullet> bullets;
	private float health;
	private float critChance;
	private float critMult;
	private ushort level = 1;
	private float movspeed = 5.0f;
    private float atkspd = 1.0f;
    private Stopwatch timer;

	private int bulletNumber = 50;
	private CharacterBody3D p;
    
    private Vector3 direction;

	public override void _Ready(){
		atkspd = 1.0f;
		health = 100.0f;
		critChance = 0.05f;
		critMult = 1.15f;
		bullets = new List<Bullet>();
        timer = new Stopwatch();

	}

	public override void _PhysicsProcess(double delta){
        if (!(bullets.Count == bulletNumber))
            Reload();
        timer.Start();
		TakeInput(delta);
	}

    private void Reload(){
        var p = GetParent();

		for (int i = 0; i < bulletNumber; i++){
			Bullet b = new Bullet();
            b.type = BulletType.PLAYER;
            p.AddChild(b);
            bullets.Add(b);
		}
    }
    
    public override void _Input(InputEvent @event){

    }

	private void TakeInput(double delta){
		direction = Vector3.Zero;
        bool shot = false;
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
        if (Input.IsActionPressed("q"))
        {
            if (ReadyToShoot()){
                if (direction == Vector3.Zero)
                    ShootBullet(new Vector3(0, 0, 0.5f));
                else
			        ShootBullet(direction);
            }
        } 
		//Implement being clicked only once
		Rotation = new Vector3(direction.X, direction.Y, direction.Z);
        
		MoveAndCollide((float)delta * direction * movspeed);
	}
    
    private bool ReadyToShoot(){
        TimeSpan t = timer.Elapsed;
        if (t.Seconds >= atkspd){
            timer.Reset();
            return true;
        }
        return false;
    }

	private void ShootBullet(Vector3 flyingDir){
	  foreach (var bullet in bullets){
		if (!bullet.IsFlying()){
			bullet.StartFlying(flyingDir, Position);
            return;
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
