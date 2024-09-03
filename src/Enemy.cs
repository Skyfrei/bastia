using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;


public abstract partial class Enemy : CharacterBody3D{
	private float health;
	private float atkspd;
	private float critChance;
	private float critMult;
	private List<Skill> pow;
	private Rarity rarity = Rarity.NORMAL;
	private ushort level;
	private EnemyType type;

	private List<Bullet> bullets;
	private Stopwatch timer;

	private int bulletNumber = 20;

	public CharacterBody3D pl;
	public CollisionShape3D coll;
	public MeshInstance3D mesh;
	public float randomg;
	private Vector3 dir;

	public override void _Ready(){
		pl = GetParent().GetNode("Player") as CharacterBody3D;
		AddCollisionExceptionWith(GetParent().GetNode("Env"));
		bullets = new List<Bullet>();
		timer = new Stopwatch();

	}

	public override void _PhysicsProcess(double delta){
		LookAt(pl.Position);
		if (bullets.Count == 0)
			Reload();
		timer.Start();
		if (ReadyToShoot()){
			Shoot(pl.Position);
		}
	}

	private void Shoot(Vector3 flyingDir){
	  foreach (var bullet in bullets){
		if (!bullet.IsFlying()){
			bullet.StartFlying(flyingDir, Position);
			return;
		}
	  }
	}

	public void Drop(){
		if (IsDead()){
			
		}
	}

	private bool IsDead(){
		return health <= 0;
	}

	private bool ReadyToShoot(){
		TimeSpan t = timer.Elapsed;
		if (t.Seconds >= atkspd){
			timer.Reset();
			return true;
		}
		return false;
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
	Skill GenerateSkill(){
		var rand = new Random();
		int generated = rand.Next(4);
		if (generated == 0){
			return Skill.S_CRITCHANCE;
		}else if (generated == 1){
			return Skill.S_CRITMULT;
		}else if (generated == 2){
			return Skill.S_HEALTH_PERC;
		}else if (generated == 3){
			return Skill.S_ATTACKSPEED;
		}
		return Skill.S_HEALTH_PERC;
	}
   
	public Enemy(Rarity r, ushort lvl){
		level = lvl;
		health += level * (float)Math.Log(health);
		int powNumber = 3 * (int)r;
	   	}
	
	private void FollowPlayer(){
		Vector3 p = pl.Position;
		dir = new Vector3(p.X - Position.X, p.Y - Position.Y, p.Z - Position.Z);
		 
	}

	public Vector3 GetDirection(){
		FollowPlayer();
		return dir;
	}


	public float DealDamage(){
		
		return 0.0f;
	}

	public void ProcEffect(){
		
	}



}
