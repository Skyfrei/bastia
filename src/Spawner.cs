using System;
using Godot;
using System.Collections.Generic;

enum EnemyType{
	ZOMBIE,
	SERPENT,
	AKIRA,
    BOSS
}

public enum Rarity{
	NORMAL,
	MAGIC,
	RARE,
	UNIQUE
}


public partial  class Spawner : Node{
	
	private List<Enemy> enemies;
	private int numOfEnemies = 100;
	private double maxSpawnTime = 1;
	private double sec;

	private CharacterBody3D body;
	private CollisionShape3D coll;
	private MeshInstance3D mesh;

	public Spawner(){}

	public override void _Ready(){
		enemies = new List<Enemy>();
		sec = maxSpawnTime;
	}

	public override void _Process(double delta){
		sec -= delta;   
		LoadEnemyResources();
	}

	private void LoadEnemyResources(){
		if (sec <= 0){
			EnemyType etype = GenerateType();
			Enemy en = SpawnEnemy(etype);
			GetParent().AddChild(en);
			enemies.Add(en);
		   sec = maxSpawnTime;
		}
	}

	private void ScaleSpawnedEnemies(){
		
	}

	private void DestroyEnemy(){

	}


	

	Enemy SpawnEnemy(EnemyType t){;
		Rarity r = GenerateRarity();
		Enemy en = new ZombieShip(r);
		switch(t){
			case EnemyType.ZOMBIE:
				en = new ZombieShip(r);
				break;

			case EnemyType.SERPENT:
				en = new SerpentShip(r);
				break;

			case EnemyType.AKIRA:
				en = new AkiraShip(r);
				break;

			case EnemyType.BOSS:
				en = new BossShip(r);
				break;

		}
		return en;
	}

	EnemyType GenerateType(){
		var rand = new Random();
		int generated = rand.Next(4);

		if (generated == 0){
			return EnemyType.ZOMBIE;
		}else if (generated == 1){
			return EnemyType.SERPENT;
		}else if (generated == 2){
			return EnemyType.AKIRA;
		}else{
            return EnemyType.BOSS;
        }

		return EnemyType.ZOMBIE;
	}

	Rarity GenerateRarity(){
		var rand = new Random();
		int generated = rand.Next(1001);
		if (generated <= 700)
			return Rarity.NORMAL;
		else if (generated <= 900)
			return Rarity.MAGIC;
		else if (generated <= 999)
			return Rarity.RARE;
		else 
			return Rarity.UNIQUE;
	}

	
}
