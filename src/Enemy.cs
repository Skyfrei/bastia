using Godot;
using System;
using System.Collections.Generic;


public abstract partial class Enemy : CharacterBody3D{
	private float health;
	private float atkspd;
	private float critChance;
	private float critMult;
	private List<Skill> pow;
	private Rarity rarity = Rarity.NORMAL;
	private ushort level;
	private EnemyType type;

	public CharacterBody3D pl;
	public CollisionShape3D coll;
	public MeshInstance3D mesh;

	private Vector3 dir;

	public override void _Ready(){
		pl = GetParent().GetNode("Player") as CharacterBody3D;
	}

	public override void _PhysicsProcess(double delta){
		Rotation = new Vector3(-1 * pl.Rotation.X, -1 * pl.Rotation.Y, -1 * pl.Rotation.Z) * (float)delta;
	}

	public void Drop(){
		if (IsDead()){
			
		}
	}

	private bool IsDead(){
		return health <= 0;
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
