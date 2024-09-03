using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;


public partial class Player : CharacterBody3D{
	private List<Skill> pow; 
	private List<Bullet> bullets;
	public float health;
	private float critChance = 0.15f;
	private float critMult = 1.5f;
	private float attack = 10.0f;
	private ushort level = 1;
	private float movspeed = 3.0f;
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
        pow = new List<Skill>();
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
		var a = new Godot.Collections.Array<Godot.Rid>{GetRid()};
		query.Exclude = a; 

		var result = space.IntersectRay(query);
		if (result.Count <= 0){
			return Position;
		}
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
		direction = GetMouse();
		LookAt(direction, new Vector3(0, 0.90f, 0));            
		if (direction != Vector3.Zero){
			direction = direction.Normalized();
		}
		if (Input.IsActionPressed("leftclick")){
			var d = new Vector3(direction.X, 0, direction.Z);
            MoveAndCollide((float)delta * d  * movspeed);
			//direction = new Vector3(mousePos.X, mousePos.Y, 0);
		}

		if (Input.IsActionPressed("q"))
		{
			if (ReadyToShoot()){
				ShootBullet(-1 * direction);
            }
		} 
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
			bullet.SetDamage(CalculateDamage());
			bullet.StartFlying(flyingDir, Position);
			return;
		}
	  }
	}

	private float CalculateDamage(){
		foreach (var p in pow){
		   switch(p){
			   case Skill.S_CRITCHANCE:
					critChance += 0.05f;
				   break;
				case Skill.S_CRITMULT:
				   critMult += 0.1f;
				   break;
				case Skill.S_HEALTH_PERC:
				   health += 100f;
				   break;
				case Skill.S_ATTACKSPEED:
					atkspd -= 0.01f;
					break;
				default:
					break;
		   }
		}

		float dmg = 0.0f;
		var ran = new Random();
		bool c = critChance >= ran.NextDouble();
		if (c){
			dmg = critMult * attack + attack;
		}else
			dmg = attack;
		return dmg;
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
