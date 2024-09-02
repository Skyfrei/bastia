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
