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
	private float movspeed = 1.0f;
	private float atkspd = 1.0f;
	private Stopwatch timer;

	private int bulletNumber = 50;
	private CharacterBody3D p;
	
	private Vector3 direction;
	private Camera3D cam;

	public override void _Ready(){
		atkspd = 1.0f;
		health = 100.0f;
		critChance = 0.05f;
		critMult = 1.15f;
		bullets = new List<Bullet>();
		timer = new Stopwatch();
		cam = GetParent().GetNode("PlayerCamera") as Camera3D;
		AddCollisionExceptionWith(GetParent().GetNode("Env"));

	}

	public override void _PhysicsProcess(double delta){
		if (bullets.Count == 0)
			Reload();
		timer.Start();
		TakeInput(delta);
	}

	private Vector3  GetMouse(){
		var space = GetWorld3D().DirectSpaceState;
		var mousePos = GetViewport().GetMousePosition();
		var origin = cam.ProjectRayOrigin(mousePos);
		var end = origin + cam.ProjectRayNormal(mousePos) * 100;
		var query = PhysicsRayQueryParameters3D.Create(origin,end);
		query.CollideWithAreas = true;
		query.HitFromInside = true;
		var a = new Godot.Collections.Array<Godot.Rid>{GetRid()};
		query.Exclude = a; 

		var result = space.IntersectRay(query);
		if (result.Count <= 0){
			return Position;
		}
		GD.Print(result["position"]);
		return (Vector3)result["position"];
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
		bool shot = false;
		direction = GetMouse();
		if (direction.Y >= 0.90f){
			LookAt(direction, new Vector3(0, 0.90f, 0));            
		}else
			LookAt(direction);
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
		if (Input.IsActionPressed("leftclick")){
			movspeed += 0.5f;
			//direction = new Vector3(mousePos.X, mousePos.Y, 0);
		}

		if (Input.IsActionPressed("q"))
		{
			if (ReadyToShoot()){
				if (direction == Vector3.Zero)
					ShootBullet(new Vector3(0, 0, 0.5f));
				else
					ShootBullet(-1 * direction);
			}else{
				movspeed = 1.0f;
			}
		} 
		//Implement being clicked only once
	    var d = new Vector3(direction.X, 0, direction.Z);
        if (d.X >= 5.0f)
            d.X = 5.0f;
        if (d.Z >= 5.0f)
            d.Z = 5.0f;
		MoveAndCollide((float)delta * d  * movspeed);
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
