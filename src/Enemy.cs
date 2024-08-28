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

   
	public Enemy(Rarity r, ushort lvl){
		level = lvl;
		health += level * (float)Math.Log(health);
		int powNumber = 3 * (int)r;

	}

	public float DealDamage(){
		
		return 0.0f;
	}

	public void ProcEffect(){
		
	}



}
